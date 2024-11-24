using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNetWebApi.DA.EF.VssDb;

namespace AspNetWebApi.DA.ViewModels.Operation
{
    public class VehicleStda110UVm
    {
        public long Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string SubModel { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }
}