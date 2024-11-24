using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNetWebApi.DA.ViewModels.Stores
{
    public class StoreTranVM
    {
        public long Id { get; set; }

        public int? WhId { get; set; }

        public decimal? PurchasePrice { get; set; }

        public int BusinessPartnerId { get; set; }

        public int? ItemId { get; set; }

        public decimal? Qty { get; set; }

        [StringLength(50)]
        public string Remark { get; set; }

        public int? StoreRecTypeId { get; set; }

        public int? ReqId { get; set; }

        public int? CreateBy { get; set; }
    }
}