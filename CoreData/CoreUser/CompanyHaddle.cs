using System.Collections.Generic;
//using System.Linq;
using CoreModels;
using CoreModels.XyUser;
using Dapper;
using System;
namespace CoreData.CoreUser
{
    public static class CompanyHaddle
    {
        ///<summary>
        ///查询公司资料
        ///</summary>
        public static DataResult GetCompanyAll(int CoID,string nameFilter, string enable,int pageIndex,int numPerPage)
        {
            var s = 0;
            string wheresql = "";
            bool flag = false;
            if(CoID != 1)
            {
                wheresql = wheresql + "where id = " + CoID;
                flag = true;
            }
            bool enableF = false;
            if(enable != "all")
            {
                if(enable == "true")   
                {
                    enableF = true;
                }
                if(flag == false)
                {
                    wheresql = "where enable =" + enableF;
                }
                else
                {
                    wheresql = wheresql + " and enable =" + enableF;
                }
                flag = true;
            }
            if(nameFilter != "" && nameFilter != null)
            {
               if(flag == false)
                {
                    wheresql = wheresql + "where name like '%"+ nameFilter +"%'";
                }
                else
                {
                    wheresql = wheresql + " and name like '%"+ nameFilter +"%'";
                }
                flag = true;
            }
            wheresql = "select name,enable,address,typelist,remark,creator,createdate from company " + wheresql ;//+ " limit 0,10";
            var u = DbBase.UserDB.Query<Company>(wheresql).AsList();
            int count = u.Count;
            decimal pagecnt = Math.Ceiling(decimal.Parse(count.ToString())/decimal.Parse(numPerPage.ToString()));

            int dataindex = (pageIndex - 1)*numPerPage;
            wheresql = wheresql + "limit " + dataindex.ToString() + " ," + numPerPage.ToString();
            u = DbBase.UserDB.Query<Company>(wheresql).AsList();
            var cp = new CompanyParm();
            cp.datacnt = count;
            cp.pagecnt = pagecnt;
            cp.com = u;
            return new DataResult(s,cp);
        }
        ///<summary>
        ///查询单笔公司资料
        ///</summary>
        public static DataResult GetCompanyEdit(int ID)
        {
            var s = 0;            
            string wheresql = "select name,enable,address,email,typelist,contacts,telphone,mobile,remark from company where id ='" + ID.ToString() + "'" ;//+ " limit 0,10";
            var u = DbBase.UserDB.Query<Company>(wheresql).AsList();
            return new DataResult(s,u);
        }
        ///<summary>
        ///检查公司资料是否已经存在
        ///</summary>
        public static DataResult IsComExist(string name)
        {
            var s = 0;            
            string wheresql = "select name,enable,address,email,typelist,contacts,telphone,mobile,remark from company where name ='" + name + "'" ;//+ " limit 0,10";
            var u = DbBase.UserDB.Query<Company>(wheresql).AsList();
            bool flag = false;
            if(u.Count > 0)
            {
                flag = true;
            }
            return new DataResult(s,flag);
        }
    }

    public class CompanyParm
    {
        public int datacnt {get;set;}
        public decimal pagecnt{get;set;}
        public List<Company> com {get;set;}
    }
}