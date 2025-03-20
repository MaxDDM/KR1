using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHW1
{
    public class Reader
    {
        public static int ReadInt()
        {
            int x;
            if (!int.TryParse(Console.ReadLine(), out x))
            {
                return -1;
            }
            return x;
        }

        public static string ReadOperationType()
        {
            Console.WriteLine("Введите название операции: 'Доход' или 'Расход'. " +
                            "Если введёте что-то другое, операция не будет выполнена\n");
            String operationType = Console.ReadLine();
            if (operationType != "Доход" && operationType != "Расход")
            {
                return "";
            }
            return operationType;
        }

        public static DateTime ReadDate()
        {
            Console.WriteLine("Введите год\n");
            int year = ReadInt();
            Console.WriteLine("Введите месяц\n");
            int month = ReadInt();
            Console.WriteLine("Введите день\n");
            int day = ReadInt();
            return new DateTime(year, month, day);
        }
    }
}
