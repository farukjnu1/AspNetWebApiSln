using Google.Protobuf.WellKnownTypes;
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

namespace AspNetWebApi.BL.Stores
{
    public class SalesPriceBL
    {
        ModelVssDb _vssDb = null;
        private IGenericFactory<SalesPriceVM> Generic_SalesPriceVM = null;
        string VssDb = "";

        public SalesPriceBL()
        {
            _vssDb = new ModelVssDb();
            Generic_SalesPriceVM = new GenericFactory<SalesPriceVM>();
            VssDb = ConfigurationManager.ConnectionStrings["VssDb"].ConnectionString;
        }

        public List<SalesPriceVM> Get(string partNo, int pageIndex = 0, int pageSize = 10)
        {
            var oHashTable = new Hashtable() { { "partNo", partNo } };
            return Generic_SalesPriceVM.ExecuteCommandList(CommandType.StoredProcedure, StoredProcedure.sp_GetSalesPrice, oHashTable, VssDb);
        }

        /*public IEnumerable<SalesPriceVM> Get(string partNo,int pageIndex = 0, int pageSize = 10)
        {
            int nRow = (from i in _vssDb.Items
                        join sp in _vssDb.SalesPrices on i.Id equals sp.ItemId
                        join b in _vssDb.Brands on i.BrandId equals b.Id
                        join bm in _vssDb.BrandModels on i.ModelId equals bm.Id
                        where i.PartNoOld == (string.IsNullOrEmpty(partNo) ? i.PartNoOld : partNo)
                        || i.PartNoNew == (string.IsNullOrEmpty(partNo) ? i.PartNoNew : partNo)
                        select sp).Count();
            var listItem = (from i in _vssDb.Items
                            join sp in _vssDb.SalesPrices on i.Id equals sp.ItemId
                            join b in _vssDb.Brands on i.BrandId equals b.Id
                            join bm in _vssDb.BrandModels on i.ModelId equals bm.Id
                            where i.PartNoOld == (string.IsNullOrEmpty(partNo) ? i.PartNoOld : partNo)
                            || i.PartNoNew == (string.IsNullOrEmpty(partNo) ? i.PartNoNew : partNo)
                            select new SalesPriceVM
                            {
                                Id = sp.Id,
                                ItemId = i.Id,
                                ItemCode = i.ItemCode,
                                ItemName = i.ItemName,
                                PartNoOld = i.PartNoOld,
                                PartNoNew = i.PartNoNew,
                                Remarks = i.Remarks,
                                BrandId = i.BrandId,
                                BrandName = b.Name,
                                ModelId = i.ModelId,
                                ModelCode = bm.ModelCode,
                                SalePrice = sp.SalePrice,
                                AvgPurchasePrice = sp.AvgPurchasePrice,
                                MinPurchasePrice = sp.MinPurchasePrice,
                                MaxPurchasePrice = sp.MaxPurchasePrice,
                                CreateDate = sp.CreateDate,
                                PageIndex = pageIndex,
                                PageSize = pageSize,
                                RowCount = nRow
                            })
                .OrderByDescending(x => x.ItemId)
                .OrderByDescending(y => y.Id)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToList();
            return listItem;
        }*/

        public bool Add(SalesPriceVM model)
        {
            bool isSuccess = false;
            using (_vssDb = new ModelVssDb())
            {
                using (var _tran = _vssDb.Database.BeginTransaction())
                {
                    try
                    {
                        _vssDb = new ModelVssDb();
                        var listSalePrice = (from x in _vssDb.SalesPrices where x.ItemId == model.ItemId && x.IsActive == true select x).ToList();
                        foreach (var item in listSalePrice)
                        {
                            item.IsActive = false;
                            _vssDb.SaveChanges();
                        }
                        var oSalePrice = new SalesPrice();
                        oSalePrice.CreateDate = DateTime.Now;
                        oSalePrice.CreateBy = model.CreateBy;
                        oSalePrice.IsActive = true;
                        oSalePrice.AvgPurchasePrice = model.AvgPurchasePrice;
                        oSalePrice.MinPurchasePrice = model.MinPurchasePrice;
                        oSalePrice.MaxPurchasePrice = model.MaxPurchasePrice;
                        oSalePrice.SalePrice = model.SalePrice;
                        oSalePrice.ItemId = model.ItemId;
                        oSalePrice.Remarks = model.Remarks;
                        _vssDb.SalesPrices.Add(oSalePrice);
                        _vssDb.SaveChanges();
                        _tran.Commit();
                        isSuccess = true;
                    }
                    catch
                    {
                        _tran.Rollback();
                    }
                }
            }
            return isSuccess;
        }

        public bool Update(SalesPriceVM model)
        {
            return false;
        }

        public bool Remove(int id)
        {
            return false;
        }
    }
}