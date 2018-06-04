using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication9
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 123;
            Console.WriteLine("装箱前i的值为{0}", i);
            object o = i;  //装箱转换        
            o = 456;  //改变o的内容
            i = (int)o;  //拆箱转换
            Console.WriteLine("拆箱后i的值为{0}", i);
            Console.Read();

        }
    }
}
