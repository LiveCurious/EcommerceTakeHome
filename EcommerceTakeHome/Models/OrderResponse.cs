using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceTakeHome.Core.Domain;


namespace EcommerceTakeHome.WebHost.Models
{
    public class OrderResponse
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public decimal? TotalAmount { get; set; }
        public string ErrorMessage { get; set; }
        public List<OrderStepResponse> Steps { get; set; }
        public string Status { get; set; }

        public OrderResponse(Order order)
        {
            Id = order.Id;
            CompletedAt = order.CompletedAt;
            CreatedAt = order.CreatedAt;

            Status = order.Status.ToString();
            TotalAmount = order.PaymentDetail?.Amount;
            Steps = new List<OrderStepResponse>();
            foreach (var step in order.Steps)
            {
                var stepResponse = new OrderStepResponse
                {
                    Status = step.Status.ToString(),
                    StepName = step.StepName.ToString(),
                    Id = step.OrderStepId

                };
                Steps.Add(stepResponse);
                if (step.Status == EnumStatus.Fail) ErrorMessage += string.Format("The step  {0}  has failed", step.StepName.ToString());
            }
        }


    }
}


