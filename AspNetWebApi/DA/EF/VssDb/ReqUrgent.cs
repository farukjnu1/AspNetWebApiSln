namespace AspNetWebApi.DA.EF.VssDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ReqUrgent")]
    public partial class ReqUrgent
    {
        [Key]
        public int UrgentId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
    }
}
