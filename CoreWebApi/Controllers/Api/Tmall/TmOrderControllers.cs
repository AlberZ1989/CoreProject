using System;
using CoreDate.CoreApi;
using CoreModels;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebApi.Api.Tmall{    
    public class TmOrderControllers : ControllBase
    {
        //订单需要返回的字段列表
        public static string OREDER_FIELDS = "seller_nick,buyer_nick,title,type,created,sid,tid,seller_rate,buyer_rate,status,payment,discount_fee,adjust_fee,post_fee,total_fee,pay_time,end_time,modified,consign_time,buyer_obtain_point_fee,point_fee,real_point_fee,received_payment,commission_fee,pic_path,num_iid,num_iid,num,price,cod_fee,cod_status,shipping_type,receiver_name,receiver_state,receiver_city,receiver_district,receiver_address,receiver_zip,receiver_mobile,receiver_phone,orders.title,orders.pic_path,orders.price,orders.num,orders.iid,orders.num_iid,orders.sku_id,orders.refund_status,orders.status,orders.oid,orders.total_fee,orders.payment,orders.discount_fee,orders.adjust_fee,orders.sku_properties_name,orders.item_meal_name,orders.buyer_rate,orders.seller_rate,orders.outer_iid,orders.outer_sku_id,orders.refund_id,orders.seller_type";

        #region 
        [HttpGetAttribute("/core/Api/Tmall/download")]
        public ResponseResult orderDownload()
        {            

            //string fields = "",string start_created="", string end_created="", string status="",string buyer_nick="",string type="", string ext_type="",string rate_status="",string tag="",int page=1, int pageSize=100, bool use_has_next = false,string token=""
            string fields = "", start_created="",  end_created="",  status="", buyer_nick="",
                                             type="",  ext_type="", rate_status="", tag="";
                                             int page=1,  pageSize=100; bool use_has_next = false;string token="6200b00egib655065f52e655b95c0e5d1761d5f80c745552058964557";
            var m = new DataResult(1,null);
            if(string.IsNullOrEmpty(fields)){
                fields = OREDER_FIELDS;
            }
            if(string.IsNullOrEmpty(token)){
                m.s = -5000; 
            }else{
                page = Math.Max(page,1);
                pageSize = Math.Min(pageSize,100);
                m = TmallHaddle.OrderDownload( fields, start_created,  end_created,  status, buyer_nick,type,  ext_type, rate_status, tag, page,  pageSize,  use_has_next, token);
            }

            
            return CoreResult.NewResponse(m.s, m.d, "Api");
        }
        #endregion




    }
}