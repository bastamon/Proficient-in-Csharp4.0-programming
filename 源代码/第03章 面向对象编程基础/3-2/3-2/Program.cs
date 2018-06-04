using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example3_2
{
    public class Car
    {
        public double OilMeter;//油表数值，单位：升
        public double OilVolume;//油箱容量，单位：升
        public double OilCosumption;//油耗, 每百公里消耗汽油数量，单位：升
        public double Mileage;//里程数，单位：公里
        public void Info()
        {
            Console.WriteLine("这辆车已经行驶 {0} 公里，油箱中还有 {1}升汽油，油耗为百公里{2} 升.", Mileage, OilMeter, OilCosumption);
        }
        public Car()
        {
            OilCosumption = 8.0;
        }
        public Car(double x,double y,double z)
        {
            Mileage = x;
            OilMeter = y;
            OilCosumption = z;
        }


    }
    class Program
    {
        static void Main(string[] args)
        {
            Car car1 = new Car( );
            car1.Info( );
            Car car2 = new Car(1000,30,12);
            car2.Info( );
            Console.ReadKey( );
        }
    }
}
