using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetWebApi.DA.ViewModels.Common
{
    public class Pager
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public int RowCount { get; set; } = 0;
    }
}