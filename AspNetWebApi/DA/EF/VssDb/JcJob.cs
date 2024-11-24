namespace AspNetWebApi.DA.EF.VssDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("JcJob")]
    public partial class JcJob
    {
        public long Id { get; set; }

        public int? JobGroupId { get; set; }

        public int? JobId { get; set; }

        public int? EngineSizeId { get; set; }

        public decimal? Price { get; set; }

        public int? Duration { get; set; }

        public int? JobStatus { get; set; }

        public long? JcId { get; set; }

        public int? CreateBy { get; set; }

        public DateTime? CreateDate { get; set; }
    }
}
