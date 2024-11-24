namespace AspNetWebApi.DA.EF.VssDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BusinessPartnerBalance")]
    public partial class BusinessPartnerBalance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BusinessPartnerId { get; set; }

        [Column(TypeName = "money")]
        public decimal? BalanceAmount { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
