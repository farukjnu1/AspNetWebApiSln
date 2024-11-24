using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing.Printing;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using AspNetWebApi.DA.ADO;
using AspNetWebApi.DA.EF.VssDb;
using AspNetWebApi.DA.Utilities;
using AspNetWebApi.DA.ViewModels.Operation;
using AspNetWebApi.DA.ViewModels.Stores;

namespace AspNetWebApi.BL.Stores
{
    public class StoreTranBL
    {
        ModelVssDb _vssDb = null;
        //List<StoreReq> listEngineSize = null;
        private IGenericFactory<StoreReqVM> Generic_StoreReq = null;

        public List<StoreReqVM> Get(int reqStatus, int storeTranTypeId, int pageIndex = 0, int pageSize = 10)
        {
            string vssDb = ConfigurationManager.ConnectionStrings["VssDb"].ConnectionString;
            Generic_StoreReq = new GenericFactory<StoreReqVM>();
            return Generic_StoreReq.ExecuteCommandList(CommandType.StoredProcedure, StoredProcedure.sp_GetStoreReq, new Hashtable() { { "PageIndex", pageIndex }, { "PageSize", pageSize }, { "ReqStatus", reqStatus }, { "StoreTranTypeId", storeTranTypeId } }, vssDb);
        }

        public StoreTranVM GetRecByReqId(int reqid)
        {
            _vssDb = new ModelVssDb();
            var oStoreRec = (from x in _vssDb.StoreTrans
                             where x.ReqId == reqid
                             select new StoreTranVM
                             {
                                 ReqId = x.ReqId,
                                 BusinessPartnerId = x.BusinessPartnerId,
                                 CreateBy = x.CreateBy,
                                 Id = x.Id,
                                 ItemId = x.ItemId,
                                 PurchasePrice = x.PurchasePrice,
                                 Qty = x.Qty,
                                 Remark = x.Remark,
                                 StoreRecTypeId = x.StoreTranTypeId,
                                 WhId = x.WhId,
                             }).FirstOrDefault();
            return oStoreRec;
        }

        public bool ApproveReqStoreTran(StoreTran model)
        {
            bool isSuccess = false;
            using (_vssDb = new ModelVssDb())
            {
                using (var _tran = _vssDb.Database.BeginTransaction())
                {
                    try
                    {
                        var oStoreReq = (from x in _vssDb.StoreReqs where x.Id == model.ReqId select x).FirstOrDefault();
                        if (oStoreReq != null)
                        {
                            oStoreReq.ReqStatus = 2;
                        }
                        model.CreateDate = DateTime.Now;
                        _vssDb.StoreTrans.Add(model);
                        _vssDb.SaveChanges();
                        #region Add / Update Stock
                        var oStock = (from x in _vssDb.Stocks where x.ItemId == model.ItemId select x).FirstOrDefault();
                        if (oStock == null)
                        {
                            oStock = new Stock();
                            oStock.ItemId = model.ItemId;
                            oStock.WareHouseId = (int)model.WhId;
                            oStock.Quantity = model.Qty;
                            _vssDb.Stocks.Add(oStock);
                            _vssDb.SaveChanges();
                        }
                        else
                        {
                            oStock.ItemId = model.ItemId;
                            oStock.WareHouseId = (int)model.WhId;
                            oStock.Quantity += model.Qty;
                            _vssDb.SaveChanges();
                        }
                        #endregion
                        _tran.Commit();
                        isSuccess = true;
                    }
                    catch (Exception ex)
                    {
                        _tran.Rollback();
                    }
                }
            }
            return isSuccess;
        }

        public bool Update(StoreTran model)
        {
            bool isSuccess = false;
            using (_vssDb = new ModelVssDb())
            {
                using (var _tran = _vssDb.Database.BeginTransaction())
                {
                    try
                    {
                        var oStoreReq = (from x in _vssDb.StoreReqs where x.Id == model.ReqId select x).FirstOrDefault();
                        if (oStoreReq != null)
                        {
                            oStoreReq.ReqStatus = 2;
                        }
                        var oStoreRec = (from x in _vssDb.StoreTrans where x.Id == model.Id select x).FirstOrDefault();
                        if(oStoreRec != null) 
                        {
                            #region Reverse Stock
                            var oStockRE = (from x in _vssDb.Stocks where x.ItemId == model.ItemId select x).FirstOrDefault();
                            if (oStockRE != null)
                            {
                                if (oStockRE.Quantity >= oStoreRec.Qty) // during reverse stock qty should be greater than or equal to Store-Receive
                                {
                                    oStockRE.Quantity -= oStoreRec.Qty;
                                    _vssDb.SaveChanges();
                                }
                                else 
                                {
                                    _tran.Rollback();
                                    return false;
                                }
                                oStoreRec.Remark = model.Remark;
                                oStoreRec.BusinessPartnerId = model.BusinessPartnerId;
                                oStoreRec.StoreTranTypeId = model.StoreTranTypeId;
                                oStoreRec.ReqId = model.ReqId;
                                oStoreRec.WhId = model.WhId;
                                oStoreRec.Qty = model.Qty;
                                oStoreRec.ItemId = model.ItemId;
                                oStoreRec.PurchasePrice = model.PurchasePrice;
                                oStoreRec.UpdateDate = DateTime.Now;
                                oStoreRec.UpdateBy = model.CreateBy;
                                oStoreRec.DeliveryTime = model.DeliveryTime;
                                _vssDb.SaveChanges();
                                #region Update Stock
                                var oStockUP = (from x in _vssDb.Stocks where x.ItemId == model.ItemId select x).FirstOrDefault();
                                if (oStockUP != null)
                                {
                                    oStockUP.ItemId = model.ItemId;
                                    oStockUP.WareHouseId = (int)model.WhId;
                                    oStockUP.Quantity += model.Qty;
                                    _vssDb.SaveChanges();
                                }
                                #endregion
                                _tran.Commit();
                                isSuccess = true;
                            }
                            #endregion
                        }
                    }
                    catch (Exception ex)
                    {
                        _tran.Rollback();
                    }
                }
            }
            return isSuccess;
        }

        public bool Remove(int id)
        {
            bool isSuccess = false;
            using (_vssDb = new ModelVssDb())
            {
                using (var _tran = _vssDb.Database.BeginTransaction())
                {
                    try
                    {
                        var oStoreRec = (from x in _vssDb.StoreTrans where x.Id == id select x).FirstOrDefault();
                        if (oStoreRec != null)
                        {
                            #region Reverse Stock
                            var oStockRE = (from x in _vssDb.Stocks where x.ItemId == oStoreRec.ItemId select x).FirstOrDefault();
                            if (oStockRE != null)
                            {
                                if (oStockRE.Quantity >= oStoreRec.Qty) // during reverse stock qty should be greater than or equal to Store-Receive
                                {
                                    oStockRE.Quantity -= oStoreRec.Qty;
                                    _vssDb.SaveChanges();
                                }
                                else
                                {
                                    _tran.Rollback();
                                    return false;
                                }
                                _vssDb.StoreTrans.Remove(oStoreRec);
                                _vssDb.SaveChanges();
                                _tran.Commit();
                                isSuccess = true;
                            }
                            #endregion
                        }
                    }
                    catch (Exception ex)
                    {
                        _tran.Rollback();
                    }
                }
            }
            return isSuccess;
        }
    }
}