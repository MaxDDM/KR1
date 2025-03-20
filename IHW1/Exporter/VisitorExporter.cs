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
    public abstract class VisitorExporter
    {
        public VisitorExporter() { }
        public abstract void ExportJSON(JSONExporter exporter, BankAccount account);

    }
}
