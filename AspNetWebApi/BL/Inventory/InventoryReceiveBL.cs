using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Drawing.Printing;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using AspNetWebApi.DA.ADO;
using AspNetWebApi.DA.EF.VssDb;
using AspNetWebApi.DA.Utilities;
using AspNetWebApi.DA.ViewModels.Inventory;
using static AspNetWebApi.DA.Utilities.BLStatus;

namespace AspNetWebApi.BL.Inventory
{
    public class InventoryReceiveBL
    {
        //ModelVssDb _vssDb = null;
        //List<StoreReq> listEngineSize = null;
        private IGenericFactory<InventoryReceiveVM> Generic_InventoryReceive = null;

        /*public bool ApproveReqStoreTran(StoreTran model)
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
        }*/

        #region CRUD
        public List<InventoryReceiveVM> Get(int requisitionTypeId, int pageIndex, int pageSize)
        {
            string VssDb = ConfigurationManager.ConnectionStrings["VssDb"].ConnectionString;
            Generic_InventoryReceive = new GenericFactory<InventoryReceiveVM>();
            Hashtable hashTable = new Hashtable()
            {
                { "QueryOption", Enams.QueryOption.GetAll},
                { "PageIndex", pageIndex },
                { "PageSize", pageSize },
                { "requisitionTypeId", requisitionTypeId }
            };
            return Generic_InventoryReceive.ExecuteCommandList(CommandType.StoredProcedure, StoredProcedure.sp_GetInventoryReceive, hashTable, VssDb);
        }

        public bool Create(InventoryReceiveVM viewModel)
        {
            bool isSuccess = false;
            string VssDb = ConfigurationManager.ConnectionStrings["VssDb"].ConnectionString;
            Generic_InventoryReceive = new GenericFactory<InventoryReceiveVM>();
            Hashtable hashTable = new Hashtable()
            {
                { "QueryOption", Enams.QueryOption.Insert},
                { "InventoryReceiveId", viewModel.InventoryReceiveId },
                { "ReferenceId", viewModel.ReferenceId },
                { "WareHouseId", viewModel.WareHouseId },
                { "ItemId", viewModel.ItemId },
                { "Quantity", viewModel.Quantity },
                { "InventoryReceiveTypeId", viewModel.InventoryReceiveTypeId },
                { "Remark", viewModel.Remark },
                { "CreateBy", viewModel.CreateBy }
            };
            var result = Generic_InventoryReceive.ExecuteCommandObject(CommandType.StoredProcedure, StoredProcedure.sp_GetInventoryReceive, hashTable, VssDb);
            return isSuccess;
        }

        public bool Update(InventoryReceiveVM viewModel)
        {
            bool isSuccess = false;
            string VssDb = ConfigurationManager.ConnectionStrings["VssDb"].ConnectionString;
            Generic_InventoryReceive = new GenericFactory<InventoryReceiveVM>();
            Hashtable hashTable = new Hashtable()
            {
                { "QueryOption", Enams.QueryOption.Update},
                { "InventoryReceiveId", viewModel.InventoryReceiveId },
                { "ReferenceId", viewModel.ReferenceId },
                { "WareHouseId", viewModel.WareHouseId },
                { "ItemId", viewModel.ItemId },
                { "Quantity", viewModel.Quantity },
                { "InventoryReceiveTypeId", viewModel.InventoryReceiveTypeId },
                { "Remark", viewModel.Remark },
                { "UpdateBy", viewModel.UpdateBy }
            };
            var result = Generic_InventoryReceive.ExecuteCommandObject(CommandType.StoredProcedure, StoredProcedure.sp_GetInventoryReceive, hashTable, VssDb);
            return isSuccess;
        }

        public bool Delete(InventoryReceiveVM viewModel)
        {
            bool isSuccess = false;
            string VssDb = ConfigurationManager.ConnectionStrings["VssDb"].ConnectionString;
            Generic_InventoryReceive = new GenericFactory<InventoryReceiveVM>();
            Hashtable hashTable = new Hashtable()
            {
                { "QueryOption", Enams.QueryOption.Delete},
                { "InventoryReceiveId", viewModel.InventoryReceiveId },
                { "UpdateBy", viewModel.DeleteBy }
            };
            var result = Generic_InventoryReceive.ExecuteCommandObject(CommandType.StoredProcedure, StoredProcedure.sp_GetInventoryReceive, hashTable, VssDb);
            return isSuccess;
        }
        #endregion

        /*public bool Remove(int id)
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
        }*/

        /*public bool Update(StoreTran model)
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
        }*/
    }
}