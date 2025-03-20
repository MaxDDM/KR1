using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHW1.Command.Decotrator
{
    public abstract class Decorator : Command
    {
        protected Command command;

        public void SetComponent(Command component)
        {
            this.command = component;
        }

        public override abstract void Execute(int sum = -1, string description = "", string category = "", 
            DateTime begin = default, DateTime end = default);
        public override abstract void Undo();
    }
}
