using System;

namespace ExceptionEx1
{
    class Program
    {
        static void Main(string[] args)
        {
            int x, y, z;
            x = 5;
            y = 0;
            z = x / y;
            Console.WriteLine("{0}/{1}={2}", x, y, z);
        }
    }
}
