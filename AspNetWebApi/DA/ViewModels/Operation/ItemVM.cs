using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AspNetWebApi.DA.ViewModels.Common;

namespace AspNetWebApi.DA.ViewModels.Operation
{
    public class ItemVM:Pager
    {
        public long Id { get; set; }

        [StringLength(50)]
        public string ItemCode { get; set; }

        [StringLength(250)]
        public string ItemName { get; set; }

        [StringLength(50)]
        public string Barcode { get; set; }

        public int? UnitId { get; set; }

        public int? ItemGroupId { get; set; }

        public int? ItemCategoryId { get; set; }

        public int? BrandId { get; set; }
        public int? ModelId { get; set; }

        public int? ManufacturerId { get; set; }

        [StringLength(50)]
        public string PartNoOld { get; set; }

        [StringLength(50)]
        public string PartNoNew { get; set; }

        [StringLength(250)]
        public string Remarks { get; set; }

        public bool? IsActive { get; set; }

        public decimal? SalePrice { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }
        public string ModelCode { get; set; }
        public decimal? MinPurchasePrice { get; set; }

        public decimal? AvgPurchasePrice { get; set; }

        public decimal? MaxPurchasePrice { get; set; }
        public decimal? Qty { get; set; }

    }
}