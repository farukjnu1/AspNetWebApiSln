using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AspNetWebApi.DA.ViewModels.Common;

namespace AspNetWebApi.DA.ViewModels.System
{
    public class MenuVM : Pager
    {
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
    }
}