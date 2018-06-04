using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example3_1
{
    public class Car
    {
        public  double  OilMeter;//油表数值，单位：升
        public  double  OilVolume;//油箱容量，单位：升
        public  double  OilCosumption;//油耗, 每百公里消耗汽油数量，单位：升
        public  double  Mileage;//里程数，单位：公里
        public  void  Info()
        {
            Console.WriteLine("This car already run {0} km, has {1} litre oil in tank，OilCosumption is {2} litre.", Mileage, OilMeter,OilCosumption);
        }
        public Car()
        {
            OilCosumption = 8.0;
        }


    }
    class Program
    {
        static void Main(string[] args)
        {
            Car car1 = new Car( );
            car1.Info( );
            Console.ReadKey( );
        }
    }
}
