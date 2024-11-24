using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AspNetWebApi.DA.ViewModels.Common;

namespace AspNetWebApi.DA.ViewModels.System
{
    public class ModuleVM:Pager
    {
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
    }
}