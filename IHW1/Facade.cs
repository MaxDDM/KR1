using IHW1.Command;
using IHW1.Command.ConcreteCommands;
using IHW1.Command.Decotrator;
using IHW1.Exporter;
using IHW1.Exporter.ConcreteExporters;
using IHW1.TemplateImportMethod;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Principal;

namespace IHW1
{
    public class Facade
    {
        Bank _bank;
        Invoker _invoker; 

        public Facade()
        {
            if (File.Exists("bank.data"))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fs = new FileStream("bank.data", FileMode.OpenOrCreate))
                {
                    _bank = (Bank)formatter.Deserialize(fs);
                }
            } 
            else
            {
                _bank = new Bank();
            }
            _invoker = new Invoker();
        }

        public void InteractWithClient()
        {
            string Input;
            do
            {
                Console.WriteLine("Если хотите создать счёт, введите 1,\n" +
                    "если хотите закрыть счёт, введите 2,\n" +
                    "если хотите внести или вывести деньги, введите 3,\n" +
                    "если хотите отредактировать описание операции, введите 4,\n" +
                    "если хотите добавить категорию, введите 5,\n" +
                    "если хотите удалить категорию, введите 6,\n" +
                    "если хотите получить аналитику по счёту, введите 7,\n" +
                    "если хотите импортировать информацию о счёте, введите 8,\n" +
                    "если хотите экспортировать информацию о счёте, введите 9,\n" +
                    "если хотите закончить, введите что-нибудь другое.\n");
                Input = Console.ReadLine();
                string name = "";
                if (Input[0] <= '9' && Input[0] >= '0')
                {
                    Console.WriteLine("Введите имя счёта\n");
                    name = Console.ReadLine();
                }
                switch (Input)
                {
                    case "1":
                        try
                        {
                            _bank.Add(new BankAccount(_bank.Accounts.Count(), name));
                        } catch (ArgumentException)
                        {
                            Console.WriteLine("Такое имя уже использовано\n");
                            continue;
                        }
                        break;
                    case "2":
                        _bank.Remove(name);
                        break;
                    case "3":
                        Console.WriteLine("Введите сумму:\n");
                        int sum = Reader.ReadInt();
                        if (sum < 0)
                        {
                            Console.WriteLine("Неверный формат ввода\n");
                            continue;
                        }
                        BankAccount accountToOperate = _bank.Get(name);
                        if (accountToOperate == null)
                        {
                            Console.WriteLine("Нет счёта с таким имененем\n");
                            continue;
                        }
                        Console.WriteLine("Введите комментарий к операции\n");
                        string comment = Console.ReadLine();
                        Console.WriteLine("Введите категорию\n");
                        string cat = Console.ReadLine();
                        Command.Command Command = null;
                        string operationType = Reader.ReadOperationType();
                        if (operationType == "Доход")
                        {
                            Command = new ContributeCommand(accountToOperate);
                        }
                        else if (operationType == "Расход")
                        {
                            Command = new WithdrawCommand(accountToOperate);
                        }
                        else
                        {
                            continue;
                        }
                        TimeMeasureDecorator dec = new TimeMeasureDecorator();
                        dec.SetComponent(Command);
                        _invoker.SetCommand(dec);
                        try
                        {
                            _invoker.Run(sum, comment, cat);
                        }
                        catch (ArgumentException)
                        {
                            Console.WriteLine("С введённым значением невозможно провести" +
                                "операцию");
                            continue;
                        }
                        break;
                    case "4":
                        Console.WriteLine("Введите id операции\n");
                        int opId = Reader.ReadInt();
                        if (opId == -1)
                        {
                            Console.WriteLine("Неверный формат ввода\n");
                            continue;
                        }
                        Console.WriteLine("Введите новое описание\n");
                        string new_description = Console.ReadLine();
                        try
                        {
                            _bank.ChangeAccountOperation(name, opId, new_description);
                        } catch (ArgumentException)
                        {
                            Console.WriteLine("Неверный формат ввода");
                            continue;
                        }
                        
                        break;
                    case "5":
                        string operationType1 = Reader.ReadOperationType();
                        if (operationType1 == "")
                        {
                            continue;
                        }
                        Console.WriteLine("Введите имя операции:\n");
                        string nameOp = Console.ReadLine();
                        try
                        {
                            _bank.AddCategoryToAccount(name, operationType1, nameOp);
                        } catch (ArgumentException)
                        {
                            Console.WriteLine("Это имя категории уже занято\n");
                            continue;
                        }
                        break;
                    case "6":
                        Console.WriteLine("Введите имя операции:\n");
                        string nameOp1 = Console.ReadLine();
                        _bank.RemoveCategoryFromAccount(name, nameOp1);
                        break;
                    case "7":
                        BankAccount accountToOperate1 = _bank.Get(name);
                        if (accountToOperate1 == null)
                        {
                            Console.WriteLine("Нет счёта с таким имененем\n");
                            continue;
                        }
                        Console.WriteLine("Введите дату начала периода:");
                        DateTime begin = Reader.ReadDate();
                        Console.WriteLine("Введите дату конца периода:");
                        DateTime end = Reader.ReadDate();
                        Command.Command Command1 = new GetAnalyticsCommand(accountToOperate1);
                        TimeMeasureDecorator dec1 = new TimeMeasureDecorator();
                        dec1.SetComponent(Command1);
                        _invoker.SetCommand(dec1);
                        _invoker.Run(-1, "", "", begin, end);
                        break;
                    case "8":
                        Console.WriteLine("Введите имя файла:\n");
                        string fileName = Console.ReadLine();
                        TemplateImportMethod.TemplateImportMethod importer = new ImportJSONMethod();
                        BankAccount account = null;
                        try
                        {
                            account = importer.Import(fileName);
                        } catch(Exception)
                        {
                            Console.WriteLine("Не удалось импортировать\n");
                            continue;
                        }
                        _bank.ChangeAccount(name, account);
                        break;
                    case "9":
                        BankAccount accountToOperate2 = _bank.Get(name);
                        if (accountToOperate2 == null)
                        {
                            Console.WriteLine("Нет счёта с таким имененем\n");
                        }
                        _bank.exporters.Add(new JSONExporter());
                        _bank.Accept(new ConcreteVisitorExporter(), accountToOperate2.id);
                        break;
                    default:
                        BinaryFormatter formatter = new BinaryFormatter();
                        using (FileStream fs = new FileStream("bank.data", FileMode.OpenOrCreate))
                        {
                            formatter.Serialize(fs, _bank);
                        }
                        break;
                }
            } while (Input[0] <= '9' && Input[0] >= '1' && Input.Length == 1);
        }
    }
}