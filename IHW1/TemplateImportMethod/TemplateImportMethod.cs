using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHW1.TemplateImportMethod
{
    public abstract class TemplateImportMethod
    {
        public BankAccount TemplateImport(string fileName)
        {
            return Import(fileName);
        }
        public abstract BankAccount Import(string fileName);
    }
}
