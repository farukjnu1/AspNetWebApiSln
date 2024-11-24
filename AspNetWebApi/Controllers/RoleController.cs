using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AspNetWebApi.Attributes;
using AspNetWebApi.BL.HR;
using AspNetWebApi.BL.System;
using AspNetWebApi.DA.EF.VssDb;
using AspNetWebApi.DA.ViewModels.System;

namespace AspNetWebApi.Controllers
{
    [MyAuth]
    public class RoleController : ApiController
    {
        // GET: api/Role
        public IEnumerable<RoleVM> Get()
        {
            RoleBL _BL = new RoleBL();
            return _BL.get();
        }

        // GET: api/Role/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Role
        public bool Post([FromBody] Role model)
        {
            RoleBL _BL = new RoleBL();
            return _BL.Add(model);
        }

        // PUT: api/Role/5
        public bool Put([FromBody] Role model)
        {
            RoleBL _BL = new RoleBL();
            return _BL.Update(model);
        }

        // DELETE: api/Role/5
        public bool Delete(int id)
        {
            RoleBL _BL = new RoleBL();

            return _BL.Remove(id);
        }
    }
}
