namespace AspNetWebApi.DA.EF.VssDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BrandModel")]
    public partial class BrandModel
    {
        public int Id { get; set; }

        public int? BrandId { get; set; }

        [StringLength(25)]
        public string ModelCode { get; set; }

        [StringLength(100)]
        public string Remarks { get; set; }
    }
}
