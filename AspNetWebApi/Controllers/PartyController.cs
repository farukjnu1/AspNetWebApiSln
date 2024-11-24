using System;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using System.Web.Http.Cors;
using AspNetWebApi.Attributes;
using AspNetWebApi.BL.Stores;
using AspNetWebApi.DA.EF.VssDb;
using AspNetWebApi.DA.ViewModels.Operation;
using AspNetWebApi.BL.Operation;

namespace AspNetWebApi.Controllers
{
    //[EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    //[VssCorsAttribute]
    [MyAuth]
    public class PartyController : ApiController
    {

        // GET: api/Client
        public IEnumerable<BusinessPartnerVM> Get([FromUri] int businessPartnerTypeId, int pageIndex = 0, int pageSize = 10, string phone = "")
        {
            BusinessPartnerBL _BL = new BusinessPartnerBL();
            return _BL.Get(businessPartnerTypeId, pageIndex, pageSize, phone);
        }

        // GET: api/Client/5
        public BusinessPartnerVM Get(int id)
        {
            BusinessPartnerBL _BL = new BusinessPartnerBL();
            return _BL.Get(id);
        }

        // POST: api/Client
        public object Post([FromBody] BusinessPartner model)
        {
            BusinessPartnerBL _BL = new BusinessPartnerBL();
            return _BL.Add(model);
        }

        // PUT: api/Client/5
        public object Put([FromBody] BusinessPartner model)
        {
            BusinessPartnerBL _BL = new BusinessPartnerBL();
            return _BL.Update(model);
        }

        // DELETE: api/Client/5
        public bool Delete(int id)
        {
            BusinessPartnerBL _BL = new BusinessPartnerBL();
            return _BL.Remove(id);
        }


        [HttpGet]
        [Route("api/Client/getSuplierName")]
        public IEnumerable<BusinessPartnerVM> getSuplierName()
        {
            BusinessPartnerBL _BL = new BusinessPartnerBL();
            return _BL.getSName();
        }

        [HttpGet]
        [Route("api/Client/GetClient")]
        public IEnumerable<BusinessPartnerVM> GetClient()
        {
            BusinessPartnerBL _BL = new BusinessPartnerBL();
            return _BL.GetClient();
        }

        [HttpGet]
        [Route("api/Client/GetClientByInfo")]
        public IEnumerable<BusinessPartnerVM> GetClientByInfo([FromUri] string value = "")
        {
            BusinessPartnerBL _BL = new BusinessPartnerBL();
            return _BL.GetClientByInfo(value);
        }

    }
}
