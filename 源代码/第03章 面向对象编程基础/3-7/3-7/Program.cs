using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3_7
{
    class Test
    {
        string[] s = { "张三", "李四", "王五" };
        public int this[string str]
        {
            get
            {
                int i = 0;
                foreach (string temp in s)
                {
                    if (temp == str) return i;
                    i++;
                }
                return -1;
            }            
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            Test t = new Test();
            Console.WriteLine(t["张三"]);
            Console.WriteLine(t["小明"]);
            Console.ReadKey();
        }
    }
}
