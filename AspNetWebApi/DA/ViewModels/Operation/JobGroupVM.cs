using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNetWebApi.DA.ViewModels.Common;

namespace AspNetWebApi.DA.ViewModels.Operation
{
    public class JobGroupVM:Pager
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
    }
}