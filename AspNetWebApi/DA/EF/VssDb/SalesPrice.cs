namespace AspNetWebApi.DA.EF.VssDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SalesPrice")]
    public partial class SalesPrice
    {
        public int Id { get; set; }

        public decimal? SalePrice { get; set; }

        public int? ItemId { get; set; }

        public decimal? MinPurchasePrice { get; set; }

        public decimal? AvgPurchasePrice { get; set; }

        public decimal? MaxPurchasePrice { get; set; }

        [StringLength(50)]
        public string Remarks { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? CreateBy { get; set; }

        public bool? IsActive { get; set; }
    }
}
