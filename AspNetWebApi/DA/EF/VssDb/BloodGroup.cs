namespace AspNetWebApi.DA.EF.VssDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BloodGroup")]
    public partial class BloodGroup
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BloodGroupId { get; set; }

        [StringLength(10)]
        public string Name { get; set; }
    }
}
