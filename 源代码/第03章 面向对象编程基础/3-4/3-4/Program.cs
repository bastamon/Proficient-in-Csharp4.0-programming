using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3_4
{
    public class Car
    {
        public double OilMeter = 0;//油表数值，单位：升
        private double OilVolume;//油箱容量，单位：升
        public double OilCosumption;//油耗, 每百公里消耗汽油数量，单位：升
        public double Mileage;//里程数，单位：公里
        public Car( )
        {
            OilMeter = 0;
            OilVolume = 50;
            OilCosumption = 9;
            Mileage = 0;
        }
        public void Info()
        {
            Console.WriteLine("这辆车已经行驶 {0} 公里，油箱中还有 {1} 升汽油，油耗为百公里 {2} 升。", Mileage, OilMeter, OilCosumption);
        }
        public void Fule(int x)//加油
        {
            if((OilMeter+x) <= OilVolume)
            {
                OilMeter = OilMeter+x;
                Console.WriteLine("向油箱中加入 {0} 升汽油， 油箱中现有 {1} 升汽油" ,x ,OilMeter);
            }
            else
            {
                Console.WriteLine("油箱已满，共加入 {0} 升汽油",OilVolume - OilMeter);
                OilMeter = OilVolume;
            }
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            Car car1 = new Car( );
            car1.Info();
            car1.Fule(10);
            car1.Fule(100);
            car1.Info();
            Console.ReadKey();
        }
    }
}
