﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EcommerceTakeHome.Core.Domain;


namespace EcommerceTakeHome.WebHost.Models
{
    public class OrderRequestPaymentDetails
    {
        [Required]
        public string Amount { get; set; }

    }

}
