using System;
using System.IO;

namespace ExceptionEx5
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream file = null;
            FileInfo fileInfo = null;

            try
            {
                fileInfo = new FileInfo("C:\\file.txt");

                file = fileInfo.OpenWrite();
                file.WriteByte(0xF);
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (file != null)
                {
                    file.Close();
                }
            }

        }
    }
}
