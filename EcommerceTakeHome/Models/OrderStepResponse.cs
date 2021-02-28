using System;

namespace EcommerceTakeHome.WebHost.Models
{
    public class OrderStepResponse
    {
        public Guid Id { get; set; }
        public string StepName { get; set; }
        public string Status { get; set; }
    }
}