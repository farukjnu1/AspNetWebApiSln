using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AspNetWebApi.BL.Billing;
using AspNetWebApi.BL.Purchase;
using AspNetWebApi.DA.ViewModels.Billing;
using AspNetWebApi.DA.ViewModels.Operation;
using AspNetWebApi.DA.ViewModels.Purchase;

namespace AspNetWebApi.Controllers
{
    public class PoController : ApiController
    {
        // GET: api/Invoice
        public IEnumerable<PoVm> Get(int pi = 0, int ps = 5, DateTime? startDate = null, DateTime? endDate = null)
        {
            PoBL _BL = new PoBL();
            return _BL.Get(pi, ps, startDate, endDate);
        }

        // GET: api/Invoice/5
        public PoVm Get(long id)
        {
            PoBL _BL = new PoBL();
            return _BL.Get(id);
        }

        // POST: api/Invoice
        public bool Post([FromBody] PoVm model)
        {
            PoBL _BL = new PoBL();
            return _BL.Add(model);
        }

        // PUT: api/Invoice/5
        public bool Put([FromBody] PoVm model)
        {
            PoBL _BL = new PoBL();
            return _BL.Update(model);
        }

        // DELETE: api/Invoice/5
        public bool Delete(long id)
        {
            PoBL _BL = new PoBL();
            return _BL.Remove(id);
        }

    }
}
