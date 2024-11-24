using AspNetWebApi.BL.Inventory;
using AspNetWebApi.DA.EF.VssDb;
using AspNetWebApi.DA.ViewModels.Inventory;
using AspNetWebApi.DA.ViewModels.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AspNetWebApi.Controllers
{
    public class InventoryController : ApiController
    {
        #region Receive
        [HttpGet]
        [Route("api/Inventory/GetReceive")]
        public List<InventoryReceiveVM> GetReceive([FromUri] int requisitionTypeId, int pageIndex = 0, int pageSize = 10)
        {
            InventoryReceiveBL _BL = new InventoryReceiveBL();
            return _BL.Get(requisitionTypeId, pageIndex, pageSize);
        }

        [HttpPost]
        [Route("api/Inventory/CreateReceive")]
        public bool CreateReceive([FromBody] InventoryReceiveVM model)
        {
            InventoryReceiveBL _BL = new InventoryReceiveBL();
            return _BL.Create(model);
        }

        [HttpPut]
        [Route("api/Inventory/UpdateReceive")]
        public bool UpdateReceive([FromBody] InventoryReceiveVM model)
        {
            InventoryReceiveBL _BL = new InventoryReceiveBL();
            return _BL.Update(model);
        }

        [HttpDelete]
        [Route("api/Inventory/DeleteReceive")]
        public bool DeleteReceive(InventoryReceiveVM model)
        {
            InventoryReceiveBL _BL = new InventoryReceiveBL();
            return _BL.Delete(model);
        }
        #endregion

        #region Issue

        #endregion
    }
}
