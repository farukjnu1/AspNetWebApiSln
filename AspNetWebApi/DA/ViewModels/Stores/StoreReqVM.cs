using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AspNetWebApi.DA.ViewModels.Common;

namespace AspNetWebApi.DA.ViewModels.Stores
{
    public class StoreReqVM:Pager
    {
        public long Id { get; set; }

        public int? WhId { get; set; }

        public int? ItemId { get; set; }

        public int? SupplierId { get; set; }

        public decimal? Qty { get; set; }

        [StringLength(50)]
        public string Remark { get; set; }
        public int? ReqStatus { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? CreateBy { get; set; }
        public string ItemName { get; set; }
        public string SupplierName { get; set; }
        public string WHName { get; set; }
        public string ReqStatusName { get; set; }
        public decimal? RecQty { get; set; }
        public decimal? PurchasePrice { get; set; }
        public int? StoreTranTypeId { get; set; }
        public int? ReqUrgentType { get; set; }

        public int? DeliveryTime { get; set; }
    }
}