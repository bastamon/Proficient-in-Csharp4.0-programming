using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3_5
{
    public class Car
    {
        public double OilMeter = 0;//油表数值，单位：升
        private double OilVolume;//油箱容量，单位：升
        public double OilCosumption;//油耗, 每百公里消耗汽油数量，单位：升
        public double Mileage;//里程数，单位：公里
        public Car()
        {
            OilMeter = 0;
            OilVolume = 50;
            OilCosumption = 9;
            Mileage = 0;
        }
        public double OVolume
        {
            set
            {
                OilVolume = value;
            }
            get
            {
                return OilVolume;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Car car1 = new Car();
            car1.OVolume = 100;
            Console.WriteLine("这辆车的油箱容积为：{0}",car1.OVolume);
            Console.ReadKey();
        }
    }
}
