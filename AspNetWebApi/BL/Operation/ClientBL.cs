using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetWebApi.DA.ADO;
using AspNetWebApi.DA.EF.VssDb;
using AspNetWebApi.DA.ViewModels.Operation;

namespace AspNetWebApi.BL.Operation
{
    public class ClientBL
    {
        ModelVssDb _vssDb = new ModelVssDb();
        List<ClientVM> listClient = null;
        ClientVM oClient = null;
        private IGenericFactory<ClientVM> Generic_Client = null;
        public ClientBL() { }
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
            return Generic_Client.ExecuteCommandList(CommandType.StoredProcedure, StoredProcedure.sp_GetClient, oHashTable, vssDb);
        }
        public ClientVM Get(int id)
        {
            oClient = new ClientVM();
            var oBP = _vssDb.BusinessPartners
                .Where(x => x.BpTypeId == 1 && x.BpId == id)
                .FirstOrDefault();
            if (oBP != null)
            {
                oClient.BpId = oBP.BpId;
                oClient.BpTypeId = oBP.BpTypeId;
                oClient.Name = oBP.Name;
                oClient.Phone = oBP.Phone;
                oClient.Email = oBP.Email;
                oClient.Address = oBP.Address;
                oClient.ContactPerson = oBP.ContactPerson;
                oClient.ContactPersonNo = oBP.ContactPersonNo;
                oClient.ClientInfo = oBP.ClientInfo;
                oClient.MembershipNo = oBP.MembershipNo;
            }
            return oClient;
        }

        public IEnumerable<ClientVM> GetClient()
        {
            var listClient = from x in _vssDb.BusinessPartners
                             where x.BpTypeId == 1
                             select new ClientVM
                             {
                                 BpId = x.BpId,
                                 Name = x.Name
                             };
            return listClient;
        }

        public IEnumerable<ClientVM> GetClientByInfo(string value = "")
        {
            var listClient = (from x in _vssDb.BusinessPartners
                             where x.BpTypeId == 1 && (x.Phone.Contains(value) || x.Name.Contains(value))
                             orderby x.Name
                             select new ClientVM
                             {
                                 BpId = x.BpId,
                                 Name = x.Phone + " - " + x.Name
                             }).ToList().Take(20);
            return listClient;
        }

        public object Add(BusinessPartner model)
        {
            try
            {
                var phone = model.Phone.Substring(model.Phone.Length - 10);
                var oClient = (from x in _vssDb.BusinessPartners where x.Phone.Substring(x.Phone.Length - 10) == phone.Trim() select x).FirstOrDefault();
                if (oClient == null)
                {
                    model.Phone = model.Phone.Trim();
                    model.BpId = GetNewId();
                    model.BpTypeId = 1;
                    model.CreateDate = DateTime.Now;
                    _vssDb.BusinessPartners.Add(model);
                    _vssDb.SaveChanges();
                    return new { message = "New client added successfully.", status = true };
                }
                else
                {
                    return new { message = "Client mobile already exist.", status = false };
                }
            }
            catch(Exception ex)
            {
                return new { message = ex.Message, status = false };
            }
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
        public object Update(BusinessPartner model)
        {
            try
            {
                var oBP = _vssDb.BusinessPartners.Where(x => x.BpTypeId == 1 && x.BpId == model.BpId).FirstOrDefault();
                if (oBP != null)
                {
                    if (oBP.Phone.Trim() == model.Phone.Trim())
                    {
                        oBP.BpTypeId = model.BpTypeId;
                        oBP.Name = model.Name;
                        oBP.Phone = model.Phone.Trim();
                        oBP.Email = model.Email;
                        oBP.Address = model.Address;
                        oBP.ContactPerson = model.ContactPerson;
                        oBP.ContactPersonNo = model.ContactPersonNo;
                        oBP.ClientInfo = model.ClientInfo;
                        oBP.MembershipNo = model.MembershipNo;
                        oBP.UpdateBy = model.UpdateBy;
                        oBP.UpdateDate = DateTime.Now;
                        _vssDb.SaveChanges();
                        return new { message = "Updated successfully.", status = true };
                    }
                    else
                    {
                        var phone = model.Phone.Substring(model.Phone.Length - 10);
                        var oClient = (from x in _vssDb.BusinessPartners where x.Phone.Substring(x.Phone.Length - 10) == phone.Trim() select x).FirstOrDefault();
                        if (oClient == null)
                        {
                            oBP.BpTypeId = model.BpTypeId;
                            oBP.Name = model.Name;
                            oBP.Phone = model.Phone.Trim();
                            oBP.Email = model.Email;
                            oBP.Address = model.Address;
                            oBP.ContactPerson = model.ContactPerson;
                            oBP.ContactPersonNo = model.ContactPersonNo;
                            oBP.ClientInfo = model.ClientInfo;
                            oBP.MembershipNo = model.MembershipNo;
                            oBP.UpdateBy = model.UpdateBy;
                            oBP.UpdateDate = DateTime.Now;
                            _vssDb.SaveChanges();
                            return new { message = "Updated successfully.", status = true };
                        }
                        else
                        {
                            return new { message = "Client mobile already registered with other client.", status = false };
                        }
                    }
                }
                else
                {
                    return new { message = "Data not exist.", status = false };
                }
            }
            catch(Exception ex)
            {
                return new { message = ex.Message, status = false };
            }
        }
        public bool Remove(int id)
        {
            try
            {
                var oBP = _vssDb.BusinessPartners
                 .Where(x => x.BpTypeId == 1 && x.BpId == id)
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

        /* public IEnumerable<ClientVM> getSName()
         {
             var listName = _vssDb.BusinessPartners
                 .Select(x => new ClientVM
                 {
                     BpId = x.BpId,
                     Name = x.Name
                 }).OrderBy(s => s.Name).ToList();
             return listName;
         }*/

        public IEnumerable<ClientVM> getSName()
        {
            listClient = new List<ClientVM>();
            var listBP = _vssDb.BusinessPartners
                .Where(x => x.BpTypeId == 2)
                .ToList();
            foreach (BusinessPartner oBP in listBP)
            {
                ClientVM oClient = new ClientVM();
                oClient.BpId = oBP.BpId;
                oClient.BpTypeId = oBP.BpTypeId;
                oClient.Name = oBP.Name;
                oClient.Phone = oBP.Phone;
                oClient.Email = oBP.Email;
                oClient.Address = oBP.Address;
                listClient.Add(oClient);
            }
            return listClient;
        }
    }
}
