using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using AspNetWebApi.DA.ADO;
using AspNetWebApi.DA.EF.VssDb;
using AspNetWebApi.DA.ViewModels.Operation;
using AspNetWebApi.DA.ViewModels.Stores;

namespace AspNetWebApi.BL.Stores
{
    public class BrandBL
    {
        ModelVssDb _vssDb = new ModelVssDb();
        public IEnumerable<BrandVM> Get(int pageIndex, int pageSize)
        {
            int nRow = _vssDb.Brands.Count();
            var listBrand = _vssDb.Brands
                .Select(x => new BrandVM
                {
                    Id = x.Id,
                    Code = x.Code,
                    Name = x.Name,
                    Country = x.Country,
                    Remarks = x.Remarks,

                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    RowCount = nRow
                })
                .OrderByDescending(s => s.Id)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToList();
            return listBrand;
        }

        public bool Add(Brand model)
        {
            try
            {
                model.Id = GetNewId();
                model.Code = model.Code;
                model.Name = model.Name;
                model.Country = model.Country;
                model.Remarks = model.Remarks;
                model.CreateDate = DateTime.Now;
                model.CreateBy = model.CreateBy;
                _vssDb.Brands.Add(model);
                _vssDb.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(Brand model)
        {
            try
            {
                var oB = _vssDb.Brands
                 .Where(x => x.Id == model.Id)
                 .FirstOrDefault();
                if (oB != null)
                {
                    oB.Id = model.Id;
                    oB.Name = model.Name;
                    oB.Country = model.Country;
                    oB.Remarks = model.Remarks;
                    oB.UpdateBy = model.UpdateBy;
                    oB.UpdateDate = DateTime.Now;
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
                var selectedBrand = _vssDb.Brands
                 .Where(x => x.Id == id)
                 .FirstOrDefault();
                if (selectedBrand != null)
                {
                    _vssDb.Brands.Remove(selectedBrand);
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

        public IEnumerable<ItemCategoryVM> GetItemCategory()
        {
            _vssDb = new ModelVssDb();
            var listItemCategory = _vssDb.ItemCategories
                .Select(x => new ItemCategoryVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    Code = x.Code
                }).OrderBy(s => s.Name).ToList();
            return listItemCategory;
        }

        public IEnumerable<BrandModelVM> GetBrandModel(int brandId)
        {
            _vssDb = new ModelVssDb();
            var listBrandModel = _vssDb.BrandModels.Where(y=>y.BrandId== brandId)
                .Select(x => new BrandModelVM
                {
                    Id = x.Id,
                    Remarks = x.Remarks,
                    BrandId = x.BrandId,
                    ModelCode = x.ModelCode
                }).OrderBy(s => s.ModelCode).ToList();
            return listBrandModel;
        }

        private int GetNewId()
        {
            try
            {
                var Id = Convert.ToInt32(_vssDb.Brands.Max(x => x.Id)) + 1;
                return Id;
            }
            catch
            {
                return 1;
            }
        }

        public IEnumerable<BrandVM> GetBrand()
        {
            _vssDb = new ModelVssDb();
            var listBrand = _vssDb.Brands
                .Select(x => new BrandVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    Code = x.Code
                }).OrderBy(s => s.Name).ToList();
            return listBrand;
        }

    }
}