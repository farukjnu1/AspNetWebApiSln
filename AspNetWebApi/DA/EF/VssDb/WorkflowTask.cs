namespace AspNetWebApi.DA.EF.VssDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WorkflowTask")]
    public partial class WorkflowTask
    {
        public long WorkflowTaskId { get; set; }

        public int WorkflowSetupId { get; set; }

        public int? WorkflowId { get; set; }

        public int? WorkflowRoleId { get; set; }

        public int? Sequence { get; set; }

        public int? UserId { get; set; }

        public int? ReferenceId { get; set; }

        public bool? IsApprove { get; set; }

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
