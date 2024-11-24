namespace AspNetWebApi.DA.EF.VssDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ClientVehicle")]
    public partial class ClientVehicle
    {
        public long Id { get; set; }

        public int? ClientId { get; set; }

        [StringLength(25)]
        public string VehicleNo { get; set; }

        [StringLength(30)]
        public string Vin { get; set; }

        [StringLength(50)]
        public string Manufacturer { get; set; }

        [StringLength(15)]
        public string Model { get; set; }

        [StringLength(50)]
        public string SubModel { get; set; }

        [StringLength(10)]
        public string From { get; set; }

        [StringLength(10)]
        public string To { get; set; }

        [StringLength(50)]
        public string Body { get; set; }

        [StringLength(50)]
        public string Engine { get; set; }

        [StringLength(50)]
        public string Transmission { get; set; }

        [StringLength(100)]
        public string Remark { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? CreateBy { get; set; }
    }
}
