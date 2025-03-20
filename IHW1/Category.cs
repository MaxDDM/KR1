using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHW1
{
    [Serializable]
    public class Category
    {
        public int id = 0;
        public string type;
        public string name;

        public Category() { }

        public Category(int id, string type, string name)
        {
            this.id = id;
            this.type = type;
            this.name = name;
        }
    }
}
