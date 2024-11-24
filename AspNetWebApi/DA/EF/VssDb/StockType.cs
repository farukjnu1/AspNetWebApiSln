namespace AspNetWebApi.DA.EF.VssDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StockType")]
    public partial class StockType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StockTypeId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
    }
}
