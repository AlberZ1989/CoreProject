using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;
using CoreData.CoreComm;
using CoreModels.XyComm;
using System.Collections.Generic;
using CoreModels;

namespace CoreWebApi.Base
{
    // [AllowAnonymous]
    public class ShopController:ControllBase
    {
        //查询多笔店铺资料
        [HttpPostAttribute("/Core/Shop/ShopQueryLst")]
        public ResponseResult ShopQueryLst([FromBodyAttribute]JObject obj)
        {
            
            var cp = Newtonsoft.Json.JsonConvert.DeserializeObject<ShopParam>(obj.ToString());
            cp.CoID = int.Parse(GetCoid());
            var res = ShopHaddle.GetShopAll(cp);
            var Result = CoreResult.NewResponse(res.s,res.d,"Basic");
            return Result;             
        }

        //查询单笔店铺资料
        [HttpPostAttribute("/Core/Shop/ShopQuery")]
        public ResponseResult ShopQuery([FromBodyAttribute]JObject obj)
        {
            var CoID = GetCoid();
        //    var CoID = "1";
            string shopid = obj["ShopID"].ToString();
            var res = ShopHaddle.ShopQuery(CoID,shopid);
            return CoreResult.NewResponse(res.s,res.d,"General");
        }

        //修改店铺状态（启用|停用）
        [HttpPostAttribute("/Core/Shop/ShopEnable")]
        public ResponseResult ShopEnable([FromBodyAttribute]JObject obj)
        {    
            var IDLst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(obj["IDLst"].ToString());            
            string UserName = GetUname(); 
            bool Enable = obj["Enable"].ToString().ToUpper()=="TRUE"?true:false;
            string Coid = GetCoid();
            
            var res = ShopHaddle.UptShopEnable(IDLst,UserName,Enable,Coid);
            return CoreResult.NewResponse(res.s,res.d,"General");
        }


        //保存店铺资料
        [HttpPostAttribute("/Core/Shop/createshop")]
         public ResponseResult createshop([FromBodyAttribute]JObject obj)
        {                         
            var shop = Newtonsoft.Json.JsonConvert.DeserializeObject<Shop>(obj.ToString());
            var Res = new DataResult(1,null);
            if(!string.IsNullOrEmpty(shop.TelPhone)){
                if(!checkTel(shop.TelPhone)){
                    Res.s = -3006;
                }
            }
            if(!string.IsNullOrEmpty(shop.ReturnPhone)){
                if(!checkPhone(shop.ReturnPhone)){
                    Res.s = -3007;
                }
            }           
            
            int CoID = int.Parse(GetCoid());
            string UserName = GetUname();
            if(Res.s == 1)  
                Res = ShopHaddle.InsertShop(shop, CoID, UserName);      
            
            return CoreResult.NewResponse(Res.s,Res.d,"General");
            //return CoreResult.NewResponse(1,shop,"General");
        }

        [HttpPostAttribute("/Core/Shop/modifyshop")]
         public ResponseResult modifyshop([FromBodyAttribute]JObject obj)
        {
            var shop = Newtonsoft.Json.JsonConvert.DeserializeObject<Shop>(obj.ToString());
            var Res = new DataResult(1,null);
            if(!string.IsNullOrEmpty(shop.TelPhone)){
                if(!checkTel(shop.TelPhone)){
                    Res.s = -3006;
                }
            }
            if(!string.IsNullOrEmpty(shop.ReturnPhone)){
                if(!checkPhone(shop.ReturnPhone)){
                    Res.s = -3007;
                }
            }    
            int CoID = int.Parse(GetCoid());
            string UserName = GetUname();            
            if(Res.s == 1)
                Res = ShopHaddle.UpdateShop(shop, CoID, UserName);            
            return CoreResult.NewResponse(Res.s,Res.d,"General");
        }

        
        //判断店铺名称是否已经存在    
        [HttpPostAttribute("/Core/Shop/ShopIsExist")]
         public ResponseResult ShopIsExist([FromBodyAttribute]JObject obj)
         {
             string ShopName = obj["ShopName"].ToString();
             int ID = int.Parse(obj["ID"].ToString());
             int CoID = int.Parse(GetCoid());
             var isexist = ShopHaddle.ExistShop(ShopName, ID, CoID);
             return CoreResult.NewResponse(1,isexist.ToString(),"General");
         }

         //获取所有授权店铺
         [HttpPostAttribute("/Core/Shop/TokenShopLst")]
         public ResponseResult TokenShopLst()
         {
             int CoID = int.Parse(GetCoid());
             var res = ShopHaddle.GetTokenShopLst(CoID);
             return CoreResult.NewResponse(res.s,res.d,"General");
         }

         //获取所有线下店铺
         [HttpPostAttribute("/Core/Shop/OfflineShopLst")]
         public ResponseResult OfflineShopLst()
         {
             int CoID = int.Parse(GetCoid());
             var res = ShopHaddle.GetOfflineShopLst(CoID);
             return CoreResult.NewResponse(res.s,res.d,"General");
         }

         [HttpPostAttribute("/Core/Shop/TokenExpired")]
         public ResponseResult TokenExpired([FromBodyAttribute]JObject obj)
         {
             string shopid = obj["shopid"].ToString();
             string coid = GetCoid();
             var res = ShopHaddle.TokenExpired(shopid,coid);
             return CoreResult.NewResponse(res.s,res.d,"General");
         }

         //修改店铺状态（启用|停用）
        [HttpPostAttribute("/Core/Shop/apienable")]
        public ResponseResult apienable([FromBodyAttribute]JObject obj)
        {    
            var shopapi = Newtonsoft.Json.JsonConvert.DeserializeObject<shopApi>(obj.ToString());            
            string UserName = GetUname();            
            string Coid = GetCoid();
            
            var res = ShopHaddle.uptApiEnable(shopapi,Coid);
            return CoreResult.NewResponse(res.s,res.d,"Api");
        }

        [HttpPostAttribute("/Core/Shop/delete")]
        public ResponseResult delete([FromBodyAttribute]JObject obj)
        {    
            var shopapi = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(obj["IDLst"].ToString());                      
            string Coid = GetCoid();
            
            var res = ShopHaddle.DelShop(shopapi,Coid);
            return CoreResult.NewResponse(res.s,res.d,"Api");
        }

        [HttpPostAttribute("/Core/Shop/setShopPrint")]
        public ResponseResult setShopPrint([FromBodyAttribute]JObject obj)
        {    
            var shopPrint = Newtonsoft.Json.JsonConvert.DeserializeObject<List<shopPrintUpdate>>(obj["shopArr"].ToString());                      
            string Coid = GetCoid();
            
            var res = ShopHaddle.setShopPrint(shopPrint,Coid);
            return CoreResult.NewResponse(res.s,res.d,"Api");
        }





    }
}