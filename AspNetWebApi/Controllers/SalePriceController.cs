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
    public class SalePriceController : ApiController
    {
        // GET: api/SalePrice
        // GET: api/Brand
        public IEnumerable<SalesPriceVM> Get([FromUri] string partNo, int pi = 0, int ps = 10)
        {
            SalesPriceBL _BL = new SalesPriceBL();
            return _BL.Get(partNo, pi, ps);
        }

        // POST: api/Brand
        public bool Post([FromBody] SalesPriceVM model)
        {
            SalesPriceBL _BL = new SalesPriceBL();
            return _BL.Add(model);
        }
    }
}
