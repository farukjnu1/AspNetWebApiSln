namespace AspNetWebApi.DA.EF.VssDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PaySettle")]
    public partial class PaySettle
    {
        public int Id { get; set; }

        public long? InvoiceId { get; set; }

        public int? PaymentMethod { get; set; }

        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }

        public DateTime? PayDate { get; set; }

        public virtual Invoice Invoice { get; set; }

        public virtual PayMethod PayMethod { get; set; }
    }
}
