using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IHW1.OperationAbstractFactory.Operation
{
    [Serializable]
    public class Operation : AbstractOperation
    {
        [JsonInclude]
        public int id;
        [JsonInclude]
        public string type;
        [JsonInclude]
        public int bank_account_id;
        [JsonInclude]
        public int amount;
        [JsonInclude]
        public DateTime date;
        [JsonInclude]
        public string description;
        [JsonInclude]
        public int category_id;

        public Operation() { }
        public Operation(int id, string type, int bank_account_id, int amount, string description, int category_id)
        {
            this.id = id;
            this.type = type;
            this.bank_account_id = bank_account_id;
            this.amount = amount;
            this.date = DateTime.Now;
            this.description = description;
            this.category_id = category_id;
        }

        public override string ToString()
        {
            return "Id: " + id + ", type " + type + ", bank_account_id: " + bank_account_id +
                ", amount: " + amount + ", date: " + date.Day + "." + date.Month + "." + date.Year +
                ", description: " + description + ", category_id: " + category_id;
        }
    }
}