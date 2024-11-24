using Microsoft.Ajax.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Http;
using AspNetWebApi.BL.Stores;
using AspNetWebApi.DA.ADO;
using AspNetWebApi.DA.EF.VssDb;
using AspNetWebApi.DA.Utilities;
using AspNetWebApi.DA.ViewModels.Billing;
using AspNetWebApi.DA.ViewModels.Operation;
using AspNetWebApi.DA.ViewModels.Purchase;

namespace AspNetWebApi.BL.Purchase
{
    public class PoBL
    {
        ModelVssDb _vssDb = null;
        private IGenericFactory<PoVm> Generic_PoVm = null;
        public bool Add(PoVm model)
        {
            using (_vssDb = new ModelVssDb())
            {
                using (var _tran = _vssDb.Database.BeginTransaction())
                {
                    try
                    {
                        var oPO = _vssDb.POes.Where(x=>x.PoId == model.PoId).FirstOrDefault();
                        if (oPO != null)
                        {
                            _tran.Rollback();
                            return false;
                        }
                        else
                        {
                            #region Purchase
                            oPO = new PO();
                            oPO.TotalAmount = model.TotalAmount;
                            oPO.PoDate = model.PoDate;
                            oPO.CreateBy = model.CreateBy;
                            oPO.CreateDate = DateTime.Now;
                            oPO.PoDate = model.PoDate;
                            oPO.IsActive = true;
                            oPO.SupplierId = model.SupplierId;
                            _vssDb.POes.Add(oPO);
                            _vssDb.SaveChanges();
                            model.PoId = oPO.PoId;
                            #endregion
                            #region Purchase Item
                            var listPoItemRem = _vssDb.PoItems.Where(x => x.PoId == model.PoId).ToList();
                            _vssDb.PoItems.RemoveRange(listPoItemRem);
                            _vssDb.SaveChanges();
                            List<PoItem> listPoItem = new List<PoItem>();
                            foreach (var poItemVm in model.PoItems)
                            {
                                PoItem oPoItem = new PoItem();
                                oPoItem.PoId = model.PoId;
                                oPoItem.ItemId = poItemVm.ItemId;
                                oPoItem.Qty = poItemVm.Qty;
                                oPoItem.Vat = poItemVm.Vat;
                                oPoItem.UnitPrice = poItemVm.UnitPrice;
                                oPoItem.Tax = poItemVm.Tax;
                                oPoItem.TotalPrice = poItemVm.UnitPrice * poItemVm.Qty;
                                oPoItem.CreateBy = model.CreateBy;
                                oPoItem.CreateDate = DateTime.Now;
                                oPoItem.PoDate = poItemVm.PoDate;
                                oPoItem.SupplierId = poItemVm.SupplierId;
                                oPoItem.TotalAmount = poItemVm.TotalPrice;
                                
                                listPoItem.Add(oPoItem);
                            }
                            _vssDb.PoItems.AddRange(listPoItem);
                            _vssDb.SaveChanges();
                            #endregion
                            _tran.Commit();
                            return true;
                        }
                    }
                    catch
                    {
                        _tran.Rollback();
                        return false;
                    }
                }
            }
        }

        public bool Update(PoVm model)
        {
            using (_vssDb = new ModelVssDb())
            {
                using (var _tran = _vssDb.Database.BeginTransaction())
                {
                    try
                    {
                        #region Validation
                        //PaySettle oPaySettle = (from x in _vssDb.PaySettles where x.InvoiceId == model.Id select x).FirstOrDefault();
                        //if (oPaySettle != null)
                        //{
                        //    _tran.Rollback();
                        //    return false;
                        //}
                        #endregion
                        var oPO = _vssDb.POes.Where(x => x.PoId == model.PoId).FirstOrDefault();
                        if (oPO == null)
                        {
                            _tran.Rollback();
                            return false;
                        }
                        else
                        {
                            #region Purchase
                            oPO = new PO();
                            oPO.TotalAmount = model.TotalAmount;
                            oPO.PoDate = model.PoDate;
                            oPO.UpdateBy = model.UpdateBy;
                            oPO.UpdateDate = DateTime.Now;
                            oPO.PoDate = model.PoDate;
                            oPO.IsActive = true;
                            oPO.SupplierId = model.SupplierId;
                            _vssDb.SaveChanges();
                            model.PoId = oPO.PoId;
                            #endregion
                            #region Purchase Item
                            var listPoItemRem = _vssDb.PoItems.Where(x => x.PoId == model.PoId).ToList();
                            _vssDb.PoItems.RemoveRange(listPoItemRem);
                            _vssDb.SaveChanges();
                            List<PoItem> listPoItem = new List<PoItem>();
                            foreach (var poItemVm in model.PoItems)
                            {
                                PoItem oPoItem = new PoItem();
                                oPoItem.PoId = model.PoId;
                                oPoItem.ItemId = poItemVm.ItemId;
                                oPoItem.Qty = poItemVm.Qty;
                                oPoItem.Vat = poItemVm.Vat;
                                oPoItem.UnitPrice = poItemVm.UnitPrice;
                                oPoItem.Tax = poItemVm.Tax;
                                oPoItem.TotalPrice = poItemVm.UnitPrice * poItemVm.Qty;
                                oPoItem.CreateBy = model.CreateBy;
                                oPoItem.CreateDate = DateTime.Now;
                                oPoItem.PoDate = poItemVm.PoDate;
                                oPoItem.SupplierId = poItemVm.SupplierId;
                                oPoItem.TotalAmount = poItemVm.TotalPrice;

                                listPoItem.Add(oPoItem);
                            }
                            _vssDb.PoItems.AddRange(listPoItem);
                            _vssDb.SaveChanges();
                            #endregion
                            _tran.Commit();
                            return true;
                        }
                    }
                    catch
                    {
                        _tran.Rollback();
                        return false;
                    }
                }
            }
        }

        //(int pageIndex = 0, int pageSize = 5, DateTime? StartDate = null, DateTime? EndDate = null)
        public IEnumerable<PoVm> Get(int pageIndex = 0, int pageSize = 5, DateTime? StartDate = null, DateTime? EndDate = null)
        {
            string vssDb = ConfigurationManager.ConnectionStrings["VssDb"].ConnectionString;
            Generic_PoVm = new GenericFactory<PoVm>();
            var oHashTable = new Hashtable()
            {
                { "PageIndex", pageIndex },
                { "PageSize", pageSize },
                { "StartDate", StartDate },
                { "EndDate", EndDate }
            };
            return Generic_PoVm.ExecuteCommandList(CommandType.StoredProcedure, StoredProcedure.sp_GetPo, oHashTable, vssDb);
        }

        public PoVm Get([FromUri] long poid)
        {
            string vssDb = ConfigurationManager.ConnectionStrings["VssDb"].ConnectionString;
            Generic_PoVm = new GenericFactory<PoVm>();
            var oHashTable = new Hashtable()
            {
                { "PoId", poid }
            };
            return Generic_PoVm.ExecuteCommandList(CommandType.StoredProcedure, StoredProcedure.sp_GetPoById, oHashTable, vssDb).FirstOrDefault();
        }

        public bool Remove(long poid)
        {
            bool isSuccess = false;
            using (_vssDb = new ModelVssDb())
            {
                using (var _tran = _vssDb.Database.BeginTransaction())
                {
                    try
                    {
                        var oPO = _vssDb.POes.Where(x => x.PoId == poid).FirstOrDefault();
                        if (oPO != null)
                        {
                            var listPoItem = _vssDb.PoItems.Where(x => x.PoId == poid).ToList();

                            _vssDb.PoItems.RemoveRange(listPoItem);
                            _vssDb.SaveChanges();

                            _vssDb.POes.Remove(oPO);
                            _vssDb.SaveChanges();

                            _tran.Commit();
                            isSuccess = true;
                        }
                    }
                    catch
                    {
                        _tran.Rollback();
                        isSuccess = false;
                    }
                }
            }
            return isSuccess;
        }


    }
}