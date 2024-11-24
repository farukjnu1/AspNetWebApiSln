namespace AspNetWebApi.DA.EF.VssDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CompanyLogo")]
    public partial class CompanyLogo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LogoId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(250)]
        public string LogoUrl { get; set; }

        public int? CompanyId { get; set; }

        public virtual Company Company { get; set; }
    }
}
