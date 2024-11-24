namespace AspNetWebApi.DA.EF.VssDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PayTran")]
    public partial class PayTran
    {
        public long Id { get; set; }

        [StringLength(25)]
        public string TrxId { get; set; }

        public int? BusinessPartnerId { get; set; }

        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }

        public DateTime? TranDate { get; set; }

        [StringLength(50)]
        public string Remarks { get; set; }

        public int? PayMethodId { get; set; }

        public int? PayStatusId { get; set; }

        [StringLength(50)]
        public string BankName { get; set; }

        [StringLength(50)]
        public string BankAccNo { get; set; }

        [StringLength(50)]
        public string BranchName { get; set; }

        [StringLength(100)]
        public string ChequeNo { get; set; }

        public DateTime? ChequeDate { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? CreateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int? UpdateBy { get; set; }

        public DateTime? DeleteDate { get; set; }

        public int? DeleteBy { get; set; }

        public bool? IsDelete { get; set; }

        public virtual BusinessPartner BusinessPartner { get; set; }

        public virtual PayMethod PayMethod { get; set; }

        public virtual PayStatu PayStatu { get; set; }
    }
}
