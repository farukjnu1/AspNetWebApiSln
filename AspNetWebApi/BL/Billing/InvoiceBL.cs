using Microsoft.Ajax.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Http;
using AspNetWebApi.BL.Stores;
using AspNetWebApi.DA.ADO;
using AspNetWebApi.DA.EF.VssDb;
using AspNetWebApi.DA.Utilities;
using AspNetWebApi.DA.ViewModels.Billing;
using AspNetWebApi.DA.ViewModels.Operation;

namespace AspNetWebApi.Billing
{
    public class InvoiceBL
    {
        ModelVssDb _vssDb = null;
        private IGenericFactory<JobCardVM> Generic_JobCardVM = null;
        public bool Add(InvoiceVM model)
        {
            using (_vssDb = new ModelVssDb())
            {
                using (var _tran = _vssDb.Database.BeginTransaction())
                {
                    try
                    {
                        var oI = _vssDb.Invoices.Where(x=>x.JcId == model.JcId).FirstOrDefault();
                        if (oI != null)
                        {
                            _tran.Rollback();
                            return false;
                        }
                        else
                        {
                            #region Invoice
                            Invoice oInvoice = new Invoice();
                            oInvoice.ClientId = model.ClientId;
                            oInvoice.JcId = model.JcId;
                            oInvoice.CreateBy = model.CreateBy;
                            oInvoice.CreateDate = DateTime.Now;
                            oInvoice.GrandTotal = model.GrandTotal;
                            oInvoice.CreateBy = model.CreateBy;
                            _vssDb.Invoices.Add(oInvoice);
                            _vssDb.SaveChanges();
                            model.Id = oInvoice.Id;
                            #endregion
                            #region Invoice Item
                            var listIIRem = _vssDb.InvoiceItems.Where(x => x.InvoiceId == model.Id).ToList();
                            _vssDb.InvoiceItems.RemoveRange(listIIRem);
                            _vssDb.SaveChanges();
                            List<InvoiceItem> listInvoiceItem = new List<InvoiceItem>();
                            foreach (var oII in model.InvoiceItems)
                            {
                                InvoiceItem oInvoiceItem = new InvoiceItem();
                                oInvoiceItem.InvoiceId = model.Id;
                                oInvoiceItem.ItemId = oII.ItemId;
                                oInvoiceItem.ItemType = oII.ItemType;
                                oInvoiceItem.Qty = oII.Qty;
                                oInvoiceItem.UnitPrice = oII.UnitPrice;
                                oInvoiceItem.Tax = oII.Tax;
                                oInvoiceItem.TotalPrice = oII.TotalPrice;
                                oInvoiceItem.Discount = oII.Discount;
                                oInvoiceItem.DiscountAmount = oII.DiscountAmount;
                                oInvoiceItem.TpAfterDiscount = oII.TpAfterDiscount;
                                oInvoiceItem.Vat = oII.Vat;
                                oInvoiceItem.TotalVat = oII.TotalVat;
                                oInvoiceItem.TotalAmount = oII.TotalAmount;
                                oInvoiceItem.DoneBy = model.CreateBy;
                                oInvoiceItem.DoneDate = DateTime.Now;
                                listInvoiceItem.Add(oInvoiceItem);
                            }
                            _vssDb.InvoiceItems.AddRange(listInvoiceItem);
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

        public bool Update(InvoiceVM model)
        {
            using (_vssDb = new ModelVssDb())
            {
                using (var _tran = _vssDb.Database.BeginTransaction())
                {
                    try
                    {
                        #region Validation
                        PaySettle oPaySettle = (from x in _vssDb.PaySettles where x.InvoiceId == model.Id select x).FirstOrDefault();
                        if (oPaySettle != null)
                        {
                            _tran.Rollback();
                            return false;
                        }
                        #endregion
                        var oInvoice = _vssDb.Invoices.Where(x => x.JcId == model.JcId).FirstOrDefault();
                        if (oInvoice == null)
                        {
                            _tran.Rollback();
                            return false;
                        }
                        else
                        {
                            #region Invoice
                            oInvoice.ClientId = model.ClientId;
                            oInvoice.JcId = model.JcId;
                            oInvoice.CreateBy = model.CreateBy;
                            oInvoice.CreateDate = DateTime.Now;
                            oInvoice.GrandTotal = model.GrandTotal;
                            oInvoice.CreateBy = model.CreateBy;
                            _vssDb.SaveChanges();
                            model.Id = oInvoice.Id;
                            #endregion
                            #region Invoice Item
                            var listIIRem = _vssDb.InvoiceItems.Where(x => x.InvoiceId == model.Id).ToList();
                            _vssDb.InvoiceItems.RemoveRange(listIIRem);
                            _vssDb.SaveChanges();
                            List<InvoiceItem> listInvoiceItem = new List<InvoiceItem>();
                            foreach (var oII in model.InvoiceItems)
                            {
                                InvoiceItem oInvoiceItem = new InvoiceItem();
                                oInvoiceItem.InvoiceId = model.Id;
                                oInvoiceItem.ItemId = oII.ItemId;
                                oInvoiceItem.ItemType = oII.ItemType;
                                oInvoiceItem.Qty = oII.Qty;
                                oInvoiceItem.UnitPrice = oII.UnitPrice;
                                oInvoiceItem.Tax = oII.Tax;
                                oInvoiceItem.TotalPrice = oII.TotalPrice;
                                oInvoiceItem.Discount = oII.Discount;
                                oInvoiceItem.DiscountAmount = oII.DiscountAmount;
                                oInvoiceItem.TpAfterDiscount = oII.TpAfterDiscount;
                                oInvoiceItem.Vat = oII.Vat;
                                oInvoiceItem.TotalVat = oII.TotalVat;
                                oInvoiceItem.TotalAmount = oII.TotalAmount;
                                oInvoiceItem.DoneBy = model.CreateBy;
                                oInvoiceItem.DoneDate = DateTime.Now;
                                listInvoiceItem.Add(oInvoiceItem);
                            }
                            _vssDb.InvoiceItems.AddRange(listInvoiceItem);
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

        //(int pageIndex = 0, int pageSize = 5, int jcStatus = 0, string JcNo = "", DateTime? StartDate = null, DateTime? EndDate = null)
        public IEnumerable<JobCardVM> Get(int pageIndex = 0, int pageSize = 5, int jcStatus = 1, bool isPaid = false, string JcNo = "", DateTime? StartDate = null, DateTime? EndDate = null)
        {
            string vssDb = ConfigurationManager.ConnectionStrings["VssDb"].ConnectionString;
            Generic_JobCardVM = new GenericFactory<JobCardVM>();
            var oHashTable = new Hashtable()
            {
                { "PageIndex", pageIndex },
                { "PageSize", pageSize },
                { "JcStatus", jcStatus },
                { "JcNo", JcNo },
                { "StartDate", StartDate },
                { "EndDate", EndDate },
                { "IsPaid", isPaid }
            };
            var list = Generic_JobCardVM.ExecuteCommandList(CommandType.StoredProcedure, StoredProcedure.sp_GetJcInvoice, oHashTable, vssDb);
            return list;
        }

        public InvoiceVM GetByJc([FromUri] int jcId)
        {
            _vssDb = new ModelVssDb();
            InvoiceVM oInvoice = null;
            oInvoice = _vssDb.Database.SqlQuery<InvoiceVM>(@"SELECT ISNULL(I.Id,0) Id
            ,ISNULL(JC.Id,0) JcId
            ,CASE WHEN I.JcId IS NULL OR I.JcId = 0 THEN 0 ELSE 1 END IsInvoice
            ,ISNULL(I.CreateBy,0) CreateBy
            ,ISNULL(I.CreateDate,'') CreateDate
            ,ISNULL(I.GrandTotal,0) GrandTotal
            ,ISNULL(JC.JcNo,'') JcNo
            ,ISNULL(JC.ClientId,0) ClientId 
            ,ISNULL(JC.ClientInfo,'') Description 
            ,ISNULL(C.Address,'') ClientAddress
            ,ISNULL(C.Email,'') ClientEmail
            ,ISNULL(C.Name,'') ClientName
            ,ISNULL(C.Phone,'') ClientPhone
            ,ISNULL(C.MembershipNo,'') MembershipNo                            
            ,ISNULL(JC.ContactPerson,'') ContactPerson
            ,ISNULL(JC.ContactPersonNo,'') ContactPersonNo
            ,ISNULL(CV.VehicleNo,'') VehicleNo
            ,ISNULL(CV.Vin,'') Vin
            ,ISNULL(CV.Model,'') Model
            ,ISNULL(CV.SubModel,'') SubModel
            ,ISNULL(E.FirstName,'') + ' ' + ISNULL(e.MiddleName,'') + ' ' + ISNULL(e.LastName,'') Supervisor
            ,ISNULL(JC.Mileage,0) Mileage
            FROM Invoice I
            LEFT JOIN BusinessPartner C ON C.BpId = I.ClientId
			LEFT JOIN JobCard JC ON JC.Id = I.JcId
            LEFT JOIN ClientVehicle CV ON CV.ClientId = I.ClientId AND RTRIM(LTRIM(JC.VehicleNo))=RTRIM(LTRIM(CV.VehicleNo))
            LEFT JOIN Employee E ON E.EmployeeId = JC.SupervisorId
            WHERE I.JcId=" + jcId).FirstOrDefault();
            if (oInvoice != null)
            {
                oInvoice.InvoiceItems = _vssDb.Database.SqlQuery<InvoiceItemVM>(@"SELECT
                                                II.Id,
                                                II.ItemId,
                                                II.ItemType,
                                                II.Qty,
                                                II.UnitPrice,
                                                II.Tax,
                                                II.DoneBy,
                                                II.DoneDate,
                                                II.TotalPrice,
                                                II.Discount,
                                                II.DiscountAmount,
                                                II.TpAfterDiscount,
                                                II.Vat,
                                                II.TotalVat,
                                                II.TotalAmount ,
                                                I.ItemName SpareParts,
	                                            J.Description JobName,
	                                            CASE WHEN I.ItemName IS NULL THEN J.Description WHEN J.Description IS NULL THEN I.ItemName ELSE '' END ItemName
                                                FROM InvoiceItem II
                                                LEFT JOIN Item I ON I.Id = II.ItemId AND II.ItemType=2
                                                LEFT JOIN Job J ON J.JobId = II.ItemId AND II.ItemType=1
                                                WHERE II.InvoiceId=" + oInvoice.Id + @" ORDER BY II.ItemType").ToList();
                oInvoice.BalanceAmount = (from b in _vssDb.BusinessPartnerBalances where b.BusinessPartnerId == oInvoice.ClientId select b.BalanceAmount).FirstOrDefault();
                oInvoice.PaySettles = (from ps in _vssDb.PaySettles
                                       join pm in _vssDb.PayMethods on ps.PaymentMethod equals pm.MethodId
                                       where ps.InvoiceId == oInvoice.Id
                                       select new PaySettleVM()
                                       {
                                           Amount = ps.Amount,
                                           Id = ps.Id,
                                           InvoiceId = oInvoice.Id,
                                           PayDate = ps.PayDate,
                                           PaymentMethod = ps.PaymentMethod,
                                           PayMethodName = pm.Name
                                       }).ToList();
                oInvoice.GrandTotalWord = NumberToWord.ConvertAmount((double)oInvoice.GrandTotal);
                oInvoice = oInvoice != null ? oInvoice : new InvoiceVM();
            }
            return oInvoice;
        }

        
    }
}