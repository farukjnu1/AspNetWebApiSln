namespace AspNetWebApi.DA.EF.VssDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InventoryReceive")]
    public partial class InventoryReceive
    {
        public long InventoryReceiveId { get; set; }

        public int? ReferenceId { get; set; }

        public int? WareHouseId { get; set; }

        public int? ItemId { get; set; }

        public decimal? Quantity { get; set; }

        public int? InventoryReceiveTypeId { get; set; }

        [StringLength(100)]
        public string Remark { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? CreateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int? UpdateBy { get; set; }

        public bool? IsDelete { get; set; }

        public DateTime? DeleteDate { get; set; }

        public int? DeleteBy { get; set; }
    }
}
