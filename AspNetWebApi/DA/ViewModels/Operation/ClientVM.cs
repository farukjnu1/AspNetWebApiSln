using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetWebApi.DA.ViewModels.Common;

namespace AspNetWebApi.DA.ViewModels.Operation
{
    public class ClientVM : Pager
    {
        public int BpId { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(250)]
        public string Address { get; set; } = "";

        [StringLength(25)]
        public string Phone { get; set; }

        [StringLength(250)]
        public string Email { get; set; } = "";

        public int? BpTypeId { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? CreateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int? UpdateBy { get; set; }

        public bool? IsDelete { get; set; }

        public int? DeleteDate { get; set; }

        public int? DeleteBy { get; set; }
        [StringLength(50)]
        public string BPTypeName { get; set; }
        [StringLength(10)]
        public string MembershipNo { get; set; }

        [StringLength(200)]
        public string ClientInfo { get; set; }

        [StringLength(50)]
        public string ContactPerson { get; set; }

        [StringLength(20)]
        public string ContactPersonNo { get; set; }
    }
}
