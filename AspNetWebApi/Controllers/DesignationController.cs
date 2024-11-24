using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AspNetWebApi.Attributes;
using AspNetWebApi.BL.HR;
using AspNetWebApi.BL.Operation;
using AspNetWebApi.BL.Stores;
using AspNetWebApi.DA.EF.VssDb;
using AspNetWebApi.DA.ViewModels.HR;
using AspNetWebApi.DA.ViewModels.Operation;
using AspNetWebApi.DA.ViewModels.Stores;

namespace AspNetWebApi.Controllers
{
    [MyAuth]
    public class DesignationController : ApiController
    {
        // GET: api/Designation
        public IEnumerable<DesignationVM> Get([FromUri] string phone, int pi = 0, int ps = 10)
        {
            DesignationBL _BL = new DesignationBL();
            return _BL.Get(phone, pi, ps);
        }
        // GET: api/Designation/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Designation
        public bool Post([FromBody] Designation model)
        {
            DesignationBL _BL = new DesignationBL();
            return _BL.Add(model);
        }

        // PUT: api/Designation/5
        public bool Put([FromBody] Designation model)
        {
            DesignationBL _BL = new DesignationBL();
            return _BL.Update(model);
        }

        // DELETE: api/Designation/5
        public bool Delete(int id)
        {
            DesignationBL _BL = new DesignationBL();

            return _BL.Remove(id);
        }
    }
}
