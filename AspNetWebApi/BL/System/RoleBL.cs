using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNetWebApi.DA.EF.VssDb;
using AspNetWebApi.DA.ViewModels.System;

namespace AspNetWebApi.BL.System
{
    public class RoleBL
    {
        ModelVssDb _vssDb = new ModelVssDb();
        internal IEnumerable<RoleVM> get()
        {
            var listRole = _vssDb.Roles
                .Select(x => new RoleVM
                {
                    RoleId = x.RoleId,
                    RoleName = x.RoleName,
                }).OrderBy(s => s.RoleId).ToList();
            return listRole;
        }

        public bool Add(Role model)
        {
            try
            {
                model.RoleId = GetNewId();
                model.RoleName = model.RoleName;
                _vssDb.Roles.Add(model);
                _vssDb.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(Role model)
        {
            try
            {
                var oRole = _vssDb.Roles
                 .Where(x => x.RoleId == model.RoleId)
                 .FirstOrDefault();
                if (oRole != null)
                {
                    oRole.RoleId = model.RoleId;
                    oRole.RoleName = model.RoleName;
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
                var selectedRole = _vssDb.Roles
                 .Where(x => x.RoleId == id)
                 .FirstOrDefault();
                if (selectedRole != null)
                {
                    _vssDb.Roles.Remove(selectedRole);
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
                var Id = Convert.ToInt32(_vssDb.Roles.Max(x => x.RoleId)) + 1;
                return Id;
            }
            catch
            {
                return 1;
            }
        }
    }
}