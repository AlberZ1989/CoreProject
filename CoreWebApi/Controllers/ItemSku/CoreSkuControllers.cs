using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;
using CoreData.CoreCore;
using CoreModels.XyCore;
using System;
using System.Collections.Generic;
using CoreData.CoreComm;
using CoreData;
using CoreModels;
namespace CoreWebApi.XyCore

{
    // [AllowAnonymous]
    public class CoreSkuControllers : ControllBase
    {

        #region 商品管理-获取商品主要资料
        [HttpGetAttribute("Core/XyCore/CoreSku/GoodsQueryLst")]
        public ResponseResult GoodsQueryLst(string Type, string GoodsCode, string SkuID, string ScoGoodsCode, string KindID, string Enable, string PageIndex, string PageSize, string SortField, string SortDirection)
        {
            var cp = new CoreSkuParam();
            int x;
            // cp.Filter = Filter;
            if (!string.IsNullOrEmpty(Type) && int.TryParse(Type, out x))
            {
                cp.Type = int.Parse(Type);
            }
            if (int.TryParse(KindID, out x))
            {
                cp.KindID = KindID;
            }
            if (!string.IsNullOrEmpty(Enable) && int.TryParse(Enable, out x))
            {
                cp.Enable = Enable;
            }
            if (int.TryParse(PageIndex, out x))
            {
                cp.PageIndex = int.Parse(PageIndex);
            }
            if (int.TryParse(PageSize, out x))
            {
                cp.PageSize = int.Parse(PageSize);
            }
            //排序参数赋值
            if (!string.IsNullOrEmpty(SortField))
            {
                var res = CommHaddle.SysColumnExists(DbBase.CoreConnectString, "coresku", SortField);
                if (res.s == 1)
                {
                    cp.SortField = SortField;
                    if (!string.IsNullOrEmpty(SortDirection) && (SortDirection.ToUpper() == "DESC" || SortDirection.ToUpper() == "ASC"))
                    {
                        cp.SortDirection = SortDirection.ToUpper();
                    }
                }
            }
            // var cp = Newtonsoft.Json.JsonConvert.DeserializeObject<CoreSkuParam>(obj["CoreSkuParam"].ToString());
            cp.CoID = int.Parse(GetCoid());
            cp.GoodsCode = GoodsCode;
            cp.SkuID = SkuID;
            cp.ScoGoodsCode = ScoGoodsCode;
            var Result = CoreSkuHaddle.GetGoodsLst(cp);
            return CoreResult.NewResponse(Result.s, Result.d, "General");
        }
        #endregion
        #region 商品管理 - 获取商品明细列表
        [HttpGetAttribute("Core/XyCore/CoreSku/SkuQueryLst")]
        public ResponseResult SkuQueryLst(string Filter, string SkuName, string SkuSimple, string Norm, string Brand, string ScoGoodsCode, string ScoSku, string ScoID, string PriceS, string PriceT, string Enable, string PageIndex, string PageSize, string SortField, string SortDirection)
        {
            // var cp = Newtonsoft.Json.JsonConvert.DeserializeObject<CoreSkuParam>(obj["CoreSkuParam"].ToString());
            var res = new DataResult(1, null);
            var cp = new CoreSkuParam();
            int x;
            cp.CoID = int.Parse(GetCoid());
            cp.Filter = Filter;
            cp.SkuName = SkuName;
            cp.SkuSimple = SkuSimple;
            cp.Norm = Norm;
            cp.ScoGoodsCode = ScoGoodsCode;
            cp.ScoSku = ScoSku;
            if (!string.IsNullOrEmpty(Brand))
            {
                if (int.TryParse(Brand, out x))
                {
                    cp.Brand = Brand;
                }
                else
                {
                    res.s = -1;
                    res.d = "品牌参数异常";
                }
            }
            if (!string.IsNullOrEmpty(ScoID))
            {
                if (int.TryParse(ScoID, out x))
                {
                    cp.SCoID = ScoID;
                }
                else
                {
                    res.s = -1;
                    res.d = "供应商参数异常";
                }
            }
            if (!string.IsNullOrEmpty(PriceS))
            {
                if (int.TryParse(PriceS, out x))
                {
                    cp.PriceS = PriceS;
                }
                else
                {
                    res.s = -1;
                    res.d = "成本价参数异常";
                }
            }
            if (!string.IsNullOrEmpty(PriceT))
            {
                if (int.TryParse(PriceT, out x))
                {
                    cp.PriceT = PriceT;
                }
                else
                {
                    res.s = -1;
                    res.d = "成本价参数异常";
                }
            }
            if (!string.IsNullOrEmpty(Enable))
            {
                if (int.TryParse(Enable, out x))
                {
                    cp.Enable = Enable;
                }
                // else
                // {
                //     res.s = -1;
                //     res.d = "是否启用参数异常";
                // }
            }
            if (int.TryParse(PageIndex, out x))
            {
                cp.PageIndex = int.Parse(PageIndex);
            }
            if (int.TryParse(PageSize, out x))
            {
                cp.PageSize = int.Parse(PageSize);
            }
            //排序参数赋值
            if (!string.IsNullOrEmpty(SortField))
            {
                res = CommHaddle.SysColumnExists(DbBase.CoreConnectString, "coresku", SortField);
                if (res.s == 1)
                {
                    cp.SortField = SortField;
                    if (!string.IsNullOrEmpty(SortDirection) && (SortDirection.ToUpper() == "DESC" || SortDirection.ToUpper() == "ASC"))
                    {
                        cp.SortDirection = SortDirection.ToUpper();
                    }
                }
            }
            if (res.s == 1)
            {
                res = CoreSkuHaddle.GetSkuLst(cp);
            }

            return CoreResult.NewResponse(res.s, res.d, "General");
        }
        #endregion        

        #region 商品管理 - 获取单笔Sku详情
        [HttpGetAttribute("Core/XyCore/CoreSku/GoodsQuery")]
        public ResponseResult GoodsQuery(string ID)
        {
            var res = new DataResult(1, null);
            int x;
            if (!string.IsNullOrEmpty(ID) && int.TryParse(ID, out x))
            {
                string CoID = GetCoid();
                res = CoreSkuHaddle.GetCoreSkuEdit(ID, CoID);
            }
            else
            {
                res.s = -1;
                res.d = "商品参数异常";
            }
            return CoreResult.NewResponse(res.s, res.d, "General");
        }
        #endregion

        #region Update商品删除标记
        [HttpPostAttribute("Core/XyCore/CoreSku/DeleteGoods")]
        public ResponseResult UptGoodsDel([FromBodyAttribute]JObject obj)
        {
            var res = new DataResult(1, null);
            List<int> IDLst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(obj["IDLst"].ToString());
            if (IDLst.Count > 0)
            {
                string UserName = GetUname();
                int CoID = int.Parse(GetCoid());
                res = CoreSkuHaddle.DelGoods(IDLst, UserName, CoID);
            }
            else
            {
                res.s = -1;
                res.d = "请选择需要删除的商品";
            }
            return CoreResult.NewResponse(res.s, res.d, "General");
        }
        #endregion

        #region Update商品删除标记
        [HttpPostAttribute("Core/XyCore/CoreSku/UptSkuDel")]
        public ResponseResult UptSkuDel([FromBodyAttribute]JObject obj)
        {
            var res = new DataResult(1, null);
            List<int> IDLst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(obj["IDLst"].ToString());
            if (IDLst.Count > 0)
            {
                string UserName = GetUname();
                int CoID = int.Parse(GetCoid());
                res = CoreSkuHaddle.DelSku(IDLst, UserName, CoID);
            }
            else
            {
                res.s = -1;
                res.d = "请选择需要删除的商品";
            }
            var Result = CoreResult.NewResponse(res.s, res.d, "General");
            return Result;
        }
        #endregion


        #region 删除回收站--****该接口暂停使用****
        [HttpPostAttribute("Core/XyCore/CoreSku/DelGoodsRec")]
        public ResponseResult DelGoodsRec([FromBodyAttribute]JObject obj)
        {
            List<string> GoodsLst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(obj["GoodsLst"].ToString());
            // int CoID = 1;
            int CoID = int.Parse(GetCoid());
            var res = CoreSkuHaddle.DelGoodsRec(GoodsLst, CoID);
            var Result = CoreResult.NewResponse(res.s, res.d, "General");
            return Result;
        }
        #endregion

        #region 新增Sku - 商品维护
        [HttpPostAttribute("Core/XyCore/CoreSku/InsertGoods")]
        public ResponseResult InsertGoods([FromBodyAttribute]JObject obj)
        {
            var main = Newtonsoft.Json.JsonConvert.DeserializeObject<Coresku_main>(obj["main"].ToString());
            var itemprops = Newtonsoft.Json.JsonConvert.DeserializeObject<List<goods_item_props>>(obj["itemprops"].ToString());
            var skuprops = Newtonsoft.Json.JsonConvert.DeserializeObject<List<goods_sku_props>>(obj["skuprops"].ToString());
            var items = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CoreSkuItem>>(obj["items"].ToString());
            main.CoID = GetCoid();
            main.Creator = GetUname();
            main.CreateDate = DateTime.Now.ToString();
            var res = CoreSkuHaddle.NewCore(main, itemprops, skuprops, items);
            var Result = CoreResult.NewResponse(res.s, res.d, "General");
            return Result;
        }
        #endregion   

        #region 修改Sku - 商品维护
        [HttpPostAttribute("Core/XyCore/CoreSku/UpdateGoods")]
        public ResponseResult UpdateGoods([FromBodyAttribute]JObject obj)
        {
            var main = Newtonsoft.Json.JsonConvert.DeserializeObject<Coresku_main>(obj["main"].ToString());
            var itemprops = Newtonsoft.Json.JsonConvert.DeserializeObject<List<goods_item_props>>(obj["itemprops"].ToString());
            var skuprops = Newtonsoft.Json.JsonConvert.DeserializeObject<List<goods_sku_props>>(obj["skuprops"].ToString());
            var items = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CoreSkuItem>>(obj["items"].ToString());
            string CoID = GetCoid();
            string UserName = GetUname();
            string CreateDate = DateTime.Now.ToString();
            main.CoID = CoID;
            var res = CoreSkuHaddle.EditCore(main, itemprops, skuprops, items, CoID, UserName);
            return CoreResult.NewResponse(res.s, res.d, "General");
        }
        #endregion

        #region 商品维护——0.停用|1.启用|2.备用
        [HttpPostAttribute("Core/XyCore/CoreSku/UpdateGoodsEnable")]
        public ResponseResult UpdateGoodsEnable([FromBodyAttribute]JObject obj)
        {
            var res = new DataResult(1, null);
            int Enable = 0;
            var IDLst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(obj["IDLst"].ToString());
            if (IDLst.Count == 0)
            {
                res.s = -1;
                res.d = "请先选中操作明细";
            }
            else if (!int.TryParse(obj["Enable"].ToString(), out Enable))
            {
                res.s = -1;
                res.d = "无效参数Enable";
            }
            else
            {
                string CoID = GetCoid();
                string UserName = GetUname();
                Enable = int.Parse(obj["Enable"].ToString());
                res = CoreSkuHaddle.UptGoodsEnable(IDLst, CoID, UserName, Enable);
            }
            return CoreResult.NewResponse(res.s, res.d, "General");
        }
        #endregion

        #region 采购查询
        [HttpPostAttribute("Core/XyCore/CoreSku/SkuQuery")]
        public ResponseResult SkuQuery([FromBodyAttribute]JObject obj)
        {
            var cp = Newtonsoft.Json.JsonConvert.DeserializeObject<SkuParam>(obj["SkuParam"].ToString());
            int CoID = int.Parse(GetCoid());
            var res = CoreSkuHaddle.GetSkuAll(cp, CoID, 2);
            var Result = CoreResult.NewResponse(res.s, res.d, "General");
            return Result;
        }

        #endregion

        #region 商品吊牌打印 - 货号查询
        [HttpGetAttribute("Core/XyCore/CoreSku/GetPrintGoodsCode")]
        public ResponseResult GetPrintGoodsCode(string GoodsCode, string ScoGoodsCode, string SkuID)
        {
            var res = new DataResult(1, null);
            if (string.IsNullOrEmpty(GoodsCode) && string.IsNullOrEmpty(ScoGoodsCode) && string.IsNullOrEmpty(SkuID))
            {
                res.s = -1;
                res.d = "供销商货号与商家货号至少填写一个！";
            }
            else
            {
                var cp = new SkuPrintParam();
                cp.CoID = GetCoid();
                cp.GoodsCode = GoodsCode;
                cp.ScoGoodsCode = ScoGoodsCode;
                cp.SkuID = SkuID;
                res = CoreSkuHaddle.PrintGoodsCode(cp);
            }
            return CoreResult.NewResponse(res.s, res.d, "General");
        }
        #endregion
        #region 商品吊牌打印 - 根据货号带出Sku属性
        [HttpGetAttribute("Core/XyCore/CoreSku/GetPrintSkuProps")]
        public ResponseResult GetPrintSkuProps(string ID)
        {
            var res = new DataResult(1, null);
            int x;
            if (string.IsNullOrEmpty(ID) || !string.IsNullOrEmpty(ID) && !int.TryParse(ID, out x))
            {
                res.s = -1;
                res.d = "无效参数";
            }
            else
            {
                string CoID = GetCoid();
                res = CoreSkuHaddle.PrintSkuProps(ID, CoID);
            }
            return CoreResult.NewResponse(res.s, res.d, "General");
        }
        #endregion

        #region 商品吊牌打印 - 根据货号&Sku属性,带出商品明细
        [HttpPostAttribute("Core/XyCore/CoreSku/PrintSkuItemQuery")]
        public ResponseResult PrintSkuItemQuery([FromBodyAttribute]JObject obj)
        {
            var res = new DataResult(1, null);
            int x;
            if (!(obj["ID"] != null && int.TryParse(obj["ID"].ToString(), out x) && obj["ValIDLst"] != null))
            {
                res.s = -1;
                res.d = "无效参数";
            }
            else
            {
                var cp = new SkuPrintParam();
                cp.CoID = GetCoid();
                cp.ID = obj["ID"].ToString();
                cp.ValIDLst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(obj["ValIDLst"].ToString());
                res = CoreSkuHaddle.PrintSkuItem(cp);
            }
            return CoreResult.NewResponse(res.s, res.d, "General");
        }
        #endregion
    }

}