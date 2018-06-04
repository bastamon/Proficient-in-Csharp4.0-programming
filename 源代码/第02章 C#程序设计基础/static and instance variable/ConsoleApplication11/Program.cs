using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication11
{
    class Program
    {
        public class VariableInclude
        {
            public static string name = "AndyLau";           //定义了静态字符串变量
            public static int age = 40;                      //定义了静态整型变量
            public string country = "china-Honkong";         //定义了非静态变量
        }

        static void Main(string[] args)
        {
            //静态变量不用定义实例对象，可直接调用
            Console.WriteLine(VariableInclude.name); Console.WriteLine(VariableInclude.age);
            // 非静态变量不能直接调用，编译报错
            //Console.WriteLine(VariableInclude.country); 
            //定义类的对象后，才能调用非静态变量
            VariableInclude vi1 = new VariableInclude(); Console.WriteLine(vi1.country);
            Console.Read();

        }
    }
}
