using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHW1.Exporter
{
    [Serializable]
    public abstract class Exporter
    {
        public Exporter() { }
        public abstract void Accept(VisitorExporter visitor, BankAccount account);
    }
}
