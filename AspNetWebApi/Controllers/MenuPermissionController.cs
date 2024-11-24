using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AspNetWebApi.Attributes;
using AspNetWebApi.BL.System;
using AspNetWebApi.DA.ViewModels.System;

namespace AspNetWebApi.Controllers
{
    [MyAuth]
    public class MenuPermissionController : ApiController
    {
        // GET: api/MenuPermission
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/MenuPermission?RoleId=5
        [HttpGet]
        [Route("api/MenuPermission/GetMenuPermission")]
        public List<MenuPermissionVM> GetMenuPermission(int RoleId)
        {
            return new MenuPermissionBL().GetMenuPermission(RoleId);
        }

        // GET: api/GetMenuByUser?UserId=5
        /*[HttpGet]
        [Route("api/MenuPermission/GetMenuByUser")]
        public List<MenuPermissionVM> GetMenuByUser(int UserId)
        {
            return new MenuPermissionBL().GetMenuByUser(UserId);
        }*/

        // POST: api/MenuPermission
        public bool Post([FromBody] List<MenuPermissionVM> Model)
        {
            MenuPermissionBL _BL = new MenuPermissionBL();
            return _BL.add(Model);
        }

        // PUT: api/MenuPermission/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/MenuPermission/5
        public void Delete(int id)
        {
        }
    }
}
