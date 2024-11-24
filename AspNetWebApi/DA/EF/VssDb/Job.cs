namespace AspNetWebApi.DA.EF.VssDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Job")]
    public partial class Job
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int JobId { get; set; }

        [StringLength(150)]
        public string Description { get; set; }

        public int? JobGroupId { get; set; }

        public int? A { get; set; }

        public int? B { get; set; }

        public int? C { get; set; }

        public int? DurationA { get; set; }

        public int? DurationB { get; set; }

        public int? DurationC { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? CreateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int? UpdateBy { get; set; }
    }
}
