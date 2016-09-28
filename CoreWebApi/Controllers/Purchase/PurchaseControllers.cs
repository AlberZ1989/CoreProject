using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;
using CoreModels.XyCore;
using CoreData.CoreCore;
using System;
namespace CoreWebApi
{
    public class PurchaseController : ControllBase
    {
        [AllowAnonymous]
        [HttpPostAttribute("/Core/Purchase/PurchaseList")]
        public ResponseResult PurchaseList([FromBodyAttribute]JObject co)
        {   
            var cp = new PurchaseParm();
            cp.CoID = int.Parse(GetCoid());
            cp.Purid = co["Purid"].ToString();
            cp.PurdateStart = DateTime.Parse(co["PurdateStart"].ToString());
            cp.PurdateEnd = DateTime.Parse(co["PurdateEnd"].ToString());
            cp.Status = co["Status"].ToString();
            cp.CoName = co["CoName"].ToString();
            cp.SortField = co["SortField"].ToString();
            cp.SortDirection = co["SortDirection"].ToString();
            cp.NumPerPage = int.Parse(co["NumPerPage"].ToString());
            cp.PageIndex = int.Parse(co["PageIndex"].ToString());
            var data = PurchaseHaddle.GetPurchaseList(cp);
            return CoreResult.NewResponse(data.s, data.d, "General"); 
        }

        [AllowAnonymous]
        [HttpPostAttribute("/Core/Purchase/PurchaseDetailList")]
        public ResponseResult PurchaseDetailList([FromBodyAttribute]JObject co)
        {   
            var cp = new PurchaseDetailParm();
            cp.CoID = int.Parse(GetCoid());
            cp.Purid = co["Purid"].ToString();
            cp.Skuid = co["Skuid"].ToString();
            cp.SkuName = co["SkuName"].ToString();
            cp.GoodsCode = co["GoodsCode"].ToString();
            cp.SortField = co["SortField"].ToString();
            cp.SortDirection = co["SortDirection"].ToString();
            cp.NumPerPage = int.Parse(co["NumPerPage"].ToString());
            cp.PageIndex = int.Parse(co["PageIndex"].ToString());
            var data = PurchaseHaddle.GetPurchaseDetailList(cp);
            return CoreResult.NewResponse(data.s, data.d, "General"); 
        }
    }
}