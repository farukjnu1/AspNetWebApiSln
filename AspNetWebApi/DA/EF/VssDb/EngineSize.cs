namespace AspNetWebApi.DA.EF.VssDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EngineSize")]
    public partial class EngineSize
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EngineSizeId { get; set; }

        [StringLength(2)]
        public string Code { get; set; }

        [StringLength(150)]
        public string Description { get; set; }

        [StringLength(20)]
        public string CC { get; set; }
    }
}
