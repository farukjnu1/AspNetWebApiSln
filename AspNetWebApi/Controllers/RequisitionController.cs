using AspNetWebApi.BL.Inventory;
using AspNetWebApi.BL.Requisition;
using AspNetWebApi.DA.ViewModels.Requisition;
using AspNetWebApi.DA.ViewModels.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AspNetWebApi.Controllers
{
    public class RequisitionController : ApiController
    {
        [HttpGet]
        [Route("api/Requisition/Read")]
        public List<RequisitionVM> ReadRequisition([FromUri] int requisitionTypeId, int pageIndex = 0, int pageSize = 10)
        {
            RequisitionBL _BL = new RequisitionBL();
            return _BL.Get(requisitionTypeId, pageIndex, pageSize);
        }

        [HttpGet]
        [Route("api/Requisition/ReadByType")]
        public List<RequisitionVM> ReadByType([FromUri] int requisitionTypeId, int pageIndex = 0, int pageSize = 10)
        {
            RequisitionBL _BL = new RequisitionBL();
            return _BL.ReadByType(requisitionTypeId, pageIndex, pageSize);
        }

        [HttpPost]
        [Route("api/Requisition/Create")]
        public bool CreateRequisition([FromBody] RequisitionVM model)
        {
            RequisitionBL _BL = new RequisitionBL();
            return _BL.Create(model);
        }

        [HttpPut]
        [Route("api/Requisition/Update")]
        public bool UpdateRequisition([FromBody] RequisitionVM model)
        {
            RequisitionBL _BL = new RequisitionBL();
            return _BL.Update(model);
        }

        [HttpDelete]
        [Route("api/Requisition/Delete")]
        public bool DeleteRequisition([FromUri] long requisitionId, int deleteBy)
        {
            RequisitionBL _BL = new RequisitionBL();
            RequisitionVM model = new RequisitionVM() { RequisitionId = requisitionId, DeleteBy = deleteBy };
            return _BL.Delete(model);
        }

    }
}
