using IHW1.CommandServices.ConcreteServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHW1.Command.ConcreteCommands
{
    internal class WithdrawCommand : Command
    {
        BankAccount receiver;
        public WithdrawCommand(BankAccount r)
        {
            receiver = r;
        }
        public override void Execute(int sum = -1, string description = "", string category = "", 
            DateTime begin = new DateTime(), DateTime end = new DateTime())
        {
            receiver.Operation<WithdrawService>(sum, description, category, begin, end);
        }

        public override void Undo()
        { }
    }
}
