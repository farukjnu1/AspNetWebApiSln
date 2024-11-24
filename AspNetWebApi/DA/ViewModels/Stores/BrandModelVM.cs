using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AspNetWebApi.DA.ViewModels.Common;

namespace AspNetWebApi.DA.ViewModels.Stores
{
    public class BrandModelVM:Pager
    {
        public int Id { get; set; }

        public int? BrandId { get; set; }
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(25)]
        public string ModelCode { get; set; }

        [StringLength(100)]
        public string Remarks { get; set; }
    }
}