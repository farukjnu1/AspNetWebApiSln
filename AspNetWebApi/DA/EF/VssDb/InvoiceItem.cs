namespace AspNetWebApi.DA.EF.VssDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InvoiceItem")]
    public partial class InvoiceItem
    {
        public long Id { get; set; }

        public long? InvoiceId { get; set; }

        public int? ItemId { get; set; }

        public int? ItemType { get; set; }

        public decimal? Qty { get; set; }

        public decimal? UnitPrice { get; set; }

        [Column(TypeName = "money")]
        public decimal? Tax { get; set; }

        public int? DoneBy { get; set; }

        public DateTime? DoneDate { get; set; }

        public decimal? TotalPrice { get; set; }

        public decimal? Discount { get; set; }

        public decimal? DiscountAmount { get; set; }

        public decimal? TpAfterDiscount { get; set; }

        public decimal? Vat { get; set; }

        public decimal? TotalVat { get; set; }

        public decimal? TotalAmount { get; set; }

        public virtual Invoice Invoice { get; set; }
    }
}
