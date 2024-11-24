using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNetWebApi.DA.ViewModels.Operation
{
    public class EngineSizeVM
    {
        public int EngineSizeId { get; set; }

        [StringLength(2)]
        public string Code { get; set; }

        [StringLength(150)]
        public string Description { get; set; }

        [StringLength(20)]
        public string CC { get; set; }
    }
}