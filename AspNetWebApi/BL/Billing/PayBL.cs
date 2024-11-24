using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNetWebApi.DA.EF.VssDb;
using AspNetWebApi.DA.ViewModels.Billing;
using AspNetWebApi.DA.ViewModels.Operation;
using AspNetWebApi.DA.ADO;

namespace AspNetWebApi.BL.Billing
{
    public class PayBL
    {
        ModelVssDb _vssDb = null;
        public bool Pay(PayTranVM model)
        {
            using (_vssDb = new ModelVssDb())
            {
                using (var _tran = _vssDb.Database.BeginTransaction())
                {
                    try
                    {
                        #region Pay Tran
                        PayTran oPayTran = new PayTran();
                        oPayTran.BusinessPartnerId = model.BusinessPartnerId;
                        oPayTran.Amount = model.Amount;
                        oPayTran.PayMethodId = model.PayMethodId;
                        oPayTran.PayStatusId = 1;
                        oPayTran.Remarks = model.Remarks;
                        oPayTran.TrxId = model.TrxId;
                        oPayTran.TranDate = DateTime.Now;
                        oPayTran.CreateBy = model.CreateBy;
                        oPayTran.CreateDate = DateTime.Now;
                        _vssDb.PayTrans.Add(oPayTran);
                        _vssDb.SaveChanges();
                        #endregion
                        #region Balance
                        BusinessPartnerBalance oBPBalance = null;
                        oBPBalance = _vssDb.BusinessPartnerBalances.Where(x=>x.BusinessPartnerId == model.BusinessPartnerId).FirstOrDefault();
                        if (oBPBalance == null)
                        {
                            oBPBalance = new BusinessPartnerBalance();
                            oBPBalance.BusinessPartnerId = (int)model.BusinessPartnerId;
                            oBPBalance.BalanceAmount = model.Amount;
                            oBPBalance.UpdateDate = DateTime.Now;
                            _vssDb.BusinessPartnerBalances.Add(oBPBalance);
                        }
                        else 
                        {
                            oBPBalance.BalanceAmount += model.Amount;
                            oBPBalance.UpdateDate = DateTime.Now;
                        }
                        _vssDb.SaveChanges();
                        #endregion
                        #region Pay Settle
                        var listInvoice = _vssDb.Invoices.Where(x => x.ClientId == model.BusinessPartnerId && x.IsPaid != true).ToList();
                        foreach (var oI in listInvoice)
                        {
                            oBPBalance = _vssDb.BusinessPartnerBalances.Where(x => x.BusinessPartnerId == model.BusinessPartnerId).FirstOrDefault();
                            if (oI.GrandTotal <= oBPBalance.BalanceAmount)
                            {
                                PaySettle oPaySettle = new PaySettle();
                                oPaySettle.PayDate = DateTime.Now;
                                oPaySettle.InvoiceId = oI.Id;
                                oPaySettle.Amount = oI.GrandTotal;
                                oPaySettle.PaymentMethod = model.PayMethodId;
                                _vssDb.PaySettles.Add(oPaySettle);

                                oBPBalance.BalanceAmount -= oPaySettle.Amount;
                                oBPBalance.UpdateDate = DateTime.Now;

                                oI.IsPaid = true;

                                _vssDb.SaveChanges();
                            }
                            else 
                            {
                                break;
                            }
                        }
                        #endregion
                        _tran.Commit();
                        return true;
                    }
                    catch
                    {
                        _tran.Rollback();
                        return false;
                    }
                }
            }
        }
    }
}