using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHW1.Command.Decotrator
{
    public class TimeMeasureDecorator : Decorator
    {

        public override void Execute(int sum = -1, string description = "", string category = "", 
            DateTime begin = default, DateTime end = default)
        {
            var sw = new Stopwatch();
            sw.Start();
            command.Execute(sum, description, category, begin, end);
            sw.Stop();
            Console.WriteLine("Время работы команды: " + sw.Elapsed);
        }

        public override void Undo()
        {
            var sw = new Stopwatch();
            sw.Start();
            command.Undo();
            sw.Stop();
            Console.WriteLine("Время работы команды: " + sw.Elapsed);
        }
    }
}
