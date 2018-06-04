using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Example4_11
{
    [Serializable]
    public class Car
    {
        public double OilMeter;//油表数值，单位：升
        public double OilVolume;//油箱容量，单位：升
        public double OilCosumption;//油耗, 每百公里消耗汽油数量，单位：升
        public double Mileage;//里程数，单位：公里
        public void Info()
        {
            Console.WriteLine("这辆车已经行驶 {0} 公里，油箱容积 {1} 升，油箱中还有 {2} 升汽油，油耗为百公里{3} 升。", Mileage,OilVolume,OilMeter, OilCosumption);
        }       
    }
    class Program
    {
        static void Main(string[] args)
        {
            Car car1 = new Car();
            car1.OilMeter = 10;
            car1.OilVolume = 50;
            car1.OilCosumption = 8.5;
            car1.Mileage = 0;
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("MyFile.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, car1);
            stream.Close();

            stream = new FileStream("MyFile.bin", FileMode.Open, FileAccess.Read, FileShare.None);
            Car car2 = (Car)formatter.Deserialize(stream);
            stream.Close();

            car2.Info();
            Console.ReadKey();
        }
    }
}
