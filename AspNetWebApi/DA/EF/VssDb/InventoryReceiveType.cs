namespace AspNetWebApi.DA.EF.VssDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InventoryReceiveType")]
    public partial class InventoryReceiveType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int InventoryReceiveTypeId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
    }
}
