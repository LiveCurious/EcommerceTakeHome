using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceTakeHome.Core.Domain;


namespace EcommerceTakeHome.WebHost.Models
{
    public class ShortOrderResponse
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public string Status { get; set; }

        public ShortOrderResponse(Order order)
        {
            Id = order.Id;

            CreatedAt = order.CreatedAt;

            Status = order.Status.ToString();
        }
    }

}
