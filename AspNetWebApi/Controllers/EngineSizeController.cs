using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AspNetWebApi.Attributes;
using AspNetWebApi.DA.EF.VssDb;
using AspNetWebApi.BL.Operation;

namespace AspNetWebApi.Controllers
{
    [MyAuth]
    public class EngineSizeController : ApiController
    {
        // GET: api/EngineSize
        public IEnumerable<EngineSize> Get()
        {
            EngineSizeBL _BL = new EngineSizeBL();
            return _BL.Get();
        }

        // POST: api/EngineSize
        public bool Post([FromBody] EngineSize model)
        {
            EngineSizeBL _BL = new EngineSizeBL();
            return _BL.Add(model);
        }

        // PUT: api/EngineSize/5      
        public bool Put([FromBody] EngineSize model)
        {
            EngineSizeBL _BL = new EngineSizeBL();
            return _BL.updateEngine(model);
        }
        
        // DELETE: api/EngineSize/5
        public bool Delete(int id)
        {
            EngineSizeBL _BL = new EngineSizeBL();
            return _BL.RemoveEngine(id);
        }
    }
}
