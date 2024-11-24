namespace AspNetWebApi.DA.EF.VssDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PO")]
    public partial class PO
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
    }
}
