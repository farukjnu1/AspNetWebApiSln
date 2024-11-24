namespace AspNetWebApi.DA.EF.VssDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RequisitionType")]
    public partial class RequisitionType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RequisitionTypeId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
    }
}
