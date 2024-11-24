namespace AspNetWebApi.DA.EF.VssDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class VehicleStda110U
    {
        public long Id { get; set; }

        [StringLength(50)]
        public string Manufacturer { get; set; }

        [StringLength(50)]
        public string Model { get; set; }

        [StringLength(200)]
        public string SubModel { get; set; }

        [StringLength(10)]
        public string From { get; set; }

        [StringLength(10)]
        public string To { get; set; }
    }
}
