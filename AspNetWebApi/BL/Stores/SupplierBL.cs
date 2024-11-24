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

namespace AspNetWebApi.BL.Stores
{

    public class SupplierBL
    {
        ModelVssDb _vssDb = new ModelVssDb();
        //List<ClientVM> listClient = null;
        //ClientVM oClient = null;
        private IGenericFactory<ClientVM> Generic_Client = null;

        public IEnumerable<ClientVM> Get(string phone, int pageIndex, int pageSize)
        {
            string vssDb = ConfigurationManager.ConnectionStrings["VssDb"].ConnectionString;
            Generic_Client = new GenericFactory<ClientVM>();
            var oHashTable = new Hashtable()
            {
                { "PageIndex", pageIndex },
                { "PageSize", pageSize },
                { "phone", phone }
            };
            return Generic_Client.ExecuteCommandList(CommandType.StoredProcedure, StoredProcedure.sp_GetSupplier, oHashTable, vssDb);
        }

        public bool Add(BusinessPartner model)
        {
            try
            {
                model.BpId = GetNewId();
                model.BpTypeId = 2;
                model.CreateDate = DateTime.Now;
                _vssDb.BusinessPartners.Add(model);
                _vssDb.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(BusinessPartner model)
        {
            try
            {
                var oBP = _vssDb.BusinessPartners
                 .Where(x => x.BpTypeId == 1 && x.BpId == model.BpId)
                 .FirstOrDefault();
                if (oBP != null)
                {
                    oBP.BpTypeId = model.BpTypeId;
                    oBP.Name = model.Name;
                    oBP.Phone = model.Phone;
                    oBP.Email = model.Email;
                    oBP.Address = model.Address;
                    oBP.ContactPerson = model.ContactPerson;
                    oBP.ContactPersonNo = model.ContactPersonNo;
                    oBP.ClientInfo = model.ClientInfo;
                    oBP.MembershipNo = model.MembershipNo;
                    oBP.UpdateBy = model.UpdateBy;
                    oBP.UpdateDate = DateTime.Now;
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
                var oBP = _vssDb.BusinessPartners
                 .Where(x => x.BpTypeId == 2 && x.BpId == id)
                 .FirstOrDefault();
                if (oBP != null)
                {
                    _vssDb.BusinessPartners.Remove(oBP);
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
                var nid = Convert.ToInt32(_vssDb.BusinessPartners.Max(x => x.BpId)) + 1;
                return nid;
            }
            catch
            {
                return 1;
            }
        }
    }
}