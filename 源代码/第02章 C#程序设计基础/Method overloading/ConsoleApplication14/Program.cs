using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication14
{
    class Program
    {
        class CalcArea
{
      //计算圆的面积
      public static double Area(double r)
      {
            return (Math.PI*r*r);
       }
       //计算矩形的面积
       public static double Area(double a,double b)
       {
             return (a * b);
       }
       //计算三角开的面积
       public static double Area(double a, double b,double c)
       {
             double l;
             l = (a + b + c) / 2;
             return (Math.Sqrt(l*(l-a)*(l-b)*(l-c)));
         }
}

        static void Main(string[] args)
        {
            Console.WriteLine("圆的面积是：" + Convert.ToInt32(CalcArea.Area(5)));
            Console.WriteLine("矩形的面积是：" + Convert.ToInt32(CalcArea.Area(6, 10)));
            Console.WriteLine("三角形的面积是：" + Convert.ToInt32(CalcArea.Area(4, 5, 6)));
            Console.ReadLine();

        }
    }
}
