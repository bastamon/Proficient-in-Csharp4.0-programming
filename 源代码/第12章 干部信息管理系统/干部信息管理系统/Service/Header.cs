using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HBMISR.Service
{
    public class Header
    {
        private Int32 hid;

        public Int32 Hid  //主键
        {
            get { return hid; }
            set { hid = value; }
        }

        private Int32 num;

        public Int32 Num  //票数的个数
        {
            get { return num; }
            set { num = value; }
        }

        //总人数
        private Int32 totalpeople;  
        public Int32 Totalpeople
        {
            get { return totalpeople; }
            set { totalpeople = value; }
        }

        //收回的票数
        private Int32 backpell;
        public Int32 Backpell
        {
            get { return backpell; }
            set { backpell = value; }
        }

        //有效票数
        private Int32 validpoll;
        public Int32 Validpoll
        {
            get { return validpoll; }
            set { validpoll = value; }
        }
        //A的总票数
        private Int32 totalA;
        public Int32 TotalA
        {
            get { return totalA; }
            set { totalA = value; }
        }

        //B的总票数
        private Int32 totalB;
        public Int32 TotalB
        {
            get { return totalB; }
            set { totalB = value; }
        }

        //C票数
        private Int32 totalC;
        public Int32 TotalC
        {
            get { return totalC; }
            set { totalC = value; }
        }

        //D票数
        private Int32 totalD;
        public Int32 TotalD
        {
            get { return totalD; }
            set { totalD = value; }
        }



        private bool recommend;

        public bool Recommend
        {
            get { return recommend; }
            set { recommend = value; }
        }
        private bool qd;

        public bool Qd     //正副
        {
            get { return qd; }
            set { qd = value; }
        }

        private string date;

        public string Date
        {
            get { return date; }
            set { date = value; }
        }





        private string TypeA;  //A票的意义

        public string TypeA1
        {
            get { return TypeA; }
            set { TypeA = value; }
        }

        private string TypeB;  //B票的意义

        public string TypeB1
        {
            get { return TypeB; }
            set { TypeB = value; }
        }

        private string TypeC;   //C票的意义

        public string TypeC1
        {
            get { return TypeC; }
            set { TypeC = value; }
        }

        private string TypeD;    //D票的意义 

        public string TypeD1
        {
            get { return TypeD; }
            set { TypeD = value; }
        }

        private string remark;

        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
        
    }
}
