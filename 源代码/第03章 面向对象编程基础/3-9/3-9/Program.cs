using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example3_9
{
    class Program
    {
        static void Main(string[] args)
        {

            /*初始化DateTime类对象的7个整型参数，年，月，日，时，分，秒，毫秒*/
            DateTime t1 = new DateTime(2013, 9, 5, 18, 7, 30, 200);
            DateTime t2 = new DateTime(2010, 9, 1);//也可以只给出年月日
            TimeSpan ts = t1 - t2;
            Console.WriteLine(t1.ToString());
            Console.WriteLine("t1-t2 = " + ts.ToString());
            Console.WriteLine("t1={0}年{1}月{2}日{3}时{4}分{5}秒{6}毫秒", t1.Year, t1.Month, t1.Day, t1.Hour, t1.Minute, t1.Second, t1.Millisecond);
            Console.WriteLine("t2是{0}年的第{1}天,是{2}", t1.Year, t1.DayOfYear, t1.DayOfWeek);
            Console.WriteLine("t1的时间部分为：{0}", t1.TimeOfDay);
            Console.WriteLine("当前时间为" + DateTime.Now.ToString());
            Console.ReadKey();
        }
    }
}
