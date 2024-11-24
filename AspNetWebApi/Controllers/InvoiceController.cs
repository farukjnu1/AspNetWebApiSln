using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AspNetWebApi.Attributes;
using AspNetWebApi.Billing;
using AspNetWebApi.BL.Billing;
using AspNetWebApi.BL.Operation;
using AspNetWebApi.DA.EF.VssDb;
using AspNetWebApi.DA.ViewModels.Billing;
using AspNetWebApi.DA.ViewModels.Operation;

namespace AspNetWebApi.Controllers
{
    [MyAuth]
    public class InvoiceController : ApiController
    {
        // GET: api/Invoice
        public IEnumerable<JobCardVM> Get(int pi = 0, int ps = 5, int jcStatus = 1, bool isPaid = false, string jcNo = "", DateTime? startDate = null, DateTime? endDate = null)
        {
            InvoiceBL _BL = new InvoiceBL();
            return _BL.Get(pi, ps, jcStatus, isPaid, jcNo, startDate, endDate);
        }

        /*public IEnumerable<JobCardVM> Get(int pi = 0, int ps = 5, int jcStatus = 0, string jcNo = "", DateTime? startDate = null, DateTime? endDate = null)
        {
            _BL = new JobCardBL();
            return _BL.Get(pi, ps, jcStatus, jcNo, startDate, endDate);
        }*/

        // GET: api/Invoice/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Invoice
        public bool Post([FromBody] InvoiceVM model)
        {
            InvoiceBL _BL = new InvoiceBL();
            return _BL.Add(model);
        }

        // PUT: api/Invoice/5
        public bool Put([FromBody] InvoiceVM model)
        {
            InvoiceBL _BL = new InvoiceBL();
            return _BL.Update(model);
        }

        // DELETE: api/Invoice/5
        public void Delete(int id)
        {
        }

        [HttpGet]
        [Route("api/Invoice/GetByJc")]
        public InvoiceVM GetByJc([FromUri] int jcId)
        {
            InvoiceBL _BL = new InvoiceBL();
            return _BL.GetByJc(jcId);
        }

    }
}
