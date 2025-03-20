using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHW1.Command
{
    public class Invoker
    {
        Command command;
        public void SetCommand(Command c)
        {
            command = c;
        }
        public void Run(int sum = -1, string decriprion = "", string category = "", 
            DateTime begin = new DateTime(), DateTime end = new DateTime())
        {
            command.Execute(sum, decriprion, category, begin, end);
        }
        public void Cancel()
        {
            command.Undo();
        }
    }
}
