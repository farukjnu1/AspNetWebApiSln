using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AspNetWebApi.BL.Stores;
using AspNetWebApi.DA.EF.VssDb;
using AspNetWebApi.BL.Operation;
using AspNetWebApi.DA.ViewModels.Operation;

namespace AspNetWebApi.Controllers
{
    public class SupplierController : ApiController
    {
        // GET: api/Supplier
        public IEnumerable<ClientVM> Get([FromUri] string phone, int pi = 0, int ps = 10)
        {
            SupplierBL _BL = new SupplierBL();
            return _BL.Get(phone, pi, ps);
        }

        // GET: api/Supplier/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Supplier
        public bool Post([FromBody] BusinessPartner model)
        {
            SupplierBL _BL = new SupplierBL();
            return _BL.Add(model);
        }

        // PUT: api/Supplier/5
        public bool Put([FromBody] BusinessPartner model)
        {
            SupplierBL _BL = new SupplierBL();
            return _BL.Update(model);
        }

        // DELETE: api/Supplier/5
        public bool Delete(int id)
        {
            SupplierBL _BL = new SupplierBL();
            return _BL.Remove(id);
        }
    }
}
