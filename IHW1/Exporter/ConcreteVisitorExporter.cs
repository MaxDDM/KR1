using IHW1.Exporter.ConcreteExporters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IHW1.Exporter
{
    [Serializable]
    public class ConcreteVisitorExporter : VisitorExporter
    {
        public ConcreteVisitorExporter() { }
        public override void ExportJSON(JSONExporter exporter, BankAccount account)
        {
            exporter.ExportJSON(account);
        }
    }
}
