using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExceptionEx2
{
    class Program
    {
        static void Main(string[] args)
        {
            int x, y, z;
            x = 5;
            y = 0;
            try
            {                                    // try 语句块包含可能抛出异常的语句
                z = x / y;
                Console.WriteLine("{0}/{1}={2}", x, y, z);
            }
            catch (Exception ex)                 // catch 语句捕获指定的异常
            {                                    //catch 语句块包含异常恢复代码
                Console.WriteLine("异常：{0}", ex.Message); 
            }
        }
    }
}
