using System;
using System.Collections.Generic;
using CoreModels;
using CoreModels.XyApi.Tmall;
using Newtonsoft.Json;

namespace CoreData.CoreApi
{
    public  class TmallItemHaddle:TmallBase
    {  
        
        private static string ONSALE_GET = "approve_status,num_iid,title,nick,type,cid,pic_url,num,props,valid_thru,list_time,price,has_discount,has_invoice,has_warranty,has_showcase,modified,delist_time,postage_id,seller_cids,outer_id"; 
        private static string SELLER_GET = "cid,outer_id,sku,pic_url,barcode,price";
        
        #region 
        /// <summary>
        /// 获取当前会话用户出售中的商品列表  参考网址： http://open.taobao.com/docs/api.htm?spm=a219a.7395905.0.0.rsTDHK&apiId=18
        /// </summary>
        public static DataResult onsaleGet (string page,string pageSize,string start_modified,string end_modified){
            var result = new DataResult(1,null);
            try{                                        
                Tmparam.Add("method", "taobao.items.onsale.get");
                Tmparam.Add("session", TOKEN);
                Tmparam.Add("fields",ONSALE_GET);
                Tmparam.Add("page_no",page);
                Tmparam.Add("page_size",pageSize);


                string sign = JsonResponse.SignTopRequest(Tmparam, SECRET, "md5");
                Tmparam.Add("sign", sign);//                                      
                var response = JsonResponse.CreatePostHttpResponse(SERVER_URL, Tmparam);            
                var res = JsonConvert.DeserializeObject<dynamic>(response.Result.ToString().Replace("\"","\'")+"}");                                                               
                if(response.Result.ToString().IndexOf("error_response") > 0){
                    result.s = -1;
                    result.d ="code:"+res.error_response.code+" "+res.error_response.sub_msg+" "+res.error_response.msg;
                }else{
                    result.d = res.items_onsale_get_response.items.item;
                }            
            }catch(Exception ex){                
                result.s = -1;
                result.d =  ex.Message;
            }finally{
                cleanParam();
            }        
            return result;
        }
        #endregion

        #region 
        /// <summary>
        /// 获取单个商品详细信息  参考网址： https://open.tmall.com/docs/api.htm?spm=a219a.7629065.0.0.W3DzKp&apiId=24625
        /// </summary>
        public static DataResult sellerGet(string num_iid){
            var result = new DataResult(1,null);
            try{                                        
                Tmparam.Add("method", "taobao.item.seller.get");
                Tmparam.Add("session", TOKEN);
                Tmparam.Add("fields",SELLER_GET);   
                Tmparam.Add("num_iid",num_iid);

                removeEmptyParam();
                string sign = JsonResponse.SignTopRequest(Tmparam, SECRET, "md5");
                Tmparam.Add("sign", sign);//                                      
                var response = JsonResponse.CreatePostHttpResponse(SERVER_URL, Tmparam);            
                var res = JsonConvert.DeserializeObject<dynamic>(response.Result.ToString().Replace("\"","\'")+"}");                                                               
                if(response.Result.ToString().IndexOf("error_response") > 0){
                    result.s = -1;
                    result.d ="code:"+res.error_response.code+" "+res.error_response.sub_msg+" "+res.error_response.msg;
                }else{
                    if(response.Result.ToString().IndexOf("sku")>-1){
                        result.d = res.item_seller_get_response.item.skus.sku;
                    }
                    
                }            
            }catch(Exception ex){                
                result.s = -1;
                result.d =  ex.Message;
            }finally{
                cleanParam();
            }        
            return result;
        }
        #endregion

        #region 
        /// <summary>
        ///   添加一个商品 参考网址： https://open.tmall.com/doc2/apiDetail.htm?apiId=22
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        public static DataResult itemAdd(string token,item_add_request item){
            var result = new DataResult(1,null);
            try{                                        
                Tmparam.Add("method", "taobao.item.add");
                Tmparam.Add("session", token);
                Tmparam.Add("num",item.num.ToString());
                Tmparam.Add("title",item.title);
                Tmparam.Add("des",item.des);
                Tmparam.Add("input_str",item.input_str);
                Tmparam.Add("input_pids",item.input_pids);
                Tmparam.Add("price",item.price);
                Tmparam.Add("type",item.type);
                Tmparam.Add("stuff_status",item.stuff_status);
                Tmparam.Add("location.state",item.locationState);
                Tmparam.Add("location.city",item.locationCity);
                Tmparam.Add("cid",item.cid.ToString());
            
                removeEmptyParam();
                string sign = JsonResponse.SignTopRequest(Tmparam, SECRET, "md5");
                Tmparam.Add("sign", sign);//                                      
                var response = JsonResponse.CreatePostHttpResponse(SERVER_URL, Tmparam);            
                var res = JsonConvert.DeserializeObject<dynamic>(response.Result.ToString().Replace("\"","\'")+"}");                                                               
                if(response.Result.ToString().IndexOf("error_response") > 0){
                    result.s = -1;
                    result.d ="code:"+res.error_response.code+" "+res.error_response.sub_msg+" "+res.error_response.msg;
                }else{
                    result.d = res;
                }            
            }catch(Exception ex){                
                result.s = -1;
                result.d =  ex.Message;
            }finally{
                cleanParam();
            }        
            return result;
        }
        #endregion





    }
}