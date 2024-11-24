using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Web;
using AspNetWebApi.Attributes;
using AspNetWebApi.DA.EF.VssDb;
using AspNetWebApi.DA.ViewModels.System;
using AspNetWebApi.BL.System;

namespace AspNetWebApi.BL.System
{
    public class LoginBL
    {
        ModelVssDb _Db = null;
        public UserVM Login(UserVM model)
        {
            UserVM oUser = null;
            _Db = new ModelVssDb();
            #region User Credential
            oUser = _Db.Users.Where(x => x.UserName == model.UserName && x.UserPass == model.UserPass && x.IsActive == true).Select(y => new UserVM()
            {
                UserName = y.UserName,
                UserPass = y.UserPass,
                FirstName = y.FirstName,
                MiddleName = y.MiddleName,
                LastName = y.LastName,
                UserID = y.UserID,
                UserCode = y.UserCode,
                Email = y.Email,
                MobileNo = y.MobileNo
            }).FirstOrDefault();
            #endregion
            if (oUser != null) 
            {
                #region Token
                NameValueCollection myKeys = ConfigurationManager.AppSettings;
                int tokenExpire = Convert.ToInt32(myKeys["TokenExpire"]);
                string VSS = myKeys["VSS"];
                oUser.Permissions = new MenuPermissionBL().GetMenuByUser(oUser.UserID);
                oUser.Token = JsonWebToken.Encode(new UserPayload() { CreateDate = DateTime.Now, UserId = oUser.UserID, TokenExpire = tokenExpire, UserName = oUser.UserName }, VSS, JwtHashAlgorithm.HS512);
                #endregion
                #region Notification
                oUser.nNotify = _Db.JcReqs.Where(x => x.IsRead != true).Count();
                #endregion
            }
            return oUser;
        }
    }
}