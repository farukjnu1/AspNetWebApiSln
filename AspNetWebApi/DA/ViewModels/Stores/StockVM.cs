using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AspNetWebApi.DA.ViewModels.Common;

namespace AspNetWebApi.DA.ViewModels.Stores
{
    public class StockVM:Pager
    {
        public int Id { get; set; }
        public int? WhId { get; set; }
        public int? ItemId { get; set; }
        public decimal? Qty { get; set; }
        public string WhName { get; set; }
        public string ItemName { get; set; }
        public string BrandName { get; set; }
        public string ModelCode { get; set; }
        public string PartNoOld { get; set; }
        public string PartNoNew { get; set; }
    }
}