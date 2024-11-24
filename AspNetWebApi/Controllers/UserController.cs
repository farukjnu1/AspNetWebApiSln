using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using AspNetWebApi.Attributes;
using AspNetWebApi.BL.HR;
using AspNetWebApi.BL.System;
using AspNetWebApi.DA.EF.VssDb;
using AspNetWebApi.DA.ViewModels.Operation;
using AspNetWebApi.DA.ViewModels.System;
using AspNetWebApi.BL.Operation;

namespace AspNetWebApi.Controllers
{
    /// <summary>
    [MyAuth]
    /// </summary>
    public class UserController : ApiController
    {
        // GET: api/User
        public IEnumerable<UserVM> Get()
        {
            UserBL _BL = new UserBL();
            return _BL.Get();
        }

        // GET: api/User/5
        public string Get(int id)
            {
                return "value";
            }

        // POST: api/User
        public bool Post([FromBody] User model)
        {
            UserBL _BL = new UserBL();
            return _BL.Add(model);
        }

        // PUT: api/User/5
        public bool Put([FromBody] User model)
        {
            UserBL _BL = new UserBL();
            return _BL.Update(model);
        }

        // DELETE: api/User/5
        public bool Delete(int id)
        {
            UserBL _BL = new UserBL();

            return _BL.Remove(id);
        }
    }
}
