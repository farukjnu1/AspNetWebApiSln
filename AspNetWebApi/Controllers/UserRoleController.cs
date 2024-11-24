using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AspNetWebApi.Attributes;
using AspNetWebApi.BL.System;
using AspNetWebApi.DA.EF.VssDb;
using AspNetWebApi.DA.ViewModels.Operation;
using AspNetWebApi.DA.ViewModels.System;
using AspNetWebApi.BL.Operation;

namespace AspNetWebApi.Controllers
{
    [MyAuth]
    public class UserRoleController : ApiController
    {
        // GET: api/UserRole
        /*public IEnumerable<UserRoleVM> Get()
        {
            UserRoleBL _BL = new UserRoleBL();
            return _BL.Get();
        }*/

        // GET: api/UserRole/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/UserRole
        public bool Post([FromBody] List<UserRoleVM> listModel)
        {
            UserRoleBL _BL = new UserRoleBL();
            return _BL.add(listModel);
        }

        // PUT: api/UserRole/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/UserRole/5
        public void Delete(int id)
        {
        }

        [HttpGet]
        [Route("api/UserRole/GetUserRole")]
        public List<UserRoleVM> GetUserRole(int userId)
        {
            return new UserRoleBL().GetUserRole(userId);
        }
    }
}
