using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AspNetWebApi.Attributes;
using AspNetWebApi.BL.Stores;
using AspNetWebApi.DA.EF.VssDb;
using AspNetWebApi.DA.ViewModels.Stores;
using AspNetWebApi.BL.Operation;
using AspNetWebApi.DA.ViewModels.Operation;

namespace AspNetWebApi.Controllers
{
    [MyAuth]
    public class BrandController : ApiController
    {
        // GET: api/Brand
        public IEnumerable<BrandVM> Get([FromUri] int pi = 0, int ps = 10)
        {
            BrandBL _BL = new BrandBL();
            return _BL.Get(pi, ps);
        }

        // GET: api/Brand/5
        //Test for push to github
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Brand
        public bool Post([FromBody] Brand model)
        {
            BrandBL _BL = new BrandBL();
            return _BL.Add(model);
        }

        // PUT: api/Brand/5
        public bool Put([FromBody] Brand model)
        {
            BrandBL _BL = new BrandBL();
            return _BL.Update(model);
        }

        // DELETE: api/Brand/5
        public bool Delete(int id)
        {
            BrandBL _BL = new BrandBL();
            return _BL.Remove(id);
        }
    }
}
