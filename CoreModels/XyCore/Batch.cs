using System;
using System.Collections.Generic;
namespace CoreModels.XyCore
{
    public class GetBatchInit
    {
        public List<Filter> BatchStatus{get;set;}
        public List<Filter> Pickor{get;set;}
        public List<Filter> Task{get;set;}
        public List<Filter> BatchType{get;set;}
    }
    public class Batch
    {
        public int ID{get;set;}
        public int Type{get;set;}
        public int PickorID{get;set;}
        public string Pickor{get;set;}
        public int OrderQty{get;set;}
        public int SkuQty{get;set;}
        public int Qty{get;set;}
        public int PickedQty{get;set;}
        public int NoQty{get;set;}
        public int Status{get;set;}
        public string Mark{get;set;}
        public int CoID{get;set;}
        public bool MixedPicking{get;set;}
        public bool PickingPrint{get;set;}
        public string Creator{get;set;}
        public DateTime CreateDate{get;set;}
        public string Modifier{get;set;}
        public DateTime ModifyDate{get;set;}
    }
    public class BatchQuery
    {
        public int ID{get;set;}
        public int Type{get;set;}
        public string TypeString{get;set;}
        public string Pickor{get;set;}
        public int OrderQty{get;set;}
        public int SkuQty{get;set;}
        public int Qty{get;set;}
        public int PickedQty{get;set;}
        public int NotPickedQty{get;set;}
        public int NoQty{get;set;}
        public int Status{get;set;}
        public string StatusString{get;set;}
        public string CreateDate{get;set;}
        public string Mark{get;set;}
        public bool MixedPicking{get;set;}
        public bool PickingPrint{get;set;}
    }
    public class BatchData
    {
        public int Datacnt {get;set;}//总资料笔数
        public decimal Pagecnt{get;set;}//总页数
        public List<BatchQuery> Batch {get;set;}
    }
    public class BatchParm
    {
        public int _CoID ;//公司id
        public List<int> _Status = null;
        public int _ID = 0;//批次号
        public string _Remark = null;
        public List<int> _PickorID = null;
        public string _Task = "A";
        public List<int> _Type = null;
        public DateTime _DateStart = DateTime.Parse("1900-01-01");//日期起
        public DateTime _DateEnd = DateTime.Parse("1900-01-01");//日期迄
        public string _SortField = "id";//排序栏位
        public string _SortDirection = "DESC";//排序方式
        public int _NumPerPage = 20;//每页显示资料笔数
        public int _PageIndex = 1;//页码
        public int CoID
        {
            get { return _CoID; }
            set { this._CoID = value;}
        }
        public List<int> Status
        {
            get { return _Status; }
            set { this._Status = value;}
        }
        public int ID
        {
            get { return _ID; }
            set { this._ID = value;}
        }
        public string Remark
        {
            get { return _Remark; }
            set { this._Remark = value;}
        }
        public List<int> PickorID
        {
            get { return _PickorID; }
            set { this._PickorID = value;}
        }
        public string Task
        {
            get { return _Task; }
            set { this._Task = value;}
        }
        public List<int> Type
        {
            get { return _Type; }
            set { this._Type = value;}
        }
        public DateTime DateStart
        {
            get { return _DateStart; }
            set { this._DateStart = value;}
        }
        public DateTime DateEnd
        {
            get { return _DateEnd; }
            set { this._DateEnd = value;}
        }
        public string SortField
        {
            get { return _SortField; }
            set { this._SortField = value;}
        }
        public string SortDirection
        {
            get { return _SortDirection; }
            set { this._SortDirection = value;}
        }
        public int NumPerPage
        {
            get { return _NumPerPage; }
            set { this._NumPerPage = value;}
        }
        public int PageIndex
        {
            get { return _PageIndex; }
            set { this._PageIndex = value;}
        }
    }
    public class BatchConfigure
    {
        public int ID{get;set;}
        public int SingleOrdQty{get;set;}
        public int MultiOrdQty{get;set;}
        public int SingleSkuQty{get;set;}
        public int MultiNotOrdQty{get;set;}
        public int BigQty{get;set;}
        public string Express{get;set;}
        public string Shop{get;set;}
        public bool SpecialOrd{get;set;}
    }
    public class ModifyRemarkSuccess
    {
        public int ID{get;set;}
        public string Remark{get;set;}
    }
    public class ModifyRemarkReturn
    {
        public List<ModifyRemarkSuccess> SuccessIDs{get;set;}
        public List<TransferNormalReturnFail> FailIDs{get;set;}
    }
    public class MarkPrintSuccess
    {
        public int ID{get;set;}
        public bool PickingPrint{get;set;}
    }
    public class MarkPrintReturn
    {
        public List<MarkPrintSuccess> SuccessIDs{get;set;}
        public List<TransferNormalReturnFail> FailIDs{get;set;}
    }
    public class GetPickorInit
    {
        public List<Filter> Role{get;set;}
        public List<Filter> Pickor{get;set;}
    }
    public class SetPickorSuccess
    {
        public int ID{get;set;}
        public string Pickor{get;set;}
    }
    public class SetPickorReturn
    {
        public List<SetPickorSuccess> SuccessIDs{get;set;}
        public List<TransferNormalReturnFail> FailIDs{get;set;}
    }
    public class GetOrdCountReturn
    {
        public int SingleOrd{get;set;}
        public int MultiOrd{get;set;}
        public int BigOrd{get;set;}
    }
    public class InvQty
    {
        public int ID{get;set;}
        public int Qty{get;set;}
        public string PCode{get;set;}
        public string Order{get;set;}
    }
    public class Sku
    {
        public int SkuAutoID{get;set;}
        public string SkuID{get;set;}
        public string SkuName{get;set;}
    }
    public class StrategyList
    {
        public int ID{get;set;}
        public string StrategyName{get;set;}
    }
    public class BatchStrategy
    {
        public int ID{get;set;}
        public int Type{get;set;}
        public string StrategyName{get;set;}
        public string SkuIn{get;set;}
        public string SkuNotIn{get;set;}
        public int OrdGift{get;set;}
        public string KindIDIn{get;set;}
        public string PCodeIn{get;set;}
        public int ExpPrint{get;set;}
        public string ExpressIn{get;set;}
        public string DistributorIn{get;set;}
        public string ShopIn{get;set;}
        public string AmtMin{get;set;}
        public string AmtMax{get;set;}
        public string PayDateStart{get;set;}
        public string PayDateEnd{get;set;}
        public string RecMessage{get;set;}
        public string SendMessage{get;set;}
        public string PrioritySku{get;set;}
        public string OrdQty{get;set;}
        public int CoID{get;set;}
    }
    public class OrdTask
    {
        public int id{get;set;}
        public int oid{get;set;}
        public string Sku{get;set;}
        public string ExpName{get;set;}
        public int OrdQty{get;set;}
    }
    public class CancleBatchSuccess
    {
        public int ID{get;set;}
        public int NotPickedQty{get;set;}
        public int NoQty{get;set;}
        public int Status{get;set;}
        public string StatusString{get;set;}
    }
    public class CancleBatchReturn
    {
        public List<CancleBatchSuccess> SuccessIDs{get;set;}
        public List<TransferNormalReturnFail> FailIDs{get;set;}
    }
    public class BatchTask
    {
        public int ID{get;set;}
        public int BatchID{get;set;}
        public int CoID{get;set;}
        public int Skuautoid{get;set;}
        public string SkuID{get;set;}
        public string SkuName{get;set;}
        public string PCode{get;set;}
        public int Qty{get;set;}
        public int Index{get;set;}
        public int PickQty{get;set;}
        public int NoQty{get;set;}
    }
    public class LackSkuList
    {
        public int SkuAutoID{get;set;}
        public string SkuID{get;set;}
        public string GoodsCode{get;set;}
        public string Norm{get;set;}
        public int OrdQty{get;set;}
        public int NoQty{get;set;}
    }
    public class BatchLog
    {
        public string SaleID{get;set;}
        public string Operate{get;set;}
        public string UniqueCode{get;set;}
        public string SkuID{get;set;}
        public int Qty{get;set;}
        public string Remark{get;set;}
        public string Remark2{get;set;}
        public string Creator{get;set;}
        public string CreateDate{get;set;}
    }
    public class BatchItemList
    {
        public string SkuID{get;set;}
        public string SkuName{get;set;}
        public string Norm{get;set;}
        public int Qty{get;set;}
        public int PickQty{get;set;}
        public int NoQty{get;set;}
    }
    public class BatchUniqueList
    {
        public string BarCode{get;set;}
        public string Sku{get;set;}
        public int Status{get;set;}
        public string StatusString{get;set;}
        public int OutID{get;set;}
        public string OutIDString{get;set;}
        public string PCode{get;set;}
        public string ModifyDate{get;set;}
    }
}