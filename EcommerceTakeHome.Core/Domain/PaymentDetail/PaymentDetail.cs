using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EcommerceTakeHome.Core.Domain.PaymentDetail
{
    public class PaymentDetail
    {
        public Guid PaymentDetailId { get; set; }

        [Required()] 
        public decimal Amount { get; set; }

    }
}
