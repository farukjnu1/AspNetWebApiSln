using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNetWebApi.DA.ViewModels.Billing;

namespace AspNetWebApi.DA.ViewModels.Purchase
{
    public class PoVm
    {
        public long PoId { get; set; }

        public decimal? TotalAmount { get; set; }

        public int? SupplierId { get; set; }

        public DateTime? PoDate { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? CreateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int? UpdateBy { get; set; }

        public bool? IsDelete { get; set; }

        public int? DeleteDate { get; set; }

        public int? DeleteBy { get; set; }
        public List<PoItemVm> PoItems { get; set; } = new List<PoItemVm>();
    }
}