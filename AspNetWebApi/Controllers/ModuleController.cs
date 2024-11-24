using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AspNetWebApi.BL.System;
using AspNetWebApi.DA.ViewModels.System;

namespace AspNetWebApi.Controllers
{
    public class ModuleController : ApiController
    {
        // GET: api/Module
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        public IEnumerable<ModuleVM> Get([FromUri] string phone, int pi = 0, int ps = 10)
        {
            ModuleBL _BL = new ModuleBL();
            return _BL.Get(phone, pi, ps);
        }

        // GET: api/Module/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Module
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Module/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Module/5
        public void Delete(int id)
        {
        }
    }
}
