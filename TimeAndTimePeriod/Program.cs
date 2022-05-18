using System;

namespace TimeAndTimePeriod
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var t0 = new Time(12, 2, 12);
            Console.WriteLine(t0);

            var t1 = new Time(1, 9, 12);
            Console.WriteLine(t1);

            var t3 = new TimePeriod("72:59:59");
            Console.WriteLine(t1 - t3);
            Console.WriteLine(t0.Minus(t3));
            Console.WriteLine(t0.Plus(t3));

            Console.WriteLine(TimePeriod.Minus(t0, t1));
        }
    }
}
