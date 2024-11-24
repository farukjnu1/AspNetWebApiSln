using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AspNetWebApi.Attributes;
using AspNetWebApi.DA.EF.VssDb;
using AspNetWebApi.DA.ViewModels.Operation;
using AspNetWebApi.BL.Operation;

namespace AspNetWebApi.Controllers
{
    [MyAuth]
    public class JobGroupController : ApiController
    {
        // GET: api/JobGroup
        public IEnumerable<JobGroupVM> Get(int pi = 0, int ps = 5)
        {
            JobGroupBL _BL = new JobGroupBL();
            return _BL.Get(pi, ps);
        }

        // GET: api/JobGroup/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/JobGroup
        public bool Post([FromBody] JobGroup model)
        {
            JobGroupBL _BL = new JobGroupBL();
            return _BL.Add(model);
        }

        // PUT: api/JobGroup/5
        public bool Put([FromBody] JobGroup model)
        {
            JobGroupBL _BL = new JobGroupBL();
            return _BL.Update(model);
        }

        // DELETE: api/JobGroup/5
        public bool Delete(int id)
        {
            JobGroupBL _BL = new JobGroupBL();
            return _BL.Remove(id);
        }
    }
}
