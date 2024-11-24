using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNetWebApi.DA.ViewModels.System
{
    public class UserVM
    {
        public int UserID { get; set; }

        [StringLength(50)]
        public string UserCode { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        [StringLength(100)]
        public string UserPass { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string MiddleName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(256)]
        public string Email { get; set; }

        [StringLength(20)]
        public string MobileNo { get; set; }

        [StringLength(40)]
        public string PhoneNo { get; set; }

        public bool? IsActive { get; set; }
        public List<MenuPermissionVM> Permissions { get; set; }
        [StringLength(500)]
        public string Token { get; set; }
        public int nNotify { get; set; }
    }
}