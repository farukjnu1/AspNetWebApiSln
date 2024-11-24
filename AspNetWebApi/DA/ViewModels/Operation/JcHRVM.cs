using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetWebApi.DA.ViewModels.Operation
{
    public class JcHRVM
    {
        public int? Id { get; set; }

        public int? EmployeeId { get; set; }
        public string FullName { get; set; }
        public long? JcId { get; set; }
    }
}