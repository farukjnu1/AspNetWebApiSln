using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNetWebApi.DA.EF.VssDb;
using AspNetWebApi.DA.ViewModels.Stores;

namespace AspNetWebApi.BL.Stores
{
    public class BrandModelBL
    {
        ModelVssDb _vssDb = new ModelVssDb();
        public IEnumerable<BrandModelVM> Get(int brandId, int pageIndex, int pageSize)
        {
            int nRow = (from bm in _vssDb.BrandModels
                        join b in _vssDb.Brands on bm.BrandId equals b.Id
                        where bm.BrandId == (brandId == 0 ? bm.BrandId : brandId)
                        select new BrandModelVM
                        {
                            Id = bm.Id,
                        }).Count();
            var listBranModel = (from bm in _vssDb.BrandModels
                                 join b in _vssDb.Brands on bm.BrandId equals b.Id
                                 where bm.BrandId == (brandId == 0 ? bm.BrandId : brandId)
                                 select new BrandModelVM
                                 {
                                     BrandId = bm.BrandId,
                                     Id = bm.Id,
                                     ModelCode = bm.ModelCode,
                                     Remarks = bm.Remarks,
                                     Name = b.Name,
                                     PageIndex = pageIndex,
                                     PageSize = pageSize,
                                     RowCount = nRow
                                 }).OrderBy(s => s.Id)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToList();
            return listBranModel;
        }

        public bool Add(BrandModel model)
        {
            try
            {
                model.Id = GetNewId();
                model.BrandId = model.BrandId;
                model.ModelCode = model.ModelCode;
                model.Remarks = model.Remarks;
                _vssDb.BrandModels.Add(model);
                _vssDb.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(BrandModel model)
        {
            try
            {
                var oBM = _vssDb.BrandModels
                 .Where(x => x.Id == model.Id)
                 .FirstOrDefault();
                if (oBM != null)
                {
                    oBM.Id = model.Id;
                    oBM.BrandId = model.BrandId;
                    oBM.ModelCode = model.ModelCode;
                    oBM.Remarks = model.Remarks;
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
                var selectedBrandModel = _vssDb.BrandModels
                 .Where(x => x.Id == id)
                 .FirstOrDefault();
                if (selectedBrandModel != null)
                {
                    _vssDb.BrandModels.Remove(selectedBrandModel);
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
                var Id = Convert.ToInt32(_vssDb.BrandModels.Max(x => x.Id)) + 1;
                return Id;
            }
            catch
            {
                return 0;
            }
        }
    }
}