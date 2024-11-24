using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AspNetWebApi.DA.ViewModels.Operation;

namespace AspNetWebApi.DA.ViewModels.Stores
{
    public class SalesPriceVM:ItemVM
    {

        //public int Id { get; set; }

        //public decimal? SalePrice { get; set; }

        public int? ItemId { get; set; }

        //public decimal? MinPurchasePrice { get; set; }

        //public decimal? AvgPurchasePrice { get; set; }

        //public decimal? MaxPurchasePrice { get; set; }

        //[StringLength(50)]
        //public string Remarks { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? CreateBy { get; set; }
    }
}