using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace HBMISR.Data
{
    sealed class SQLPool
    {
        //互斥锁 
        public static Mutex mutexSQLPool = new Mutex();

        //SQL池 
        Stack<string> pool;

        /// <summary> 
        /// 初始化SQL池 
        /// </summary> 
        internal SQLPool()
        {
            this.pool = new Stack<string>();
        }

        /// <summary> 
        /// 获取SQL池数量 
        /// </summary> 
        internal Int32 Count
        {
            get { return this.pool.Count; }
        }

        /// <summary> 
        /// 从池中取出SQL
        /// </summary> 
        /// <returns></returns>
        internal string Pop()
        {
            lock (this.pool)
            {
                return this.pool.Pop();
            }
        }

        /// <summary> 
        /// 增加一个SQL到池中 
        /// </summary> 
        /// <param name="item"></param> 
        internal void Push(string item)
        {
            if (item.Trim() == "")
            {
                throw new ArgumentNullException("Items added to a SQLPool cannot be null");
            }

            //此处向SQL池中push SQL必须与Clear互斥 
            mutexSQLPool.WaitOne();
            try
            {
                this.pool.Push(item);    //此处如果出错，则不会执行ReleaseMutex，将会死锁 
            }
            catch { }

            mutexSQLPool.ReleaseMutex();
        }

        /// <summary>
        /// 清空SQL池 
        /// 清空前，返回SQL池中所有SQL语句
        /// </summary> 
        internal string[] Clear()
        {
            string[] array = new string[] { };

            //此处必须与Push互斥
            mutexSQLPool.WaitOne();
            try
            {
                array = this.pool.ToArray();     //此处如果出错，则不会执行ReleaseMutex，将会死锁 
                this.pool.Clear();
            }
            catch { }

            mutexSQLPool.ReleaseMutex();

            return array;
        }
    }
}