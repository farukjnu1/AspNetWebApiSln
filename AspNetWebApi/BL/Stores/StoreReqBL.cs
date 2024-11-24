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
using AspNetWebApi.DA.Utilities;
using AspNetWebApi.DA.ViewModels.Operation;
using AspNetWebApi.DA.ViewModels.Stores;

namespace AspNetWebApi.BL.Stores
{
    public class StoreReqBL
    {
        ModelVssDb _vssDb = new ModelVssDb();
        private IGenericFactory<StoreReqVM> Generic_StoreReq = null;

        public List<StoreReqVM> Get(int reqStatus, int storeTranTypeId, int pageIndex = 0, int pageSize = 10)
        {
            string vssDb = ConfigurationManager.ConnectionStrings["VssDb"].ConnectionString;
            Generic_StoreReq = new GenericFactory<StoreReqVM>();
            return Generic_StoreReq.ExecuteCommandList(CommandType.StoredProcedure, StoredProcedure.sp_GetStoreReq, new Hashtable() { { "PageIndex", pageIndex }, { "PageSize", pageSize }, { "ReqStatus", reqStatus }, { "StoreTranTypeId", storeTranTypeId } }, vssDb);
        }

        public bool Add(StoreReq model)
        {
            try
            {
                model.CreateDate = DateTime.Now;
                _vssDb.StoreReqs.Add(model);
                _vssDb.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(StoreReq model)
        {
            try
            {
                var oStoreReq = _vssDb.StoreReqs
                 .Where(x => x.Id == model.Id).FirstOrDefault();
                if (oStoreReq != null)
                {
                    if (oStoreReq.ReqStatus == (int)BLStatus.ReqStatus.Initial)
                    {
                        oStoreReq.WhId = model.WhId;
                        oStoreReq.ItemId = model.ItemId;
                        oStoreReq.SupplierId = model.SupplierId;
                        oStoreReq.Remark = model.Remark;
                        oStoreReq.Qty = model.Qty;
                        oStoreReq.ReqStatus = model.ReqStatus;
                        oStoreReq.UpdateBy = model.CreateBy;
                        oStoreReq.UpdateDate = DateTime.Now;
                        oStoreReq.DeliveryTime = model.DeliveryTime;
                        oStoreReq.ReqUrgentType = model.ReqUrgentType;
                        _vssDb.SaveChanges();
                        return true;
                    }
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
                var oStoreReq = _vssDb.StoreReqs
                 .Where(x => x.Id == id)
                 .FirstOrDefault();
                if (oStoreReq != null)
                {
                    if (oStoreReq.ReqStatus == (int)BLStatus.ReqStatus.Initial)
                    {
                        _vssDb.StoreReqs.Remove(oStoreReq);
                        _vssDb.SaveChanges();
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
            return false;
        }
        
    }
}