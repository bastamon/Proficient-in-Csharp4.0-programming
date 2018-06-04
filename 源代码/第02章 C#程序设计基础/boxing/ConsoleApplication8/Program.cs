using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication8
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 123;
            object o = i;  //装箱转换        
            i = 456;  //改变i的内容
            Console.WriteLine("值类型的值为{0}", i);
            Console.WriteLine("引用类型的值为{0}", o);
            Console.Read();

        }
    }
}
