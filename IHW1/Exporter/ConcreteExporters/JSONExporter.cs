using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IHW1.Exporter.ConcreteExporters
{
    [Serializable]
    public class JSONExporter : Exporter
    {
        public JSONExporter() { }
        public override void Accept(VisitorExporter visitor, BankAccount account)
        {
            visitor.ExportJSON(this, account);
        }
        public void ExportJSON(BankAccount account)
        {
            using (FileStream fs = new FileStream("account.json", FileMode.OpenOrCreate))
            {
                JsonSerializer.Serialize<BankAccount>(fs, account);
            }

        }
    }
}
