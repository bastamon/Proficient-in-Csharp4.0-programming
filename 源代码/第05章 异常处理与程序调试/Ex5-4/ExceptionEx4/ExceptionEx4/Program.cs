using System;

namespace ExceptionEx4
{
    class Program
    {
        static void Main(string[] args)
        {
            object o = "string";
            try
            {
                int i = (int)o;
                Console.WriteLine("i = {0}", i);
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine("异常：{0}", ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("异常：{0}", ex.Message);
            }
        }
    }
}
