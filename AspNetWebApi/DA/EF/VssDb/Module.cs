namespace AspNetWebApi.DA.EF.VssDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Module")]
    public partial class Module
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Module()
        {
            Menus = new HashSet<Menu>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ModuleId { get; set; }

        [Required]
        [StringLength(50)]
        public string ModuleCode { get; set; }

        [Required]
        [StringLength(100)]
        public string ModuleName { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [StringLength(50)]
        public string ModuleIcon { get; set; }

        [StringLength(50)]
        public string ModuleColor { get; set; }

        [Required]
        [StringLength(250)]
        public string ModulePath { get; set; }

        public int? ModuleSequence { get; set; }

        public bool? IsActive { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Menu> Menus { get; set; }
    }
}
