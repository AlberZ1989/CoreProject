using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CoreWebApi
{
    public class ControllBase : Controller
    {
        #region 账号相关
        ///<summary>
        ///获取用户ID
        ///</summary>
        public string GetUid()
        {
            //return "1";
            return HttpContext.User.Claims.FirstOrDefault(q => q.Type.Equals("uid")).Value; 
        }

        ///<summary>
        ///获取用户名
        ///</summary>
        public string GetUname()
        {
            return "管理员";
            //  return HttpContext.User.Claims.FirstOrDefault(q => q.Type.Equals("uname")).Value;
        }

        ///<summary>
        ///获取公司ID
        ///</summary>
        public string GetCoid()
        {
            return "1";
            // return HttpContext.User.Claims.FirstOrDefault(q => q.Type.Equals("coid")).Value;
        }

        ///<summary>
        ///获取公司ID
        ///</summary>
        public string GetRoleid()
        {
            // return  "1";
            return HttpContext.User.Claims.FirstOrDefault(q => q.Type.Equals("roleid")).Value;
        }

        #endregion

        ///<summary>
        ///判断是否是系统管理员
        ///</summary>
        public bool checkIsAdmin(){            
            return GetRoleid() == "1";
        }

        ///<summary>
        ///json格式判断
        ///</summary>
        public bool isJson(params string[]  jstring){
            bool flag =true;
            object jsonObj;
            try{
                foreach(string s in jstring){
                    if(!string.IsNullOrEmpty(s)){                        
                        jsonObj =  JsonConvert.DeserializeObject(s);
                        //Console.WriteLine(jsonObj);
                        //Console.WriteLine(jsonObj.GetType().FullName);
                        if(jsonObj.GetType().FullName == "Newtonsoft.Json.Linq.JObject"||jsonObj.GetType().FullName == "Newtonsoft.Json.Linq.JArray"){
                            flag =true;
                        }else{
                            flag =false;
                        }
                    }

                }               
            }catch{
                flag = false;               
            }
            return flag;
        }

        ///<summary>
        ///json文件转义，数据库中 /u1234 -> //u1234
        ///</summary>
        public string JsonEscape(string  jstring){
            if(!string.IsNullOrEmpty(jstring)){
                jstring = jstring.Replace("\\u","\\\\u");
            }
            return jstring;
        }  


        public bool checkInt(params int[]  jstring){
             bool flag =true;            
             try{
                 foreach(int s in jstring){
                     if(s<1){
                         flag = false;
                     }
                 }               
             }catch{
                 flag = false;               
             }
             return flag;
          }
 
         public bool checkInt(string strs){
             bool flag =true;            
             try{
                foreach (string item in strs.Split(','))
                 {
                    flag =  checkInt(int.Parse(item));
                 }     
             }catch{
                 flag = false;               
             }
             return flag;

         }

         public bool checkString(params string[]  jstring){
             bool flag =true;
             try{
                 foreach(string s in jstring){
                     if(string.IsNullOrEmpty(s)){
                         flag = false;
                     }else if(s == "undefined"){
                          flag = false;
                     }
                 }               
             }catch{
                 flag = false;               
             }
             return flag;
         }

        public bool checkTel(string tel){            
            return Regex.IsMatch(tel, @"^1[3|4|5|7|8][0-9]\d{8}$");
        }

        public bool checkPhone(string phone){            
            return Regex.IsMatch(phone, @"(\(\d{3,4}\)|\d{3,4}-|\s)?\d{8}");
        }


        #region MD5
        public string GetMD5(string sDataIn, string move)
        {

            using (var md5 = MD5.Create())
            {
                byte[] bytValue, bytHash;
                bytValue = System.Text.Encoding.UTF8.GetBytes(move + sDataIn);
                bytHash = md5.ComputeHash(bytValue);
                md5.Dispose();
                string sTemp = "";
                for (int i = 0; i < bytHash.Length; i++)
                {
                    sTemp += bytHash[i].ToString("x").PadLeft(2, '0');
                }
                return sTemp;
            }
        }
        #endregion
    }
}