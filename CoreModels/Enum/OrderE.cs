
namespace CoreModels.Enum
{
    public class OrderE
    {
        public enum ShopSit
        {
            淘宝 = 0,
            天猫 = 1,
            阿里 = 2,
            京东 = 3,
            唯品会 = 4,
            当当 = 5,
            亚马逊 = 6,
            苏宁易购 = 7,
            一号店 = 8,
            有赞 = 9,
            聚美 = 10,
            国美 = 11,
            美丽说 = 12,
            楚楚街 = 13,
            蘑菇街 = 14,
            口袋 = 15,
            速卖通 = 16,
            折800 = 17,
            贝贝网 = 18,
            微盟 = 19,
            萌店 = 20,
            卖客疯 = 21,
            微众 = 22,
            飞牛网 = 23,
            美团 = 24,
            爱奇艺 = 25,
            幸福9号 = 26,
            关爱 = 27,
            高朋 = 28,
            明星衣橱 = 29,
            卷皮 = 30,
            工商 = 31,
            网易 = 32,
            拼多多 = 33,
            蜜芽 = 34,
            线下 = 35
        }  
        public enum ApiTypes
        {
            Unknow = -1,
            Taobao = 0,
            Tmall = 1,
            Alibaba = 2,
            Jos = 3,
            Vip =4,
            Dd = 5,
            Amazon = 6,
            Sunning = 7,
            Yhd = 8,
            Youzan = 9,
            Jm =10,
            Gm = 11,
            Mls =12,
            Ccj =13,
            Mgj = 14,
            Koudai = 15,
            Aliexpress = 16,
            Zhe800 = 17,
            Beibei = 18,
            Weimob = 19,
            Mengdian = 20,
            Mkf = 21,
            Weizoom=22,
            Feiniu = 23,
            Meituan = 24,
            Iqiyi = 25,
            Xf9 = 26,
            Guanai = 27,
            Gaopeng =28,
            Hichao = 29,
            Juanpi = 30,
            Gongshang = 31,
            Kaola = 32,
            Pinduoduo =33,
            Mia = 34,
            Xianxia = 35,
            Base = 36,

        }
        public enum OrdStatus
        {
            待付款 = 0,
            已付款待审核 = 1,
            已审核待配快递 = 2,
            发货中 = 3,
            已发货 = 4,
            被合并 = 5,
            已取消 = 6,
            异常 = 7,
            等供销商发货 = 8
        }
        public enum OrdSource
        {
            微信订单 = 0,
            PC端订单 = 1,
            移动端订单 = 2,
            手工下单 = 3,
            手Q订单 = 4,
            历史订单暂无来源 =5,
            售后 = 6
        }
        public enum TokenType {
            未授权 = 0,
            已授权 = 1,
            已过期 = 2
        }
        public enum SaleOutStatus
        {
            待出库 = 0,
            已出库生效 = 1,
            已出库待发货 = 2,
            已出库并且已发货 = 3,
            归档 = 4,
            出库后作废 = 5,
            出库前作废 = 6,
            外部发货中 = 7
        }
        public enum ASStatus
        {
            待确认 = 0,
            已确认 = 1,
            作废 = 2,
            被合并 = 3
        }
        public enum ASType
        {
            普通退货 = 0,
            拒收退货 = 1,
            换货 = 2,
            补发 = 3,
            投诉 = 4,
            其他 = 5
        }
        public enum IssueType
        {
            不喜欢 = 0,
            有质量问题 = 1,
            七天无理由退货 = 2,
            重复购买 = 3,
            与描述不符 = 4,
            不想买了 = 5,
            其他 = 6,
            买错了 = 7,
            服务承诺态度 = 8,
            条码与吊牌与商品不符 = 9,
            发错货 = 10,
            退运费 = 11,
            未按约定时间发货 = 12,
            差价 = 13,
            补偿 = 14,
            丢件或快递破损 = 15
        }
        public enum Result
        {
            待退货 = 0,
            已退过 = 1,
            支付宝退不了 = 2,
            未申请 = 3,
            邮费差价再核实 = 4,
            漏填无法退 = 5,
            中差评 = 6,
            其他 = 7,
            未确认收货 = 8
        }
        public enum ReturnType
        {
            退货 = 0,
            换货 = 1,
            补发 = 2
        }
        public enum PayStatus
        {
            待审核 = 0,
            已确认 = 1,
            作废 = 2
        }
        public enum RefundStatus
        {
            待审核 = 0,
            已确认 = 1,
            作废 = 2,
            完成 = 3
        }
    }
}
