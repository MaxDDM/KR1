using IHW1.CommandServices;
using IHW1.CommandServices.ConcreteServices;
using IHW1.OperationAbstractFactory;
using IHW1.OperationAbstractFactory.Operation;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

namespace IHW1
{
    [Serializable]
    public class BankAccount
    {
        [JsonInclude]
        public int id = 0;
        [JsonInclude]
        public string name;
        [JsonInclude]
        public int balance;

        [JsonInclude]
        public List<Operation> operations = new List<Operation>();
        [JsonInclude]
        public List<Category> categories = new List<Category>();
        [NonSerialized]
        private ServiceCollection services = null;
        [NonSerialized]
        private ServiceProvider serviceProvider;

        private ConcreteOperationFactory operationCreator = null;

        public BankAccount() { }
        public BankAccount(int id, string name)
        {
            this.id = id;
            balance = 0;
            this.name = name;

            categories.Add(new Category(0, "default", "default"));
        }

        private void AddServices()
        {
            services = new ServiceCollection();
            services.AddSingleton<AnalyticsService>();
            services.AddSingleton<ContributeService>();
            services.AddSingleton<WithdrawService>();
            serviceProvider = services.BuildServiceProvider();
        }

        public void Statistics(DateTime begin, DateTime end)
        {
            Dictionary<string, List<Operation>> group = new Dictionary<string, List<Operation>>();
            for (int i = 0; i < operations.Count(); ++i)
            {
                if (operations[i].date <= end && operations[i].date >= begin)
                { 
                    Category? category = categories.Find(cat => cat.id == operations[i].category_id);
                    if (category != null)
                    {
                        if (!group.ContainsKey(category.name))
                        {
                            group.Add(category.name, new List<Operation> { operations[i] });
                        }
                        else
                        {
                            group[category.name].Add(operations[i]);
                        }
                    }
                }
            }
            Console.WriteLine("Баланс: " + balance + "\n");
            var keys = group.Keys;
            foreach (string s in keys)
            {
                Console.WriteLine(s + ":");
                for (int i = 0; i < group[s].Count; ++i)
                {
                    Console.WriteLine(group[s][i]);
                }
                Console.WriteLine();
            }
        }

        public void Withdraw(int sum, string description, string category)
        {
            int category_id = 0;
            Category cat = categories.Find(cat => cat.name == category);
            if (cat != null)
            {
                category_id = cat.id;
            }
            if (operationCreator == null)
            {
                operationCreator = new ConcreteOperationFactory();
            }
            operations.Add(operationCreator.CreateOperation(balance, operations.Count(), "Расход", id, sum, description, category_id));
            balance -= sum;
        }

        public void Contribute(int sum, string description, string category)
        {
            int category_id = 0;
            Category cat = categories.Find(cat => cat.name == category);
            if (cat != null)
            {
                category_id = cat.id;
            }
            if (operationCreator == null)
            {
                operationCreator = new ConcreteOperationFactory();
            }
            operations.Add(operationCreator.CreateOperation(balance, operations.Count(), "Доход", id, sum, description, category_id));
            balance += sum;
        }

        public void AddCategory(string type, string name)
        {
            for (int i = 0; i < categories.Count; ++i)
            {
                if (categories[i].name == name)
                {
                    throw new ArgumentException();
                }
            }
            categories.Add(new Category(categories.Count(), type, name));
        }

        public void RemoveCategory(string name)
        {
            int idToDel = -1;
            for (int i = 0; i < categories.Count; i++)
            {
                if (categories[i].name == name)
                {
                    idToDel = i;
                    break;
                }
            }
            if (idToDel != -1)
            {
                categories.RemoveAt(idToDel);
            }
        }

        public void Operation<T>(int sum = 0, string description = "", string category = "", 
            DateTime begin = new DateTime(), DateTime end = new DateTime()) where T : Service
        {
            if (services == null)
            {
                AddServices();
            }
            var service = serviceProvider.GetService<T>();
            BankAccount account = this;
            service.Execute(ref account, sum, description, category, begin, end);
            balance = account.balance;
            operations = account.operations;
        }

        public void ChangeDescription(int id, string descr)
        {
            if (id < 0 || id > operations.Count() - 1)
            {
                throw new ArgumentException();
            }
            operations[id].description = descr;
        }
    }
}