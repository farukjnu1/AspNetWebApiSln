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

namespace AspNetWebApi.Controllers
{
    [MyAuth]
    public class StoreReqController : ApiController
    {
        // GET: api/StoreReq

        [HttpGet]
        public IEnumerable<StoreReqVM> Get(int reqStatus, int storeTranTypeId, int pi = 0, int ps = 10)
        {
            StoreReqBL _BL = new StoreReqBL();
            return _BL.Get(reqStatus, storeTranTypeId, pi, ps);
        }

        // POST: api/StoreReq
        public bool Post([FromBody] StoreReq model)
        {
            StoreReqBL _BL = new StoreReqBL();
            return _BL.Add(model);
        }

        // PUT: api/StoreReq/5
        public bool Put([FromBody] StoreReq model)
        {
            StoreReqBL _BL = new StoreReqBL();
            return _BL.Update(model);
        }

        // DELETE: api/StoreReq/5
        public bool Delete(int id)
        {
            StoreReqBL _BL = new StoreReqBL();
            return _BL.Remove(id);
        }

    }
}
