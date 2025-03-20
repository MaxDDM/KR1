using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json;
using JsonConverter = Newtonsoft.Json.JsonConverter;
using JsonConverterAttribute = Newtonsoft.Json.JsonConverterAttribute;

namespace IHW1.OperationAbstractFactory.Operation
{
    [Serializable]
    [JsonDerivedType(typeof(Operation))]
    public abstract class AbstractOperation
    {
        public AbstractOperation() { }
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
    }
}
