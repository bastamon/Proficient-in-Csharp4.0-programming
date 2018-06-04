using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example4_5
{
    class Father
    {
        public virtual void FuncA()
        {
            Console.WriteLine("这是基类Father的方法成员FuncA( )");
        }
    }
    class Son : Father
    {
        public override void FuncA()
        {
            Console.WriteLine("这是派生类Son重写的方法成员FuncA( )");
        }
    }
    class Program
    {
        static public void FuncT(Father a)
        {
            a.FuncA( );
        }
        static void Main(string[] args)
        {
            Father f1 = new Father();
            FuncT(f1);
            //Son s1 = new Son();
            //FuncT(ref s1);
            Father f2 = new Son();
            FuncT(f2);
            Console.ReadKey();
        }
    }
}
