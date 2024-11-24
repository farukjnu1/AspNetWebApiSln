namespace AspNetWebApi.DA.EF.VssDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StoreReq")]
    public partial class StoreReq
    {
        public long Id { get; set; }

        public int? WhId { get; set; }

        public int? ItemId { get; set; }

        public int? SupplierId { get; set; }

        public decimal? Qty { get; set; }

        [StringLength(50)]
        public string Remark { get; set; }

        public int? ReqStatus { get; set; }

        public int? StoreTranTypeId { get; set; }

        public int? ReqUrgentType { get; set; }

        public int? DeliveryTime { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? CreateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int? UpdateBy { get; set; }
    }
}
