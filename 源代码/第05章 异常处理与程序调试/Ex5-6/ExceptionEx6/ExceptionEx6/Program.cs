using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExceptionEx6
{
    
    class AgeException : Exception
    {
        public override string Message
        {
            get
            {
                return base.Message;//"数字不符合年龄的要求。";
            }
        }
        public AgeException()
        {
            
        }
    }
    class Program
    {
        static int ToAge(string str)
        {
            int age = 0;
            try
            {
                age = int.Parse(str);
                if (age < 0 || age > 150)
                {
                    age = 0;
                    throw new AgeException();
                }
            }
            catch
            {
                throw new AgeException();
            }
            return age;

        }
        static void Main(string[] args)
        {
            string str = "1200";
            try
            {
                int age = ToAge(str);
                Console.WriteLine(age);
            }
            catch (AgeException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
