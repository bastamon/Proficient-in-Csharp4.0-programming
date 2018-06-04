using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HBMISR.Service
{
    /// <summary>
    /// 属性类
    /// 功能：
    /// 1、该类对应干部人选信息采集表表的打印预览
    /// </summary>      
    class Class_HBMainMessage
    {
        private string cid;//该干部的编号

        public string Cid
        {
            get { return cid; }
            set { cid = value; }
        }

        private string hbname;//姓名

        public string Hbname
        {
            get { return hbname; }
            set { hbname = value; }
        }

        private string sex;//性别

        public string Sex
        {
            get { return sex; }
            set { sex = value; }
        }

        private string birthday;//出生年月及岁数

        public string Birthday
        {
            get { return birthday; }
            set { birthday = value; }
        }

        private string nation;//民族

        public string Nation
        {
            get { return nation; }
            set { nation = value; }
        }

        private string native;//籍贯

        public string Native
        {
            get { return native; }
            set { native = value; }
        }

        private string birthplace;//出生地

        public string Birthplace
        {
            get { return birthplace; }
            set { birthplace = value; }
        }

        private string partytime;//入党时间

        public string Partytime
        {
            get { return partytime; }
            set { partytime = value; }
        }

        private string worktime;//参加工作时间

        public string Worktime
        {
            get { return worktime; }
            set { worktime = value; }
        }

        private string health;//健康状况

        public string Health
        {
            get { return health; }
            set { health = value; }
        }

        private string photo;//照片

        public string Photo
        {
            get { return photo; }
            set { photo = value; }
        }

        private string technicalpost;//专业技术职务

        public string Technicalpost
        {
            get { return technicalpost; }
            set { technicalpost = value; }
        }

        private string specialtskill;//熟悉专业有何专长

        public string Specialtskill
        {
            get { return specialtskill; }
            set { specialtskill = value; }
        }

        private string fullteach;//全日制教育学历学位

        public string Fullteach
        {
            get { return fullteach; }
            set { fullteach = value; }
        }

        private string fullGraduateSpecialty;//全日制教育毕业院校、系及专业

        public string FullGraduateSpecialty
        {
            get { return fullGraduateSpecialty; }
            set { fullGraduateSpecialty = value; }
        }

        private string workteach;//在职教育学历学位

        public string Workteach
        {
            get { return workteach; }
            set { workteach = value; }
        }

        private string workGraduateSpecialty;//在职教育毕业院校、系及专业

        public string WorkGraduateSpecialty
        {
            get { return workGraduateSpecialty; }
            set { workGraduateSpecialty = value; }
        }

        private string nowpost;//现任职务

        public string Nowpost
        {
            get { return nowpost; }
            set { nowpost = value; }
        }

        private string knowfiled;//熟悉领域

        public string Knowfiled
        {
            get { return knowfiled; }
            set { knowfiled = value; }
        }

        private string traindirection;//培养方向

        public string Traindirection
        {
            get { return traindirection; }
            set { traindirection = value; }
        }

        private string trainmeasuer;//培养措施

        public string Trainmeasuer
        {
            get { return trainmeasuer; }
            set { trainmeasuer = value; }
        }

        private string resume;//简历

        public string Resume
        {
            get { return resume; }
            set { resume = value; }
        }

        private string rewardsAndpunishment;//奖惩情况

        public string RewardsAndpunishment
        {
            get { return rewardsAndpunishment; }
            set { rewardsAndpunishment = value; }
        }

        private string yearcheck;//年度考核

        public string Yearcheck
        {
            get { return yearcheck; }
            set { yearcheck = value; }
        }

        //在这个打印类中，记录该后备干部的七条家庭成员及重要社会关系
        //第一个家庭成员及重要社会关系
        private string appellation1;//称谓

        public string Appellation1
        {
            get { return appellation1; }
            set { appellation1 = value; }
        }

        private string name1;//姓名

        public string Name1
        {
            get { return name1; }
            set { name1 = value; }
        }

        private string age1;//年龄

        public string Age1
        {
            get { return age1; }
            set
            {
                if (value == "0")
                    age1 = "";
                else
                    age1 = value;
            }
        }

        private string polityface1;//政治面貌

        public string Polityface1
        {
            get { return polityface1; }
            set { polityface1 = value; }
        }

        private string unitjob1;//工作单位及职务

        public string Unitjob1
        {
            get { return unitjob1; }
            set { unitjob1 = value; }
        }

        //第2个家庭成员及重要社会关系
        private string appellation2;//称谓

        public string Appellation2
        {
            get { return appellation2; }
            set { appellation2 = value; }
        }

        private string name2;//姓名

        public string Name2
        {
            get { return name2; }
            set { name2 = value; }
        }

        private string age2;//年龄

        public string Age2
        {
            get { return age2; }
            set {
                if (value == "0")
                    age2 = "";
                else
                    age2 = value;
            }
        }

        private string polityface2;//政治面貌

        public string Polityface2
        {
            get { return polityface2; }
            set { polityface2 = value; }
        }

        private string unitjob2;//工作单位及职务

        public string Unitjob2
        {
            get { return unitjob2; }
            set { unitjob2 = value; }
        }

        //第3个家庭成员及重要社会关系
        private string appellation3;//称谓

        public string Appellation3
        {
            get { return appellation3; }
            set { appellation3 = value; }
        }

        private string name3;//姓名

        public string Name3
        {
            get { return name3; }
            set { name3 = value; }
        }

        private string age3;//年龄

        public string Age3
        {
            get { return age3; }
            set {
                if (value == "0")
                    age3 = "";
                else
                    age3 = value;
            }
        }

        private string polityface3;//政治面貌

        public string Polityface3
        {
            get { return polityface3; }
            set { polityface3 = value; }
        }

        private string unitjob3;//工作单位及职务

        public string Unitjob3
        {
            get { return unitjob3; }
            set { unitjob3 = value; }
        }

        //第4个家庭成员及重要社会关系
        private string appellation4;//称谓

        public string Appellation4
        {
            get { return appellation4; }
            set { appellation4 = value; }
        }

        private string name4;//姓名

        public string Name4
        {
            get { return name4; }
            set { name4 = value; }
        }

        private string age4;//年龄

        public string Age4
        {
            get { return age4; }
            set {
                if (value == "0")
                    age4 = "";
                else
                    age4 = value;
            }
        }

        private string polityface4;//政治面貌

        public string Polityface4
        {
            get { return polityface4; }
            set { polityface4 = value; }
        }

        private string unitjob4;//工作单位及职务

        public string Unitjob4
        {
            get { return unitjob4; }
            set { unitjob4 = value; }
        }

        //第5个家庭成员及重要社会关系
        private string appellation5;//称谓

        public string Appellation5
        {
            get { return appellation5; }
            set { appellation5 = value; }
        }

        private string name5;//姓名

        public string Name5
        {
            get { return name5; }
            set { name5 = value; }
        }

        private string age5;//年龄

        public string Age5
        {
            get { return age5; }
            set {
                if (value == "0")
                    age5 = "";
                else
                    age5 = value;
            }
        }

        private string polityface5;//政治面貌

        public string Polityface5
        {
            get { return polityface5; }
            set { polityface5 = value; }
        }

        private string unitjob5;//工作单位及职务

        public string Unitjob5
        {
            get { return unitjob5; }
            set { unitjob5 = value; }
        }

        //第6个家庭成员及重要社会关系
        private string appellation6;//称谓

        public string Appellation6
        {
            get { return appellation6; }
            set { appellation6 = value; }
        }

        private string name6;//姓名

        public string Name6
        {
            get { return name6; }
            set { name6 = value; }
        }

        private string age6;//年龄

        public string Age6
        {
            get { return age6; }
            set {
                if (value == "0")
                    age6 = "";
                else
                    age6 = value;
            }
        }

        private string polityface6;//政治面貌

        public string Polityface6
        {
            get { return polityface6; }
            set { polityface6 = value; }
        }

        private string unitjob6;//工作单位及职务

        public string Unitjob6
        {
            get { return unitjob6; }
            set { unitjob6 = value; }
        }

        //第7个家庭成员及重要社会关系
        private string appellation7;//称谓

        public string Appellation7
        {
            get { return appellation7; }
            set { appellation7 = value; }
        }

        private string name7;//姓名

        public string Name7
        {
            get { return name7; }
            set { name7 = value; }
        }

        private string age7;//年龄

        public string Age7
        {
            get { return age7; }
            set {
                if (value == "0")
                    age7 = "";
                else
                    age7 = value;
            }
        }

        private string polityface7;//政治面貌

        public string Polityface7
        {
            get { return polityface7; }
            set { polityface7 = value; }
        }

        private string unitjob7;//工作单位及职务

        public string Unitjob7
        {
            get { return unitjob7; }
            set { unitjob7 = value; }
        }
    }
}
