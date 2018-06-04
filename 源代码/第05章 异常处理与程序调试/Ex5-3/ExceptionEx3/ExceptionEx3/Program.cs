using System;

namespace ExceptionEx3
{
    class Program
    {
        static void Main(string[] args)
        {
            object o = null;
            try
            {
                int i = (int) o;   
                Console.WriteLine("i = {0}", i);
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine("异常：{0}", ex.Message);
            }

        }
    }
}
