using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IHW1.TemplateImportMethod
{
    public class ImportJSONMethod : TemplateImportMethod
    {
        public override BankAccount Import(string fileName)
        {
            BankAccount account;
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                account = JsonSerializer.Deserialize<BankAccount>(fs);
            }
            return account;
        }
    }
}
