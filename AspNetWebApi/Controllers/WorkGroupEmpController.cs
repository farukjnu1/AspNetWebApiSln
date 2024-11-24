using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AspNetWebApi.BL.Operation;
using AspNetWebApi.DA.EF.VssDb;
using AspNetWebApi.DA.ViewModels.Operation;

namespace AspNetWebApi.Controllers
{
    public class WorkGroupEmpController : ApiController
    {
        // GET: api/WorkGroupEmp
        public IEnumerable<WorkGroupEmpVM> Get([FromUri] string phone, int pi = 0, int ps = 10)
        {
            WorkGroupEmpBL _BL = new WorkGroupEmpBL();
            return _BL.Get(phone, pi, ps);
        }

        // GET: api/WorkGroupEmp/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/WorkGroupEmp
        public bool Post([FromBody] WorkGroupEmp model)
        {
            WorkGroupEmpBL _BL = new WorkGroupEmpBL();
            return _BL.Add(model);
        }


        // PUT: api/WorkGroupEmp/5
        public bool Put([FromBody] WorkGroupEmp model)
        {
            WorkGroupEmpBL _BL = new WorkGroupEmpBL();
            return _BL.Update(model);
        }

        // DELETE: api/WorkGroupEmp/5
        public bool Delete(int id)
        {
            WorkGroupEmpBL _BL = new WorkGroupEmpBL();
            return _BL.Remove(id);
        }
    }
}
