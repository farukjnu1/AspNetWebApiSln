namespace AspNetWebApi.DA.EF.VssDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SalesReturn")]
    public partial class SalesReturn
    {
        public long Id { get; set; }

        [StringLength(20)]
        public string JcNo { get; set; }

        public int? ClientId { get; set; }

        public long? InvoiceId { get; set; }

        public int? ItemId { get; set; }

        public int? ItemType { get; set; }

        public long? InvoiceItemId { get; set; }

        public decimal? Qty { get; set; }

        public decimal? UnitPrice { get; set; }

        public decimal? Discount { get; set; }

        public decimal? DiscountAmount { get; set; }

        public decimal? TpAfterDiscount { get; set; }

        [StringLength(100)]
        public string Remarks { get; set; }

        public decimal? ReturnCharge { get; set; }

        public decimal? ReturnAmount { get; set; }

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
