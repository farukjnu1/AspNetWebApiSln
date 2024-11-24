using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AspNetWebApi.DA.ViewModels.Common;

namespace AspNetWebApi.DA.ViewModels.Requisition
{
    public class RequisitionVM : Pager
    {
        public long RequisitionId { get; set; }

        public int? ItemId { get; set; }

        public decimal? Quantity { get; set; }

        [StringLength(100)]
        public string Remark { get; set; }

        public int? RequisitionTypeId { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? CreateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int? UpdateBy { get; set; }

        public bool? IsDelete { get; set; }

        public DateTime? DeleteDate { get; set; }

        public int? DeleteBy { get; set; }
        // extra
        public string ItemName { get; set; }
        public string PartNoOld { get; set; }
        public string PartNoNew { get; set; }
        public string Creator { get; set; }
    }
}