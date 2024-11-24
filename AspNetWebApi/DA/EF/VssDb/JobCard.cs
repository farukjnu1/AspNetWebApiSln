namespace AspNetWebApi.DA.EF.VssDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("JobCard")]
    public partial class JobCard
    {
        public long Id { get; set; }

        [StringLength(20)]
        public string JcNo { get; set; }

        [StringLength(25)]
        public string VehicleNo { get; set; }

        [StringLength(15)]
        public string Model { get; set; }

        [StringLength(30)]
        public string Vin { get; set; }

        public DateTime? ReceiveDate { get; set; }

        public int? SupervisorId { get; set; }

        public int? IsDone { get; set; }

        public int? Bay { get; set; }

        public decimal? Mileage { get; set; }

        public int? JcStatus { get; set; }

        public decimal? EstiCostJob { get; set; }

        public decimal? EstiCostSpare { get; set; }

        public decimal? EstiCostTotal { get; set; }

        public decimal? ActualCostJob { get; set; }

        public decimal? ActualCostSpare { get; set; }

        public decimal? ActualCostTotal { get; set; }

        public int? ClientId { get; set; }

        [StringLength(200)]
        public string ClientInfo { get; set; }

        [StringLength(50)]
        public string ContactPerson { get; set; }

        [StringLength(20)]
        public string ContactPersonNo { get; set; }

        public bool? IsDelivered { get; set; }

        public bool? IsPaid { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? CreateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int? UpdateBy { get; set; }

        public bool? IsDelete { get; set; }

        public int? DeleteDate { get; set; }

        public int? DeleteBy { get; set; }
    }
}
