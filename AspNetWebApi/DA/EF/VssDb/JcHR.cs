namespace AspNetWebApi.DA.EF.VssDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("JcHR")]
    public partial class JcHR
    {
        public int Id { get; set; }

        public int? EmployeeId { get; set; }

        public long? JcId { get; set; }
    }
}
