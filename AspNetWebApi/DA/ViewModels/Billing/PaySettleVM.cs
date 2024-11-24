using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AspNetWebApi.DA.ViewModels.Billing
{
    public class PaySettleVM
    {
        public int Id { get; set; }

        public long? InvoiceId { get; set; }

        public int? PaymentMethod { get; set; }

        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }

        public DateTime? PayDate { get; set; }
        public string PayMethodName { get; set; }
    }
}