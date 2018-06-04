using System;

namespace ExceptionEx7
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ThrowException();
            }
            catch(DivideByZeroException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void ThrowException()
        {
            DivideByZeroException ex = new DivideByZeroException();
            throw ex;
        }

    }
}
