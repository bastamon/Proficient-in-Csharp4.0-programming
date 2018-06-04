using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HBMISR.Service
{
    /// <summary>
    /// 属性类 
    /// 功能： 
    /// 1、配合简要情况登记的数据操作，对应数据表TB_Commoninfo。
    /// </summary>
    class BriefRigist
    {
        private Int32 age;

        public Int32 Age
        {
            get { return age; }
            set { age = value; }
        }
        private string nowPost;

        public string NowPost
        {
            get { return nowPost; }
            set { nowPost = value; }
        }
        private string specialtySkill;

        public string SpecialtySkill
        {
            get { return specialtySkill; }
            set { specialtySkill = value; }
        }
        private string health;

        public string Health
        {
            get { return health; }
            set { health = value; }
        }
       

        private byte[] image;

        public byte[] Image
        {
            get { return image; }
            set { image = value; }
        }
        private string remark;

        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        } 
        private string startYear;

        public string StartYear
        {
            get { return startYear; }
            set { startYear = value; }
        }
        private string result1;


        public string Result1
        {
            get { return result1; }
            set { result1 = value; }
        }
        private string result2;


        public string Result2
        {
            get { return result2; }
            set { result2 = value; }
        }

        private string result3;

        public string Result3
        {
            get { return result3; }
            set { result3 = value; }
        }


    }
}
