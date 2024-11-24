namespace AspNetWebApi.DA.EF.VssDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ItemPrice")]
    public partial class ItemPrice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PriceId { get; set; }

        public int? ItemId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Price { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? CreateBy { get; set; }

        public bool? IsActive { get; set; }

        public virtual Item Item { get; set; }
    }
}
