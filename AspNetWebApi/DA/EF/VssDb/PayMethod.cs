namespace AspNetWebApi.DA.EF.VssDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PayMethod")]
    public partial class PayMethod
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PayMethod()
        {
            PaySettles = new HashSet<PaySettle>();
            PayTrans = new HashSet<PayTran>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MethodId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public virtual PayMethod PayMethod1 { get; set; }

        public virtual PayMethod PayMethod2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PaySettle> PaySettles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PayTran> PayTrans { get; set; }
    }
}
