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
    public class StockController : ApiController
    {
        // GET: api/SalePrice
        // GET: api/Brand
        public IEnumerable<StockVM> Get([FromUri] string partNo, int pi = 0, int ps = 10)
        {
            StockBL _BL = new StockBL();
            return _BL.Get(partNo, pi, ps);
        }

        
    }
}
