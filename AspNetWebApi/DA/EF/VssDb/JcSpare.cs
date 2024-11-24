namespace AspNetWebApi.DA.EF.VssDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("JcSpare")]
    public partial class JcSpare
    {
        public long Id { get; set; }

        public decimal? SalePrice { get; set; }

        public decimal? Quantity { get; set; }

        public decimal? SpareAmount { get; set; }

        public int? ItemStatus { get; set; }

        public long? JcId { get; set; }

        public int? ItemId { get; set; }
    }
}
