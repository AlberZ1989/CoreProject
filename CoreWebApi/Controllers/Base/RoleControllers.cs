using CoreData.CoreUser;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebApi
{
    public class RoleControllers:ControllBase
    {
         [HttpGetAttribute("/core/role/rolelist")]
         public ResponseResult rolelist()
         {             
            var roleid = GetRoleid();
            var coid = GetCoid();        
            var m = RoleHaddle.getrolelist();
            return CoreResult.NewResponse(m.s, m.d, "Indentity");           
         }


    }
    
}