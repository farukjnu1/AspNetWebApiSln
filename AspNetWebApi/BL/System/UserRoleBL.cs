using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using AspNetWebApi.DA.ADO;
using AspNetWebApi.DA.EF.VssDb;
using AspNetWebApi.DA.ViewModels.Operation;
using AspNetWebApi.DA.ViewModels.Stores;
using AspNetWebApi.DA.ViewModels.System;

namespace AspNetWebApi.BL.System
{
    public class UserRoleBL
    {

        ModelVssDb _vssDb = new ModelVssDb();
        private IGenericFactory<UserRoleVM> Generic_UserRoleVM = null;

        public List<UserRoleVM> GetUserRole(int userId)
        {
            string vssDb = ConfigurationManager.ConnectionStrings["VssDb"].ConnectionString;
            Generic_UserRoleVM = new GenericFactory<UserRoleVM>();
            var oHashTable = new Hashtable() 
            {
                { "UserId", userId }
            };
            return Generic_UserRoleVM.ExecuteCommandList(CommandType.StoredProcedure,StoredProcedure.sp_GetUserRole, oHashTable, vssDb);
        }

        public bool add(List<UserRoleVM> listModel)
        {
            using (_vssDb = new ModelVssDb())
            {
                using (var _tran = _vssDb.Database.BeginTransaction())
                {
                    try
                    {
                        #region JC HR
                        var UserId = listModel[0].UserId;
                        var listUrRem = _vssDb.UserRoles.Where(x => x.UserId == UserId).ToList();
                        _vssDb.UserRoles.RemoveRange(listUrRem);
                        _vssDb.SaveChanges();
                        List<UserRole> listUR = new List<UserRole>();
                        foreach (var model in listModel)
                        {
                            if (model.IsSelect)
                            {
                                UserRole oUR = new UserRole();
                                oUR.UserId = model.UserId;
                                oUR.RoleId = model.RoleId;
                                listUR.Add(oUR);
                            }
                        }
                        _vssDb.UserRoles.AddRange(listUR);
                        _vssDb.SaveChanges();
                        #endregion
                        _tran.Commit();
                        return true;
                    }
                    catch
                    {
                        _tran.Rollback();
                        return false;
                    }
                }
            }
        }
    }
}