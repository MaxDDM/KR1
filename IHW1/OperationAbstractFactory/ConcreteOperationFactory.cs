using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IHW1.OperationAbstractFactory.Operation;

namespace IHW1.OperationAbstractFactory
{
    [Serializable]
    public class ConcreteOperationFactory : OperationAbstractFactory
    {
        public ConcreteOperationFactory() { }
        public override Operation.Operation CreateOperation(int balance, int id, string type, 
            int bank_account_id, int amount, string description, int category_id)
        {
            if (amount < 0 || (type == "Расход" && amount > balance))
            {
                throw new ArgumentException();
            }
            return new Operation.Operation(id, type, bank_account_id, amount, description, category_id);
        }
    }
}
