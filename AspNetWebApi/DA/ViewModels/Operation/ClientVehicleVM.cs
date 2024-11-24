using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AspNetWebApi.DA.ViewModels.Common;

namespace AspNetWebApi.DA.ViewModels.Operation
{
    public class ClientVehicleVM:Pager
    {
        public long Id { get; set; }

        public int? ClientId { get; set; }

        [StringLength(25)]
        public string VehicleNo { get; set; }

        [StringLength(30)]
        public string Vin { get; set; }

        [StringLength(50)]
        public string Manufacturer { get; set; }

        [StringLength(15)]
        public string Model { get; set; }

        [StringLength(50)]
        public string SubModel { get; set; }

        [StringLength(10)]
        public string From { get; set; }

        [StringLength(10)]
        public string To { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? CreateBy { get; set; }
        // extra
        public string ClientName { get; set; }
        public string Phone { get; set; }
        public string Value { get; set; }
        public string Body { get; set; }
        public string Engine { get; set; }
        public string Transmission { get; set; }
        public string Remark { get; set; }
    }
}