using IHW1.Exporter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IHW1
{
    [Serializable]
    public class Bank
    {
        List<BankAccount> _accounts = new List<BankAccount>();
        public List<Exporter.Exporter> exporters = new List<Exporter.Exporter>();

        public List<BankAccount> Accounts
        {
            get
            {
                return _accounts;
            }
            private set { }
        }
        public void Add(BankAccount account)
        {
            if (_accounts.Find(acc => acc.name == account.name) == null)
            {
                account.id = _accounts.Count();
                _accounts.Add(account);
            } else
            {
                throw new ArgumentException();
            }
        }
        public void Remove(string name)
        {
            for (int i = 0; i < _accounts.Count(); i++)
            {
                if (_accounts[i].name == name)
                {
                    _accounts.RemoveAt(i);
                    return;
                }
            }
        }
        public void Accept(VisitorExporter visitor, int id)
        {
            for (int i = 0; i < exporters.Count(); i++)
            {
                exporters[i].Accept(visitor, _accounts[i]);
            }
        }

        public BankAccount Get(string name)
        {
            for (int i = 0; i < _accounts.Count(); ++i)
            {
                if (_accounts[i].name == name)
                {
                    return _accounts[i];
                }
            }
            return null;
        }

        public void ChangeAccount(string accName, BankAccount account)
        {
            int accId = getId(accName);
            _accounts[accId] = account;
        }

        public void ChangeAccountOperation(string accName, int opId, string descr)
        {
            int accId = getId(accName);
            if (accId == -1)
            {
                throw new ArgumentException();
            }
            _accounts[accId].ChangeDescription(opId, descr);
        }

        public void AddCategoryToAccount(string accName, string type, string name)
        {
            int accId = getId(accName);
            if (accId == -1)
            {
                throw new ArgumentException();
            }
            _accounts[accId].AddCategory(type, name);
        }

        public void RemoveCategoryFromAccount(string accName, string catName)
        {
            int accId = getId(accName);
            _accounts[accId].RemoveCategory(catName);
        }

        private int getId(string name)
        {
            int accId = -1;
            for (int i = 0; i < _accounts.Count(); ++i)
            {
                if (_accounts[i].name == name)
                {
                    accId = i;
                    break;
                }
            }
            return accId;
        }
    }
}
