namespace AspNetWebApi.DA.EF.VssDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StoreTran")]
    public partial class StoreTran
    {
        public long Id { get; set; }

        public int? WhId { get; set; }

        public decimal? PurchasePrice { get; set; }

        public int BusinessPartnerId { get; set; }

        public int? ItemId { get; set; }

        public decimal? Qty { get; set; }

        [StringLength(50)]
        public string Remark { get; set; }

        public int? StoreTranTypeId { get; set; }

        public int? ReqId { get; set; }

        public int? DeliveryTime { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? CreateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int? UpdateBy { get; set; }
    }
}
