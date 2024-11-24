using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNetWebApi.DA.EF.VssDb;
using AspNetWebApi.DA.ViewModels.HR;
using AspNetWebApi.DA.ViewModels.Stores;

namespace AspNetWebApi.BL.HR
{
    public class DesignationBL
    {
        ModelVssDb _vssDb = new ModelVssDb();
        public IEnumerable<DesignationVM> Get(string phone, int pageIndex, int pageSize)
        {
            int nRow = _vssDb.Designations.Count();
            var listDesignation = _vssDb.Designations
                .Select(x => new DesignationVM
                {
                    DesignateId = x.DesignateId,
                    Name = x.Name,
                    Short = x.Short,

                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    RowCount = nRow
                })
                .OrderByDescending(s => s.DesignateId)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToList();
            return listDesignation;
        }

        public bool Add(Designation model)
        {
            try
            {
                model.DesignateId = GetNewId();
                model.Name = model.Name;
                model.Short = model.Short;
                _vssDb.Designations.Add(model);
                _vssDb.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(Designation model)
        {
            try
            {
                var oDesignation = _vssDb.Designations
                 .Where(x => x.DesignateId == model.DesignateId)
                 .FirstOrDefault();
                if (oDesignation != null)
                {
                    oDesignation.DesignateId = model.DesignateId;
                    oDesignation.Name = model.Name;
                    oDesignation.Short = model.Short;
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
                var selectedDesignation = _vssDb.Designations
                 .Where(x => x.DesignateId == id)
                 .FirstOrDefault();
                if (selectedDesignation != null)
                {
                    _vssDb.Designations.Remove(selectedDesignation);
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
                var Id = Convert.ToInt32(_vssDb.Designations.Max(x => x.DesignateId)) + 1;
                return Id;
            }
            catch
            {
                return 1;
            }
        }
    }
}