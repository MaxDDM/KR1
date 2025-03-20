using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHW1.Command
{
    public abstract class Command
    {
        public BankAccount receiver;
        public abstract void Execute(int sum = -1, string description = "", string category = "", 
            DateTime begin = new DateTime(), DateTime end = new DateTime());
        public abstract void Undo();
    }
}
