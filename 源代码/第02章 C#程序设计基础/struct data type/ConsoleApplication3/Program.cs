using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication3
{
    class Program
    {
        struct Student
        {
            string name;//结构里，默认为私有（private）变量
            public int age;
            string address;
            public Student(int a)//与结构同名的构造函数
            {
                age = a;
                address = "";
                name = "";
            }
            public string AccessName()     //访问私有变量的成员方法
            {
                return name;
            } 

        }
        static void Main(string[] args)
        {
            Student s1 = new Student(19);
            Console.WriteLine("The age of student s1 is " + s1.age);
            Console.Read();


        }
    }
}
