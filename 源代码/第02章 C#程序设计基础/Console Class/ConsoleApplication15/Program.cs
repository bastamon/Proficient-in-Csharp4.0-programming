using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication15
{
    class Program
    {
        static void Main(string[] args)
        {
            int a, b;
            Console.Write("请输入长方形的长和宽，以空格隔开，以回车结束：");
            string str = Console.ReadLine();
            string[] result = str.Split(' ');
            a = Convert.ToInt32(result[0]);
            b = Convert.ToInt32(result[1]);
            Console.WriteLine("长为{0}宽为{1}的面积为{2}", a, b, a * b);
            Console.ReadKey(); 

        }
    }
}
