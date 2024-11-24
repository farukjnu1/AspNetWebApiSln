using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNetWebApi.DA.EF.VssDb;
using AspNetWebApi.DA.ViewModels.System;

namespace AspNetWebApi.BL.System
{
    public class UserBL
    {
        ModelVssDb _vssDb = new ModelVssDb();
        public IEnumerable<UserVM> Get()
        {
            var listUser = _vssDb.Users
                .Where(y=>y.UserID > 2)
                .Select(x => new UserVM
                {
                    UserID = x.UserID,
                    UserCode = x.UserCode,
                    UserName = x.UserName,
                    FirstName = x.FirstName,
                    MiddleName = x.MiddleName,
                    LastName = x.LastName,
                    Email = x.Email,
                    MobileNo = x.MobileNo,
                    PhoneNo = x.PhoneNo,
                }).OrderBy(s => s.FirstName).ToList();
            return listUser;
        }

        public bool Add(User model)
        {
            try
            {
                model.UserID = GetNewId();
                model.UserCode = model.UserCode;
                model.UserName = model.UserName;
                model.FirstName = model.FirstName;
                model.MiddleName = model.MiddleName;
                model.LastName = model.LastName;
                model.Email = model.Email;
                model.MobileNo = model.MobileNo;
                model.PhoneNo = model.PhoneNo;
                model.CreateBy = model.CreateBy;
                model.CreateDate = DateTime.Now;
                model.UserPass = string.IsNullOrEmpty(model.UserPass) ? "123" : model.UserPass;
                _vssDb.Users.Add(model);
                _vssDb.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Update(User model)
        {
            try
            {
                var oUser = _vssDb.Users
                 .Where(x => x.UserID == model.UserID)
                 .FirstOrDefault();
                if (oUser != null)
                {
                    oUser.UserID = model.UserID;
                    oUser.UserName = model.UserName;
                    oUser.FirstName = model.FirstName;
                    oUser.MiddleName = model.MiddleName;    
                    oUser.LastName = model.LastName;
                    oUser.Email = model.Email;
                    oUser.MobileNo = model.MobileNo;
                    oUser.PhoneNo = model.PhoneNo;
                    oUser.UpdateBy = model.UpdateBy;
                    oUser.UpdateDate = DateTime.Now;
                    model.UserPass = string.IsNullOrEmpty(model.UserPass) ? "123" : model.UserPass;
                    _vssDb.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }

        public bool Remove(int id)
        {
            try
            {
                var selectedUser = _vssDb.Users
                 .Where(x => x.UserID == id)
                 .FirstOrDefault();
                if (selectedUser != null)
                {
                    _vssDb.Users.Remove(selectedUser);
                    _vssDb.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }
        private int GetNewId()
        {
            try
            {
                var Id = Convert.ToInt32(_vssDb.Users.Max(x => x.UserID)) + 1;
                return Id;
            }
            catch
            {
                return 1;
            }
        }
    }
}