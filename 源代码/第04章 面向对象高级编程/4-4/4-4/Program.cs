using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example4_4
{
    class Father
    {
        protected void fnc()
        { }
        public virtual void FuncA( )
        {
            Console.WriteLine("这是基类Father的方法成员FuncA( )!");
        }
    }
    class Son : Father
    {
         public override void FuncA( )
        {
            
             base.FuncA();
            Console.WriteLine("这是派生类Son重写的方法成员FuncA( )!");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Father f1 = new Father();
            f1.FuncA();
            Son s1 = new Son();
            s1.FuncA( );
            Father f2 = new Son();
            f2.FuncA();
            Console.ReadKey();
        }
    }
}
