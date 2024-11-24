using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNetWebApi.DA.EF.VssDb;
using AspNetWebApi.DA.ViewModels.Operation;
using AspNetWebApi.DA.ViewModels.System;

namespace AspNetWebApi.BL.Operation
{
    public class WorkGroupBL
    {
        ModelVssDb _vssDb = new ModelVssDb();
        public IEnumerable<WorkGroupVM> Get(string phone, int pageIndex, int pageSize)
        {
            int nRow = _vssDb.WorkGroups.Count();
            var listWorkGroup = _vssDb.WorkGroups
                .Select(x => new WorkGroupVM
                {
                    WgId = x.WgId,
                    WgName = x.WgName,

                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    RowCount = nRow
                })
                .OrderBy(s => s.WgId)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToList();
            return listWorkGroup;
        }

        public bool Add(WorkGroup model)
        {
            try
            {
                model.WgId = GetNewId();
                model.WgName = model.WgName;
                _vssDb.WorkGroups.Add(model);
                _vssDb.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(WorkGroup model)
        {
            try
            {
                var oWorkGroup = _vssDb.WorkGroups
                 .Where(x => x.WgId == model.WgId)
                 .FirstOrDefault();
                if (oWorkGroup != null)
                {
                    oWorkGroup.WgId = model.WgId;
                    oWorkGroup.WgName = model.WgName;

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
                var selectedWorkGroup = _vssDb.WorkGroups
                 .Where(x => x.WgId == id)
                 .FirstOrDefault();
                if (selectedWorkGroup != null)
                {
                    _vssDb.WorkGroups.Remove(selectedWorkGroup);
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

        public IEnumerable<WorkGroupVM> GetWorkGroup()
        {
            var listWorkGroup = _vssDb.WorkGroups
                .Select(x => new WorkGroupVM
                {
                    WgId = x.WgId,
                    WgName = x.WgName
                }).OrderBy(s => s.WgId).ToList();
            return listWorkGroup;
        }

        private int GetNewId()
        {
            try
            {
                var Id = Convert.ToInt32(_vssDb.WorkGroups.Max(x => x.WgId)) + 1;
                return Id;
            }
            catch
            {
                return 1;
            }
        }
    }
}