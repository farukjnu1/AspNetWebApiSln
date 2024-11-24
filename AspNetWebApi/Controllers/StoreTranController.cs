using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AspNetWebApi.BL.Stores;
using AspNetWebApi.DA.EF.VssDb;
using AspNetWebApi.DA.ViewModels.Stores;

namespace AspNetWebApi.Controllers
{
    public class StoreTranController : ApiController
    {
        [HttpGet]
        [Route("api/StoreRec/GetRecByReqId")]
        public StoreTranVM GetByReqid([FromUri] int reqId)
        {
            StoreTranBL _BL = new StoreTranBL();
            return _BL.GetRecByReqId(reqId);
        }

        //Approve Requisition & Store Receive
        // POST: api/StoreRec
        public bool Post([FromBody] StoreTran model)
        {
            StoreTranBL _BL = new StoreTranBL();
            return _BL.ApproveReqStoreTran(model);
        }

        // PUT: api/StoreRec/5
        public bool Put([FromBody] StoreTran model)
        {
            StoreTranBL _BL = new StoreTranBL();
            return _BL.Update(model);
        }

        // DELETE: api/StoreRec/5
        public bool Delete(int id)
        {
            StoreTranBL _BL = new StoreTranBL();
            return _BL.Remove(id);
        }
    }
}
