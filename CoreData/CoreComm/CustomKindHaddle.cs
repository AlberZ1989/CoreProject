using CoreModels;
using Dapper;
using System;
using System.Text;
using System.Collections.Generic;
using CoreModels.XyComm;
using MySql.Data.MySqlClient;


namespace CoreData.CoreComm
{
    public static class CustomKindHaddle
    {

        #region 获取类目列表
        public static DataResult GetKindLst(CusKindParam IParam)
        {
            var result = new DataResult(1, null);
            var Kind = new CustomKindData();
            string sql = @"SELECT * FROM customkind WHERE Type=@Type AND CoID=@CoID AND IsDelete=0 AND ParentID = @ParentID";
            var p = new DynamicParameters();
            p.Add("@Type", IParam.Type);
            p.Add("@CoID", IParam.CoID);
            p.Add("@ParentID", IParam.ParentID);
            if (!string.IsNullOrEmpty(IParam.Enable) && IParam.Enable.ToUpper() != "ALL")//是否启用
            {
                sql = sql + " AND Enable = @Enable";
                p.Add("@Enable", IParam.Enable.ToUpper() == "TRUE" ? true : false);
            }
            using (var conn = new MySqlConnection(DbBase.CommConnectString))
            {
                try
                {
                    var Children = conn.Query<CustomKindData>(sql, p).AsList();
                    if (IParam.ParentID > 0)
                    {
                        var res = GetKind(IParam.ParentID, IParam.CoID.ToString());
                        Kind = res.d as CustomKindData;
                    }
                    Kind.Children = Children;

                    // foreach (var p in ParentLst)
                    // {
                    //     var res = GetKindLst(Type, p.ID, CoID);
                    //     if (res.s == 1)
                    //     {
                    //         // p.Children = res.d as List<CustomKindData>;
                    //     }
                    //     else
                    //         break;
                    // }
                    result.d = Kind;
                    conn.Close();
                }
                catch (Exception e)
                {
                    result.s = -1;
                    result.d = e.Message;
                }
            }
            return result;
        }
        #endregion

        #region 获取标准类目列表
        public static DataResult GetStdKindLst(int ParentID)
        {
            var result = new DataResult(1, null);
            var KindLst = new List<ItemCateStdData>();
            string sql = @"SELECT * FROM Item_cates_standard WHERE parent_id=@ParentID";
            using (var conn = new MySqlConnection(DbBase.CommConnectString))
            {
                try
                {
                    var ParentLst = conn.Query<ItemCateStdData>(sql, new { ParentID = ParentID }).AsList();
                    KindLst.AddRange(ParentLst);
                    // foreach (var p in ParentLst)
                    // {
                    //     var res = GetStdKindLst(p.id);
                    //     if (res.s == 1)
                    //     {
                    //         p.Children = res.d as List<ItemCateStdData>;
                    //     }
                    //     else
                    //         break;
                    // }
                    result.d = ParentLst;
                    conn.Close();
                }
                catch (Exception e)
                {
                    result.s = -1;
                    result.d = e.Message;
                }
            }
            return result;
        }
        #endregion 

        #region 获取单笔类目资料
        public static DataResult GetKind(int KindID, string CoID)
        {
            var result = new DataResult(1, null);
            using (var conn = new MySqlConnection(DbBase.CommConnectString))
            {
                try
                {
                    string cname = "customkind" + CoID + KindID;
                    var KindNode = CacheBase.Get<CustomKindData>(cname);//读取缓存
                    if (KindNode == null)
                    {
                        KindNode = conn.QueryFirst<CustomKindData>("SELECT * FROM customkind WHERE ID=@ID", new { ID = KindID });
                        CacheBase.Set(cname, KindNode);//新增缓存
                    }
                    result.d = KindNode;
                }
                catch (Exception e)
                {
                    result.s = -1;
                    result.d = e.Message;
                    // CoreHelper.Debuger.exception(e);
                }
                return result;
            }
        }
        #endregion

        #region 新增类目资料
        public static DataResult InsertKind(CustomKind kind)
        {
            var result = ExistKind(kind.KindName, kind.ParentID, 0, kind.CoID);//判断类目名称是否已存在
            if (result.s == 1)
            {
                result = AddKind(kind);
            }
            return result;
        }

        public static DataResult AddKind(CustomKind kind)
        {
            var result = new DataResult(1, null);
            using (var conn = new MySqlConnection(DbBase.CommConnectString))
            {
                try
                {
                    // if (kind.ParentID > 0)
                    // {
                    //     var res = GetKind(kind.ParentID, CoID);
                    //     if (res.s == 1)
                    //     {
                    //         var PKind = res.d as CustomKindData;
                    //         // ck.FullName = PKind.FullName + "=>" + kind.KindName;
                    //     }
                    // }
                    // else
                    // {
                    //     ck.FullName = kind.KindName;
                    // }
                    string sql = @"INSERT INTO customkind(
                                        Type,
                                        KindName,
                                        FullName,
                                        Enable,
                                        ParentID,
                                        CoID,
                                        Creator,
                                        CreateDate
                                        ) VALUES(
                                        @Type,
                                        @KindName,
                                        @FullName,
                                        @Enable,
                                        @ParentID,
                                        @CoID,
                                        @Creator,
                                        @CreateDate
                                        ) ";
                    int count = conn.Execute(sql, kind);
                    kind.ID = conn.QueryFirst<int>("select LAST_INSERT_ID()");
                    if (count < 0)
                    {
                        result.s = -3002;
                    }
                    else
                    {
                        var Kind = conn.QueryFirst<CustomKind>("SELECT * FROM customkind WHERE CoID=@CoID AND ID=@KindID AND IsDelete=0", new { CoID = kind.CoID, KindID = kind.ID });
                        string cname = "customkind" + kind.CoID + kind.ID;
                        CacheBase.Set(cname, kind);//新增缓存
                        CoreUser.LogComm.InsertUserLog("新增" + kind.Type, "CustomKind", kind.KindName, kind.Creator, kind.CoID, DateTime.Now);
                    }
                }
                catch (Exception e)
                {
                    result.s = -1;
                    result.d = e.Message;
                }
                return result;
            }
        }
        #endregion

        #region 修改类目资料
        public static DataResult UptKind(CustomKind Kind, string CoID, string UserName)
        {
            var result = ExistKind(Kind.KindName, Kind.ParentID, Kind.ID, int.Parse(CoID));//判断类目名称是否已存在
            var cname = "customkind" + CoID.ToString() + Kind.ID;
            string contents = string.Empty;
            if (result.s == 1)
            {
                using (var conn = new MySqlConnection(DbBase.CommConnectString))
                {
                    try
                    {
                        var res = GetKind(Kind.ID, CoID);//原类目资料
                        var OldKind = res.d as CustomKindData;
                        // res = GetKind(Kind.ParentID, CoID);//上级类目资料
                        // var pkind = res.d as CustomKind;
                        // string fullname = pkind.FullName + "=>" + Kind.KindName;
                        if (OldKind.KindName != Kind.KindName)
                        {
                            contents = contents + "类目名称:" + OldKind.KindName + "=>" + Kind.KindName + ";";
                        }
                        // if (OldKind.FullName != Kind.FullName)
                        // {
                        //     contents = contents + "类目全名:" + OldKind.FullName + "=>" + Kind.FullName + ";";
                        // }
                        if (OldKind.ParentID != Kind.ParentID)
                        {
                            contents = contents + OldKind.ParentID.ToString() + "=>" + Kind.ParentID.ToString();
                        }
                        if (!string.IsNullOrEmpty(contents))
                        {
                            string sql = "update customkind Set KindName=@KindName,ParentID=@ParentID,Modifier=@Modifier,ModifyDate=@ModifyDate WHERE ID=@ID";
                            var p = new DynamicParameters();
                            p.Add("@KindName", Kind.KindName);
                            // p.Add("@FullName", fullname);
                            p.Add("@ParentID", Kind.ParentID);
                            p.Add("@Modifier", UserName);
                            p.Add("@ModifyDate", DateTime.Now);
                            p.Add("@ID", Kind.ID);
                            int count = conn.Execute(sql, p);
                            if (count < 0)
                            {
                                result.s = -3003;
                            }
                            else
                            {
                                CacheBase.Remove(cname);//清除缓存
                                var kind = conn.QueryFirst<CustomKind>("select * from customkind WHERE ID=@ID", new { ID = Kind.ID });
                                CacheBase.Set(cname, kind);//添加缓存
                                CoreUser.LogComm.InsertUserLog("修改" + "商品类目", "CustomKind", contents, UserName, int.Parse(CoID), DateTime.Now);//操作记录
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        result.s = -1;
                        result.d = e.Message;
                    }
                }
            }
            return result;
        }
        #endregion

        #region 检查商品类目是否存在
        public static DataResult ExistKind(string KindName, int ParentID, int ID, int CoID)
        {
            int count = 0;
            var res = new DataResult(1, null);
            using (var conn = new MySqlConnection(DbBase.CommConnectString))
            {
                try
                {
                    StringBuilder querystr = new StringBuilder();
                    querystr.Append("select count(ID) from customkind where CoID = @CoID and ParentID=@ParentID and KindName = @KindName");
                    var p = new DynamicParameters();
                    p.Add("@CoID", CoID);
                    p.Add("@KindName", KindName);
                    p.Add("@ParentID", ParentID);
                    if (ID > 0)
                    {
                        querystr.Append(" and ID !=@ID");
                        p.Add("@ID", ID);
                    }
                    count = conn.QueryFirst<int>(querystr.ToString(), p);
                    if (count > 0)
                    {
                        res.s = -1;
                        res.d = "类目名称已存在";
                    }
                }
                catch (Exception e)
                {
                    res.s = -1;
                    res.d = e.Message;
                }
                return res;
            }
        }
        #endregion

        #region 删除商品类目
        public static DataResult DelKind(List<int> IDLst, int CoID, string UserName)
        {
            var result = new DataResult(1, null);
            using (var conn = new MySqlConnection(DbBase.CommConnectString))
            {
                try
                {
                    string sql = "update customkind Set IsDelete=@IsDelete,Modifier=@Modifier,ModifyDate=@ModifyDate WHERE ID in @IDLst";
                    var p = new DynamicParameters();
                    p.Add("@IsDelete", 1);
                    p.Add("@Modifier", UserName);
                    p.Add("@ModifyDate", DateTime.Now);
                    p.Add("@IDLst", IDLst);
                    int count = conn.Execute(sql, p);
                    if (count > 0)
                    {
                        foreach (var ID in IDLst)
                        {
                            var cname = "customkind" + CoID + ID;
                            CacheBase.Remove(cname);
                        }
                        string contents = "删除类目=>" + string.Join(",", IDLst);
                        CoreUser.LogComm.InsertUserLog("删除类目资料", "User", contents, UserName, CoID, DateTime.Now);
                    }
                }
                catch (Exception e)
                {
                    result.s = -1;
                    result.d = e.Message;
                }
            }
            return result;
        }
        #endregion   


        #region 状态(停用|启用)
        public static DataResult UptKindEnable(List<int> IDLst, string CoID, string UserName, bool Enable)
        {
            var res = new DataResult(1, null);
            using (var conn = new MySqlConnection(DbBase.CommConnectString))
            {
                conn.Open();
                var Trans = conn.BeginTransaction();
                try
                {
                    string contents = string.Empty;
                    string uptsql = @"update customkind set Enable = @Enable,Modifier=@Modifier,ModifyDate=@ModifyDate where ID in @IDLst";
                    var p = new DynamicParameters();
                    p.Add("@Enable", Enable);
                    p.Add("@Modifier", UserName);
                    p.Add("@ModifyDate", DateTime.Now);
                    p.Add("@IDLst", IDLst);
                    int count = conn.Execute(uptsql, p, Trans);
                    if (count < 0)
                    {
                        res.s = -3003;
                    }
                    else
                    {
                        if (Enable)
                        {
                            contents = "类目状态启用：";
                            res.s = 3001;
                        }
                        else
                        {
                            contents = "类目状态停用：";
                            res.s = 3002;
                        }
                        contents += string.Join(",", IDLst.ToArray());
                        CoreUser.LogComm.InsertUserLog("修改类目状态", "Brand", contents, UserName, int.Parse(CoID), DateTime.Now);
                        if (res.s > 0)
                        {
                            Trans.Commit();
                        }
                    }
                }
                catch (Exception e)
                {
                    Trans.Rollback();
                    res.s = -1;
                    res.d = e.Message;
                }
                finally
                {
                    Trans.Dispose();
                    conn.Close();
                }
            }

            return res;
        }
        #endregion  

        #region 获取商品类目列表
        public static DataResult GetCommKindLst(CusKindParam IParam)
        {
            var result = new DataResult(1, null);
            string sql = @"SELECT * FROM customkind WHERE Type=@Type AND CoID=@CoID AND ParentID = @ParentID AND Enable=1 AND IsDelete=0";
            var p = new DynamicParameters();
            p.Add("@Type", IParam.Type);
            p.Add("@CoID", IParam.CoID);
            p.Add("@ParentID", IParam.ParentID);
            using (var conn = new MySqlConnection(DbBase.CommConnectString))
            {
                try
                {
                    var Children = conn.Query<CustomKindData>(sql, p).AsList();
                    result.d=Children;
                }
                catch (Exception e)
                {
                    result.s = -1;
                    result.d = e.Message;
                }
            }
            return result;
        }
        #endregion
    }
}