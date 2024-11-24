using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetWebApi.DA.ViewModels.Purchase
{
    public class PoItemVm
    {
        public long PoItemId { get; set; }

        public long? PoId { get; set; }

        public decimal? TotalAmount { get; set; }

        public int? SupplierId { get; set; }

        public DateTime? PoDate { get; set; }

        public int? ItemId { get; set; }

        public decimal? Qty { get; set; }

        public decimal? UnitPrice { get; set; }

        public decimal? TotalPrice { get; set; }

        public decimal? Vat { get; set; }

        public decimal? Tax { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? CreateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int? UpdateBy { get; set; }

        public bool? IsDelete { get; set; }

        public int? DeleteDate { get; set; }

        public int? DeleteBy { get; set; }
    }
}