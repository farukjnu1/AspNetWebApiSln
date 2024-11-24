using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNetWebApi.DA.ViewModels.System
{
    public class CompanyVM
    {
        public int CompanyId { get; set; }

        [StringLength(10)]
        public string CompanyCode { get; set; }

        [StringLength(50)]
        public string CompanyName { get; set; }

        [StringLength(150)]
        public string Description { get; set; }

        [StringLength(25)]
        public string DateFormat { get; set; }

        public int? DecimalPlace { get; set; }

        public int? Bay { get; set; }

        public decimal? Vat { get; set; }

        [StringLength(150)]
        public string Address { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(100)]
        public string Website { get; set; }

        public bool? IsActive { get; set; }
        public List<CompanyLogoVM> Logos { get; set; } = new List<CompanyLogoVM>();
    }

    public class CompanyLogoVM
    {
        public int LogoId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(250)]
        public string LogoUrl { get; set; }

        public int? CompanyId { get; set; }
    }
}