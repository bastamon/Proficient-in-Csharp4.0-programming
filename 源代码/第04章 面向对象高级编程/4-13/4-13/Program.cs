using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example4_13
{
    public class Sort<T> where T : IComparable
    {
        public void MpSort(T[] a)
        {
            int i, j, n;
            n = a.Length;
            T temp;
            for (i = 1; i < n; i++)
                for (j = 0; j <= n - i - 1; j++)
                    if (a[j].CompareTo(a[j + 1])>0)
                    {
                        temp = a[j]; a[j] = a[j + 1]; a[j + 1] = temp;
                    }
        }
    }
    class Program
    {     
        static void Main(string[] args)
        {
            Sort<int> st = new Sort<int>( );
            int[] b = {2,1,5,3,1};
            st.MpSort(b);
           foreach(int x in b)
               Console.Write("  {0}",x);
            Console.ReadKey();
        }
    }
}
