using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNetWebApi.DA.EF.VssDb;
using AspNetWebApi.DA.ViewModels.Operation;
using AspNetWebApi.DA.ViewModels.Stores;

namespace AspNetWebApi.BL.Stores
{
    public class WarehouseBL
    {
        ModelVssDb _vssDb = new ModelVssDb();

        public IEnumerable<WarehouseVM> Get(int pageIndex = 0, int pageSize = 5)
        {
            int nRow = _vssDb.Warehouses.Count();
            var listWarehouse = _vssDb.Warehouses
                .Select(x => new WarehouseVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    RowCount = nRow
                })
                .OrderByDescending(s => s.Id)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToList();
            return listWarehouse;
        }

        public bool Update(Warehouse model)
        {
            try
            {
                var selectedWarehouse = _vssDb.Warehouses
                 .Where(x => x.Id == model.Id).FirstOrDefault();
                if (selectedWarehouse != null)
                {
                    selectedWarehouse.Name = model.Name;
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

        public IEnumerable<WarehouseVM> GetWarehouse()
        {
            var listWarehouse = _vssDb.Warehouses
                .Select(x => new WarehouseVM
                {
                    Id = x.Id,
                    Name = x.Name
                }).OrderBy(s => s.Name).ToList();
            return listWarehouse;
        }
    }
}