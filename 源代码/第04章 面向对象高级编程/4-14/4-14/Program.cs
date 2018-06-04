using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example4_14
{
    public class Stu
    {
        public string name;
        public int age;
        public void Info()
        {
            Console.WriteLine("姓名 = {0}，年龄 = {1}", name, age);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<Stu> list1 = new List<Stu>();
            Stu stu1 = new Stu();
            stu1.name = "小明";
            stu1.age = 5;
            list1.Add(stu1);
            Stu stu2 = new Stu();
            stu2.name = "小丽";
            stu2.age = 4; 
            list1.Add(stu2);
            foreach (Stu stutemp in list1)
                stutemp.Info();
            Console.ReadKey();

        }
    }
}
