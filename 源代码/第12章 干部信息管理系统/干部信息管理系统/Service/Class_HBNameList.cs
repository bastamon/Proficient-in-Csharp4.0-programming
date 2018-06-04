using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HBMISR.Service
{
    
    class Class_HBNameList
    {        

        private string pagecount;//用来记录表格页数

        public string Pagecount
        {
            get { return pagecount; }
            set { pagecount = value; }
        }


       
        //第1个后备干部
        private string hbname1;//姓名，性别，民族

        public string Hbname1
        {
            get { return hbname1; }
            set { hbname1 = value; }
        }

        private string unitjop1;//工作单位及职务

        public string Unitjop1
        {
            get { return unitjop1; }
            set { unitjop1 = value; }
        }

        private string native1;//籍贯

        public string Native1
        {
            get { return native1; }
            set { native1 = value; }
        }

        private string birthday1;//出生年月

        public string Birthday1
        {
            get { return birthday1; }
            set { birthday1 = value; }
        }

        private string partyTime1;//入党时间

        public string PartyTime1
        {
            get { return partyTime1; }
            set { partyTime1 = value; }
        }

        private string workTime1;//参加工作时间

        public string WorkTime1
        {
            get { return workTime1; }
            set { workTime1 = value; }
        }

        private string fullED1;//全日制学历、学位

        public string FullED1
        {
            get { return fullED1; }
            set { fullED1 = value; }
        }

        private string fullSS1;//全日制毕业院校、专业

        public string FullSS1
        {
            get { return fullSS1; }
            set { fullSS1 = value; }
        }

        private string workED1;//在职学历、学位

        public string WorkED1
        {
            get { return workED1; }
            set { workED1 = value; }
        }

        private string workSS1;//在职毕业院校、专业

        public string WorkSS1
        {
            get { return workSS1; }
            set { workSS1 = value; }
        }

        private string technicalPost1;//专业技术职务

        public string TechnicalPost1
        {
            get { return technicalPost1; }
            set { technicalPost1 = value; }
        }

        private string experiencePost1;//历任主要职务

        public string ExperiencePost1
        {
            get { return experiencePost1; }
            set { experiencePost1 = value; }
        }

        private string knowField1;//熟悉领域

        public string KnowField1
        {
            get { return knowField1; }
            set { knowField1 = value; }
        }

        private string trainDirection1;//培养方向

        public string TrainDirection1
        {
            get { return trainDirection1; }
            set { trainDirection1 = value; }
        }

        private string trainMeasure1;//培养措施

        public string TrainMeasure1
        {
            get { return trainMeasure1; }
            set { trainMeasure1 = value; }
        }

        


        ////第2个后备干部
        //private string hbname2;//姓名，性别，民族

        //public string Hbname2
        //{
        //    get { return hbname2; }
        //    set { hbname2 = value; }
        //}

        //private string unitjop2;//工作单位及职务

        //public string Unitjop2
        //{
        //    get { return unitjop2; }
        //    set { unitjop2 = value; }
        //}

        //private string native2;//籍贯

        //public string Native2
        //{
        //    get { return native2; }
        //    set { native2 = value; }
        //}

        //private string birthday2;//出生年月

        //public string Birthday2
        //{
        //    get { return birthday2; }
        //    set { birthday2 = value; }
        //}

        //private string partyTime2;//入党时间

        //public string PartyTime2
        //{
        //    get { return partyTime2; }
        //    set { partyTime2 = value; }
        //}

        //private string workTime2;//参加工作时间

        //public string WorkTime2
        //{
        //    get { return workTime2; }
        //    set { workTime2 = value; }
        //}

        //private string fullED2;//全日制学历、学位

        //public string FullED2
        //{
        //    get { return fullED2; }
        //    set { fullED2 = value; }
        //}

        //private string fullSS2;//全日制毕业院校、专业

        //public string FullSS2
        //{
        //    get { return fullSS2; }
        //    set { fullSS2 = value; }
        //}

        //private string workED2;//在职学历、学位

        //public string WorkED2
        //{
        //    get { return workED2; }
        //    set { workED2 = value; }
        //}

        //private string workSS2;//在职毕业院校、专业

        //public string WorkSS2
        //{
        //    get { return workSS2; }
        //    set { workSS2 = value; }
        //}

        //private string technicalPost2;//专业技术职务

        //public string TechnicalPost2
        //{
        //    get { return technicalPost2; }
        //    set { technicalPost2 = value; }
        //}

        //private string experiencePost2;//历任主要职务

        //public string ExperiencePost2
        //{
        //    get { return experiencePost2; }
        //    set { experiencePost2 = value; }
        //}

        //private string knowField2;//熟悉领域

        //public string KnowField2
        //{
        //    get { return knowField2; }
        //    set { knowField2 = value; }
        //}

        //private string trainDirection2;//培养方向

        //public string TrainDirection2
        //{
        //    get { return trainDirection2; }
        //    set { trainDirection2 = value; }
        //}

        //private string trainMeasure2;//培养措施

        //public string TrainMeasure2
        //{
        //    get { return trainMeasure2; }
        //    set { trainMeasure2 = value; }
        //}

        
       
        
        ////第3个后备干部
        //private string hbname3;//姓名，性别，民族

        //public string Hbname3
        //{
        //    get { return hbname3; }
        //    set { hbname3 = value; }
        //}

        //private string unitjop3;//工作单位及职务

        //public string Unitjop3
        //{
        //    get { return unitjop3; }
        //    set { unitjop3 = value; }
        //}

        //private string native3;//籍贯

        //public string Native3
        //{
        //    get { return native3; }
        //    set { native3 = value; }
        //}

        //private string birthday3;//出生年月

        //public string Birthday3
        //{
        //    get { return birthday3; }
        //    set { birthday3 = value; }
        //}

        //private string partyTime3;//入党时间

        //public string PartyTime3
        //{
        //    get { return partyTime3; }
        //    set { partyTime3 = value; }
        //}

        //private string workTime3;//参加工作时间

        //public string WorkTime3
        //{
        //    get { return workTime3; }
        //    set { workTime3 = value; }
        //}

        //private string fullED3;//全日制学历、学位

        //public string FullED3
        //{
        //    get { return fullED3; }
        //    set { fullED3 = value; }
        //}

        //private string fullSS3;//全日制毕业院校、专业

        //public string FullSS3
        //{
        //    get { return fullSS3; }
        //    set { fullSS3 = value; }
        //}

        //private string workED3;//在职学历、学位

        //public string WorkED3
        //{
        //    get { return workED3; }
        //    set { workED3 = value; }
        //}

        //private string workSS3;//在职毕业院校、专业

        //public string WorkSS3
        //{
        //    get { return workSS3; }
        //    set { workSS3 = value; }
        //}

        //private string technicalPost3;//专业技术职务

        //public string TechnicalPost3
        //{
        //    get { return technicalPost3; }
        //    set { technicalPost3 = value; }
        //}

        //private string experiencePost3;//历任主要职务

        //public string ExperiencePost3
        //{
        //    get { return experiencePost3; }
        //    set { experiencePost3 = value; }
        //}

        //private string knowField3;//熟悉领域

        //public string KnowField3
        //{
        //    get { return knowField3; }
        //    set { knowField3 = value; }
        //}

        //private string trainDirection3;//培养方向

        //public string TrainDirection3
        //{
        //    get { return trainDirection3; }
        //    set { trainDirection3 = value; }
        //}

        //private string trainMeasure3;//培养措施

        //public string TrainMeasure3
        //{
        //    get { return trainMeasure3; }
        //    set { trainMeasure3 = value; }
        //}

        
        
        
        ////第4个后备干部
        //private string hbname4;//姓名，性别，民族

        //public string Hbname4
        //{
        //    get { return hbname4; }
        //    set { hbname4 = value; }
        //}

        //private string unitjop4;//工作单位及职务

        //public string Unitjop4
        //{
        //    get { return unitjop4; }
        //    set { unitjop4 = value; }
        //}

        //private string native4;//籍贯

        //public string Native4
        //{
        //    get { return native4; }
        //    set { native4 = value; }
        //}

        //private string birthday4;//出生年月

        //public string Birthday4
        //{
        //    get { return birthday4; }
        //    set { birthday4 = value; }
        //}

        //private string partyTime4;//入党时间

        //public string PartyTime4
        //{
        //    get { return partyTime4; }
        //    set { partyTime4 = value; }
        //}

        //private string workTime4;//参加工作时间

        //public string WorkTime4
        //{
        //    get { return workTime4; }
        //    set { workTime4 = value; }
        //}

        //private string fullED4;//全日制学历、学位

        //public string FullED4
        //{
        //    get { return fullED4; }
        //    set { fullED4 = value; }
        //}

        //private string fullSS4;//全日制毕业院校、专业

        //public string FullSS4
        //{
        //    get { return fullSS4; }
        //    set { fullSS4 = value; }
        //}

        //private string workED4;//在职学历、学位

        //public string WorkED4
        //{
        //    get { return workED4; }
        //    set { workED4 = value; }
        //}

        //private string workSS4;//在职毕业院校、专业

        //public string WorkSS4
        //{
        //    get { return workSS4; }
        //    set { workSS4 = value; }
        //}

        //private string technicalPost4;//专业技术职务

        //public string TechnicalPost4
        //{
        //    get { return technicalPost4; }
        //    set { technicalPost4 = value; }
        //}

        //private string experiencePost4;//历任主要职务

        //public string ExperiencePost4
        //{
        //    get { return experiencePost4; }
        //    set { experiencePost4 = value; }
        //}

        //private string knowField4;//熟悉领域

        //public string KnowField4
        //{
        //    get { return knowField4; }
        //    set { knowField4 = value; }
        //}

        //private string trainDirection4;//培养方向

        //public string TrainDirection4
        //{
        //    get { return trainDirection4; }
        //    set { trainDirection4 = value; }
        //}

        //private string trainMeasure4;//培养措施

        //public string TrainMeasure4
        //{
        //    get { return trainMeasure4; }
        //    set { trainMeasure4 = value; }
        //}

        
        
        
        ////第5个后备干部
        //private string hbname5;//姓名，性别，民族

        //public string Hbname5
        //{
        //    get { return hbname5; }
        //    set { hbname5 = value; }
        //}

        //private string unitjop5;//工作单位及职务

        //public string Unitjop5
        //{
        //    get { return unitjop5; }
        //    set { unitjop5 = value; }
        //}

        //private string native5;//籍贯

        //public string Native5
        //{
        //    get { return native5; }
        //    set { native5 = value; }
        //}

        //private string birthday5;//出生年月

        //public string Birthday5
        //{
        //    get { return birthday5; }
        //    set { birthday5 = value; }
        //}

        //private string partyTime5;//入党时间

        //public string PartyTime5
        //{
        //    get { return partyTime5; }
        //    set { partyTime5 = value; }
        //}

        //private string workTime5;//参加工作时间

        //public string WorkTime5
        //{
        //    get { return workTime5; }
        //    set { workTime5 = value; }
        //}

        //private string fullED5;//全日制学历、学位

        //public string FullED5
        //{
        //    get { return fullED5; }
        //    set { fullED5 = value; }
        //}

        //private string fullSS5;//全日制毕业院校、专业

        //public string FullSS5
        //{
        //    get { return fullSS5; }
        //    set { fullSS5 = value; }
        //}

        //private string workED5;//在职学历、学位

        //public string WorkED5
        //{
        //    get { return workED5; }
        //    set { workED5 = value; }
        //}

        //private string workSS5;//在职毕业院校、专业

        //public string WorkSS5
        //{
        //    get { return workSS5; }
        //    set { workSS5 = value; }
        //}

        //private string technicalPost5;//专业技术职务

        //public string TechnicalPost5
        //{
        //    get { return technicalPost5; }
        //    set { technicalPost5 = value; }
        //}

        //private string experiencePost5;//历任主要职务

        //public string ExperiencePost5
        //{
        //    get { return experiencePost5; }
        //    set { experiencePost5 = value; }
        //}

        //private string knowField5;//熟悉领域

        //public string KnowField5
        //{
        //    get { return knowField5; }
        //    set { knowField5 = value; }
        //}

        //private string trainDirection5;//培养方向

        //public string TrainDirection5
        //{
        //    get { return trainDirection5; }
        //    set { trainDirection5 = value; }
        //}

        //private string trainMeasure5;//培养措施

        //public string TrainMeasure5
        //{
        //    get { return trainMeasure5; }
        //    set { trainMeasure5 = value; }
        //}
          
    }
}
