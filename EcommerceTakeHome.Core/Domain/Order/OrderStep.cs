using System;

namespace EcommerceTakeHome.Core.Domain
{
    public class OrderStep
    {
        public Guid OrderStepId { get; set; }
        public EnumStepName StepName { get; set; }
        public EnumStatus Status { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }

    }

}