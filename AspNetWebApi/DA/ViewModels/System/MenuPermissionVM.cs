using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNetWebApi.DA.EF.VssDb;

namespace AspNetWebApi.DA.ViewModels.System
{
    public class MenuPermissionVM
    {
        public int? MenuId { get; set; }

        public int? ModuleId { get; set; }

        public string MenuCode { get; set; }

        public string MenuName { get; set; }

        public int? ParentId { get; set; }

        public bool? IsSubParent { get; set; }

        public int? SubPareMenuNamentId { get; set; }

        public string MenuIcon { get; set; }

        public string MenuPath { get; set; }

        public int MenuSequence { get; set; }

        public bool? IsActive { get; set; }

        public int? RoleId { get; set; }

        public string RoleName { get; set; }

        public string ModuleName { get; set; }
        public string ModuleIcon { get; set; }

        public bool? CanView { get; set; }

        public bool? CanCreate { get; set; }
        
        public bool? CanEdit { get; set; }

        public bool? CanDelete { get; set; }
    }
}