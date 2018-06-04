using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example4_8
{
    public interface Iname
    {
        string Name { get; set; }
    }
    public interface Ising
    {
        void Sing();
    }
    public interface Idance
    {
        void Dance();
    }  
    class Test : Iname,Ising, Idance
    {
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        public void Sing()
        {
            Console.WriteLine("我会唱歌！");
        }
        public void Dance()
        {
            Console.WriteLine("我能跳舞！");
        }
        public void Myname()
        {
            Console.WriteLine("我的名字叫{0}!", name);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Test t = new Test();
            t.Name = "小明";
            t.Myname();
            t.Sing();
            t.Dance();
            Console.ReadKey();

            }
    }
}

