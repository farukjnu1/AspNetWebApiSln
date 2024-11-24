using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNetWebApi.DA.EF.VssDb;
using AspNetWebApi.DA.ViewModels.HR;

namespace AspNetWebApi.BL.HR
{
    public class EmployeeBL
    {
        ModelVssDb _vssDb = new ModelVssDb();
        public IEnumerable<EmployeeVM> Get(string phone, int pageIndex, int pageSize)
        {
            int nRow = _vssDb.Employees.Count();
            var listEmployee = _vssDb.Employees
                .Select(x => new EmployeeVM
                {
                    EmployeeId = x.EmployeeId,
                    FirstName = x.FirstName,
                    MiddleName = x.MiddleName,
                    LastName = x.LastName,
                    Mobile = x.Mobile,
                    Email = x.Email,
                    NID = x.NID,
                    DesignateId = x.DesignateId,
                    DesignationName = (from y in _vssDb.Designations where y.DesignateId == x.DesignateId select y).FirstOrDefault() == null ? "" : (from y in _vssDb.Designations where y.DesignateId == x.DesignateId select y).FirstOrDefault().Name,
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    RowCount = nRow
                })
                .OrderByDescending(s => s.EmployeeId)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToList();
            return listEmployee;
        }

        public bool Add(Employee model)
        {
            try
            {
                model.FirstName = model.FirstName;
                model.MiddleName = model.MiddleName;
                model.LastName = model.LastName;
                model.Mobile = model.Mobile;
                model.Email = model.Email;
                model.NID = model.NID;
                model.DesignateId = model.DesignateId;
                model.CreatedBy = model.CreatedBy;
                model.CreateDate = DateTime.Now;
                _vssDb.Employees.Add(model);
                _vssDb.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(Employee model)
        {
            try
            {
                var oEmployee = _vssDb.Employees
                 .Where(x => x.EmployeeId == model.EmployeeId)
                 .FirstOrDefault();
                if (oEmployee != null)
                {
                    oEmployee.EmployeeId = model.EmployeeId;
                    oEmployee.FirstName = model.FirstName;
                    oEmployee.MiddleName = model.MiddleName;
                    oEmployee.LastName = model.LastName;
                    oEmployee.Mobile = model.Mobile;
                    oEmployee.Email = model.Email;
                    oEmployee.NID = model.NID;
                    oEmployee.DesignateId = model.DesignateId;
                    oEmployee.CreatedBy = model.CreatedBy;

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
                var selectedEmployee = _vssDb.Employees
                 .Where(x => x.EmployeeId == id)
                 .FirstOrDefault();
                if (selectedEmployee != null)
                {
                    _vssDb.Employees.Remove(selectedEmployee);
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

        public IEnumerable<EmployeeVM> GetEmployee()
        {
            var listEmployee = _vssDb.Employees
                .Select(x => new EmployeeVM
                {
                    EmployeeId = x.EmployeeId,
                    FirstName = x.FirstName,
                    MiddleName = x.MiddleName,
                    LastName = x.LastName,
                })
                .OrderBy(s => s.EmployeeId)
                .ToList();
            return listEmployee;
        }

        private int GetNewId()
        {
            try
            {
                var Id = Convert.ToInt32(_vssDb.Employees.Max(x => x.EmployeeId)) + 1;
                return Id;
            }
            catch
            {
                return 1;
            }
        }
    }
}