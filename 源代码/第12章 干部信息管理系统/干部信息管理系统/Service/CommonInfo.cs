using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HBMISR.Service
{
    /// <summary>
    /// 属性类
    /// 功能：
    /// 配合初步人选名册的数据操作，对应数据表TB_Commoninfo
    /// </summary>
    class CommonInfo
    {
        private string  cid;
        public string Cid
        {
            get { return cid; }
            set { cid = value; }
        }

        private string unitname;
        public string Unitname
        {
            get { return unitname; }
            set { unitname = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string sex;
        public string Sex
        {
            get { return sex; }
            set { sex = value; }
        }

        private string nation;
        public string Nation
        {
            get { return nation; }
            set { nation = value; }
        }

        private string department;
        public string Department
        {
            get { return department; }
            set { department = value; }
        }

        private string position;
        public string Position
        {
            get { return position; }
            set { position = value; }
        }

        private string native;
        public string Native
        {
            get { return native; }
            set { native = value; }
        }

        private string birthplace;
        public string Birthplace
        {
            get { return birthplace; }
            set { birthplace = value; }
        }

        private string birthday;
        public string Birthday
        {
            get { return birthday; }
            set { birthday = value; }
        }

        private Int32 age;
        public Int32 Age
        {
            get { return age; }
            set { age = value; }
        }

        private string partyTime;
        public string PartyTime
        {
            get { return partyTime; }
            set { partyTime = value; }
        }

        private string workTime;
        public string WorkTime
        {
            get { return workTime; }
            set { workTime = value; }
        }

        private string health;
        public string Health
        {
            get { return health; }
            set { health = value; }
        }

        private string technicalPost;
        public string TechnicalPost
        {
            get { return technicalPost; }
            set { technicalPost = value; }
        }

        private string specialtySkill;
        public string SpecialtySkill
        {
            get { return specialtySkill; }
            set { specialtySkill = value; }
        }

        private string fullEducation;
        public string FullEducation
        {
            get { return fullEducation; }
            set { fullEducation = value; }
        }

        private string fullDegree;
        public string FullDegree
        {
            get { return fullDegree; }
            set { fullDegree = value; }
        }

        private string fullSchool;
        public string FullSchool
        {
            get { return fullSchool; }
            set { fullSchool = value; }
        }

        private string fullSpecialty;
        public string FullSpecialty
        {
            get { return fullSpecialty; }
            set { fullSpecialty = value; }
        }

        private string workEducation;
        public string WorkEducation
        {
            get { return workEducation; }
            set { workEducation = value; }
        }

        private string workDegree;
        public string WorkDegree
        {
            get { return workDegree; }
            set { workDegree = value; }
        }

        private string workGraduate;
        public string WorkGraduate
        {
            get { return workGraduate; }
            set { workGraduate = value; }
        }

        private string workSpecialty;
        public string WorkSpecialty
        {
            get { return workSpecialty; }
            set { workSpecialty = value; }
        }

        private string nowPost;
        public string NowPost
        {
            get { return nowPost; }
            set { nowPost = value; }
        }

        private string knowField;
        public string KnowField
        {
            get { return knowField; }
            set { knowField = value; }
        }

        private string trainDirection;
        public string TrainDirection
        {
            get { return trainDirection; }
            set { trainDirection = value; }
        }

        private string trainMeasure;
        public string TrainMeasure
        {
            get { return trainMeasure; }
            set { trainMeasure = value; }
        }

        private string examine;
        public string Examine
        {
            get { return examine; }
            set { examine = value; }
        }

        private string startYear;
        public string StartYear
        {
            get { return startYear; }
            set { startYear = value; }
        }

        private string remark;
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        private string experiencePost;
        public string ExperiencePost
        {
            get { return experiencePost; }
            set { experiencePost = value; }
        }

        private bool  joinTeam = false;
        public bool JoinTeam
        {
            get { return joinTeam; }
            set { joinTeam = value; }
        }

        private string status;
        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        private byte[] photo;
        public byte[] Photo  
        {
            get { return photo; }
            set { photo = value; }
        }

        private bool qd;
        public bool Qd
        {
            get { return qd; }
            set { qd = value; }
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

        private string partyClass;
        public string PartyClass
        {
            get { return partyClass; }
            set { partyClass = value; }
        }

        private Int32 grade;
        public Int32 Grade
        {
            get { return grade; }
            set { grade = value; }
        }

        private string state;
        public string State
        {
            get { return state; }
            set { state = value; }
        }

        private bool isRemainInfo = false;
        public bool IsRemainInfo
        {
            get { return isRemainInfo; }
            set { isRemainInfo = value; }
        }

        private string  unitClass = "";
        public string UnitClass
        {
            get { return unitClass; }
            set { unitClass = value; }
        }

        private string sPDegree = "";
        public string SPDegree
        {
            get { return sPDegree; }
            set { sPDegree = value; }
        }

        private bool isTwoYear = false;
        public bool IsTwoYear
        {
            get { return isTwoYear; }
            set { isTwoYear = value; }
        }

        private bool isGuide = false;
        public bool IsGuide
        {
            get { return isGuide; }
            set { isGuide = value; }
        }

        private string systemTime = "";
        public string SystemTime
        {
            get { return systemTime; }
            set { systemTime = value; }
        }

        private bool publicselect = false;

        public bool Publicselect
        {
            get { return publicselect; }
            set { publicselect = value; }
        }


        private bool TYGE = false;

        public bool TYGE1
        {
            get { return TYGE; }
            set { TYGE = value; }
        }

        private string initialFullSpelling;

        public string InitialFullSpelling
        {
            get { return initialFullSpelling; }
            set { initialFullSpelling = value; }
        }

        private string unitNamePinYin;

        public string UnitNamePinYin
        {
            get { return unitNamePinYin; }
            set { unitNamePinYin = value; }
        }
    }
}
