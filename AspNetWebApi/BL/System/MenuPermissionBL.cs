using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using AspNetWebApi.DA.ADO;
using AspNetWebApi.DA.EF.VssDb;
using AspNetWebApi.DA.ViewModels.System;

namespace AspNetWebApi.BL.System
{
    public class MenuPermissionBL
    {
        ModelVssDb _vssDb = new ModelVssDb();
        private IGenericFactory<MenuPermissionVM> Generic_MenuPermissionVM = null;

        public List<MenuPermissionVM> GetMenuPermission(int RoleId)
        {
            string rvssDb = ConfigurationManager.ConnectionStrings["VssDb"].ConnectionString;
            Generic_MenuPermissionVM = new GenericFactory<MenuPermissionVM>();
            var rHashTable = new Hashtable()
            {
                { "RoleId", RoleId }
            };

            List<MenuPermissionVM> ResutSet = Generic_MenuPermissionVM.ExecuteCommandList(CommandType.StoredProcedure, StoredProcedure.sp_GetMenuPermission, rHashTable, rvssDb);
            return ResutSet;
        }

        public List<MenuPermissionVM> GetMenuByUser(int UserId)
        {
            string rvssDb = ConfigurationManager.ConnectionStrings["VssDb"].ConnectionString;
            Generic_MenuPermissionVM = new GenericFactory<MenuPermissionVM>();
            var rHashTable = new Hashtable()
            {
                { "UserId", UserId }
            };
            return Generic_MenuPermissionVM.ExecuteCommandList(CommandType.StoredProcedure, StoredProcedure.sp_GetMenuPermissionByUserId, rHashTable, rvssDb);
        }

        internal bool add(List<MenuPermissionVM> listModel)
        {
            using (_vssDb = new ModelVssDb())
            {
                using (var _tran = _vssDb.Database.BeginTransaction())
                {
                    try
                    {
                        #region JC HR
                        var RoleId = listModel[0].RoleId;
                        var listMPRem = _vssDb.MenuPermissions.Where(x => x.RoleId == RoleId).ToList();
                        _vssDb.MenuPermissions.RemoveRange(listMPRem);
                        _vssDb.SaveChanges();
                        List<MenuPermission> listMP = new List<MenuPermission>();
                        foreach (var model in listModel)
                        {
                            if ((bool)model.CanView)
                            {
                                MenuPermission objectMP = new MenuPermission();
                                objectMP.CanCreate = model.CanCreate;
                                objectMP.CanEdit = model.CanEdit;
                                objectMP.CanDelete = model.CanDelete;
                                objectMP.CanView = model.CanView;

                                objectMP.MenuId = model.MenuId;
                                objectMP.RoleId = model.RoleId;
                                listMP.Add(objectMP);
                            }
                        }
                        _vssDb.MenuPermissions.AddRange(listMP);
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