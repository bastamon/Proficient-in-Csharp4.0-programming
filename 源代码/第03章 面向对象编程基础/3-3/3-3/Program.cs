using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3_3
{
    public class Car
    {
        public double OilMeter=0;//油表数值，单位：升
        public double OilVolume;//油箱容量，单位：升
        public double OilCosumption;//油耗, 每百公里消耗汽油数量，单位：升
        public double Mileage;//里程数，单位：公里
        static public int count;
        public Car()
        {
            count++;
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            Car car1 = new Car();
            Car car2 = new Car();
            car1.OilVolume = 50;
            car2.OilVolume = 100;
            Console.WriteLine("car1的油耗：{0}", car1.OilVolume);
            Console.WriteLine("car2的油耗：{0}", car2.OilVolume);
            Console.WriteLine("Car类的实例个数：{0}", Car.count);
            Console.ReadKey();
        }
    }  
}
