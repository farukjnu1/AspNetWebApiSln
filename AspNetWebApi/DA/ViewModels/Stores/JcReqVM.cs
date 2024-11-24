using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AspNetWebApi.DA.ViewModels.Common;

namespace AspNetWebApi.DA.ViewModels.Stores
{
    public class JcReqVM:Pager
    {
        public long Id { get; set; }

        [StringLength(20)]
        public string JcNo { get; set; }

        [StringLength(50)]
        public string Brand { get; set; }

        [StringLength(50)]
        public string BrandModel { get; set; }

        [StringLength(50)]
        public string PartName { get; set; }

        [StringLength(50)]
        public string PartNo { get; set; }

        public decimal? Qty { get; set; }

        [StringLength(50)]
        public string Remark { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? CreateBy { get; set; }

        public bool? IsRead { get; set; }

        public int? ReadBy { get; set; }
        public string UserName { get; set; }
    }
}