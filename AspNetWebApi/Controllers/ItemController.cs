using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AspNetWebApi.Attributes;
using AspNetWebApi.BL.Stores;
using AspNetWebApi.DA.EF.VssDb;
using AspNetWebApi.DA.ViewModels.Operation;
using AspNetWebApi.DA.ViewModels.Stores;
using AspNetWebApi.BL.Operation;

namespace AspNetWebApi.Controllers
{
    [MyAuth]
    public class ItemController : ApiController
    {
        // GET: api/Item
        public IEnumerable<ItemVM> Get(int pi = 0, int ps = 10)
        {
            ItemBL _BL = new ItemBL();
            return _BL.Get(pi,ps);
        }

        // GET: api/Item/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Item
        public bool Post([FromBody] Item model)
        {
            ItemBL _BL = new ItemBL();
            return _BL.Add(model);
        }

        // PUT: api/Item/5
        public bool Put([FromBody] Item model)
        {
            ItemBL _BL = new ItemBL();
            return _BL.Update(model);
        }

        // DELETE: api/Item/5
        public bool Delete(int id)
        {
            ItemBL _BL = new ItemBL();
            return _BL.Remove(id);
        }

        [HttpGet]
        [Route("api/Item/GetBrand")]
        public IEnumerable<BrandVM> GetBrand()
        {
            BrandBL _BL = new BrandBL();
            return _BL.GetBrand();
        }

        [HttpGet]
        [Route("api/Item/GetItemCategory")]
        public IEnumerable<ItemCategoryVM> GetItemCategory()
        {
            BrandBL _BL = new BrandBL();
            return _BL.GetItemCategory();
        }

        [HttpGet]
        [Route("api/Item/getItemName")]
        public IEnumerable<ItemVM> getWareHouse()
        {
            ItemBL _BL = new ItemBL();
            return _BL.getItemName();
        }

        [HttpGet]
        [Route("api/Item/GetBrandModel")]
        public IEnumerable<BrandModelVM> GetBrandModel([FromUri]int id)
        {
            BrandBL _BL = new BrandBL();
            return _BL.GetBrandModel(id);
        }

    }
}
