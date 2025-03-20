using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using IHW1.OperationAbstractFactory.Operation;

namespace IHW1.OperationAbstractFactory
{
    [Serializable]
    [JsonDerivedType(typeof(ConcreteOperationFactory))]
    public abstract class OperationAbstractFactory
    {
        public OperationAbstractFactory() { }
        public abstract AbstractOperation CreateOperation(int balance, int id, string type, int bank_account_id, int amount, string description, int category_id);
    }
}
