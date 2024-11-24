using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNetWebApi.DA.EF.VssDb;
using AspNetWebApi.DA.ViewModels.Operation;

namespace AspNetWebApi.BL.Operation
{
    public class WorkGroupEmpBL
    {
        ModelVssDb _vssDb = new ModelVssDb();
        public IEnumerable<WorkGroupEmpVM> Get(string phone, int pageIndex, int pageSize)
        {
            int nRow = _vssDb.WorkGroupEmps.Count();
            var listWorkGroupMember = _vssDb.WorkGroupEmps
                .Select(x => new WorkGroupEmpVM
                {
                    Id =x.Id,
                    WgId = x.WgId,
                    WgName = (from y in _vssDb.WorkGroups where y.WgId == x.WgId select y).FirstOrDefault() == null ? "" : (from y in _vssDb.WorkGroups where y.WgId == x.WgId select y).FirstOrDefault().WgName,
                    EmpId = x.EmpId,
                    FirstName = (from y in _vssDb.Employees where y.EmployeeId == x.EmpId select y).FirstOrDefault() == null ? "" : (from y in _vssDb.Employees where y.EmployeeId == x.EmpId select y).FirstOrDefault().FirstName,
                    MiddleName = (from y in _vssDb.Employees where y.EmployeeId == x.EmpId select y).FirstOrDefault() == null ? "" : (from y in _vssDb.Employees where y.EmployeeId == x.EmpId select y).FirstOrDefault().MiddleName,
                    LastName = (from y in _vssDb.Employees where y.EmployeeId == x.EmpId select y).FirstOrDefault() == null ? "" : (from y in _vssDb.Employees where y.EmployeeId == x.EmpId select y).FirstOrDefault().LastName,


                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    RowCount = nRow
                })
                .OrderBy(s => s.Id)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToList();
            return listWorkGroupMember;
        }

        public bool Add(WorkGroupEmp model)
        {
            try
            {
                model.WgId = model.WgId;
                model.EmpId = model.EmpId;
                _vssDb.WorkGroupEmps.Add(model);
                _vssDb.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(WorkGroupEmp model)
        {
            try
            {
                var oWorkGroupMember = _vssDb.WorkGroupEmps
                 .Where(x => x.Id == model.Id)
                 .FirstOrDefault();
                if (oWorkGroupMember != null)
                {
                    oWorkGroupMember.WgId = model.WgId;
                    oWorkGroupMember.EmpId = model.EmpId;

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
                var selectedWorkGroupMember = _vssDb.WorkGroupEmps
                 .Where(x => x.Id == id)
                 .FirstOrDefault();
                if (selectedWorkGroupMember != null)
                {
                    _vssDb.WorkGroupEmps.Remove(selectedWorkGroupMember);
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
                var Id = Convert.ToInt32(_vssDb.WorkGroupEmps.Max(x => x.Id)) + 1;
                return Id;
            }
            catch(Exception ex)
            {
                return 1;
            }
        }
    }
}