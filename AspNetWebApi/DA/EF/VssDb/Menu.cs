namespace AspNetWebApi.DA.EF.VssDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Menu")]
    public partial class Menu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Menu()
        {
            MenuPermissions = new HashSet<MenuPermission>();
        }

        public int MenuId { get; set; }

        public int? ModuleId { get; set; }

        [StringLength(50)]
        public string MenuCode { get; set; }

        [StringLength(50)]
        public string MenuName { get; set; }

        public int? ParentId { get; set; }

        public bool? IsSubParent { get; set; }

        public int? SubParentId { get; set; }

        [StringLength(50)]
        public string MenuIcon { get; set; }

        [StringLength(250)]
        public string MenuPath { get; set; }

        public int? MenuSequence { get; set; }

        public bool? IsActive { get; set; }

        public virtual Module Module { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MenuPermission> MenuPermissions { get; set; }
    }
}
