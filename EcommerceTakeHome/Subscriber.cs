using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceTakeHome.Core.Domain;
using EcommerceTakeHome.DataAccess;

namespace EcommerceTakeHome.WebHost
{
    public class Subscriber
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        public Subscriber()
        {
            Order.ContactDetailsCreated += HandleOnContactDetailsCreated;
            Order.DeliveryCreated += HandleOnDeliveryCreated;
            Order.OrderCreated += HandleOnOrderCreated;
            Order.ContactDetailsError += HandleOnContactDetailsError;
            Order.DeliveryError += HandleOnDeliveryError;
            Order.PaymentCreated += HandleOnPaymentCreated;
            Order.PaymentError += HandleOnPaymentError;
            Order.OrderSuccess += HandleOnOrderSuccess;
            Order.OrderFailed += HandleOnOrderFailed;
        }

        private void Run(OrderStep step, EnumStatus status)
        {
            step.Status = status;
            unitOfWork.Save();
        }

        private void HandleOnContactDetailsCreated(object sender, OrderEventArgs e)
        {
            var step = unitOfWork.OrderStepRepository.Get(x => x.OrderId == e.OrderId && x.StepName == EnumStepName.ContactDetails).FirstOrDefault();
            step.Status = EnumStatus.Success;

            var step2 = unitOfWork.OrderStepRepository.Get(x => x.OrderId == e.OrderId && x.StepName == EnumStepName.ProcessPayment).FirstOrDefault();
            step2.Status = EnumStatus.Started;

            var step3 = unitOfWork.OrderStepRepository.Get(x => x.OrderId == e.OrderId && x.StepName == EnumStepName.ProcessDeliveryAppointment).FirstOrDefault();
            step3.Status = EnumStatus.Started;
            unitOfWork.Save();
        }

        private void HandleOnOrderCreated(object sender, OrderEventArgs e)
        {
            
            var step1 = new OrderStep
            {
                OrderId = e.OrderId,
                OrderStepId = Guid.NewGuid(),
                StepName = EnumStepName.ProcessDeliveryAppointment,
                Status = EnumStatus.Pending
            };

            var step2 = new OrderStep
            {
                OrderId = e.OrderId,
                OrderStepId = Guid.NewGuid(),
                StepName = EnumStepName.ContactDetails,
                Status = EnumStatus.Pending
            };

            var step3 = new OrderStep
            {
                OrderId = e.OrderId,
                OrderStepId = Guid.NewGuid(),
                StepName = EnumStepName.ProcessPayment,
                Status = EnumStatus.Pending
            };
            unitOfWork.OrderStepRepository.Insert(step1);
            unitOfWork.OrderStepRepository.Insert(step2);
            unitOfWork.OrderStepRepository.Insert(step3);
            unitOfWork.Save();
        }


        private void HandleOnDeliveryCreated(object sender, OrderEventArgs e)
        {
            var step = unitOfWork.OrderStepRepository.Get(x => x.OrderId == e.OrderId && x.StepName == EnumStepName.ProcessDeliveryAppointment).FirstOrDefault();
            Run(step, EnumStatus.Success);
            var order = unitOfWork.OrderRepository.Get(x => x.Id == e.OrderId, null, includeProperties: "Steps").FirstOrDefault();
            if (order.Steps.TrueForAll(x => x.Status == EnumStatus.Success)) order.OnOrderSuccess(new OrderEventArgs { OrderId = order.Id });
        }

        private void HandleOnDeliveryError(object sender, OrderEventArgs e)
        {
            var step = unitOfWork.OrderStepRepository.Get(x => x.OrderId == e.OrderId && x.StepName == EnumStepName.ProcessDeliveryAppointment).FirstOrDefault();
            Run(step, EnumStatus.Fail);
        }

        private void HandleOnPaymentError(object sender, OrderEventArgs e)
        {
            var step = unitOfWork.OrderStepRepository.Get(x => x.OrderId == e.OrderId && x.StepName == EnumStepName.ProcessPayment).FirstOrDefault();
            Run(step, EnumStatus.Fail);
        }


        private void HandleOnContactDetailsError(object sender, OrderEventArgs e)
        {
            var step = unitOfWork.OrderStepRepository.Get(x => x.OrderId == e.OrderId && x.StepName == EnumStepName.ContactDetails).FirstOrDefault();
            Run(step, EnumStatus.Fail);
        }


        private void HandleOnPaymentCreated(object sender, OrderEventArgs e)
        {
            var step = unitOfWork.OrderStepRepository.Get(x => x.OrderId == e.OrderId && x.StepName == EnumStepName.ProcessPayment).FirstOrDefault();
            Run(step, EnumStatus.Success);
            
            var order =unitOfWork.OrderRepository.Get(x => x.Id == e.OrderId, null, includeProperties: "Steps").FirstOrDefault();
            if (order.Steps.TrueForAll(x=>x.Status==EnumStatus.Success)) order.OnOrderSuccess(new OrderEventArgs{OrderId = order.Id});

        }

        private void HandleOnOrderSuccess(object sender, OrderEventArgs e)
        {
            var order = unitOfWork.OrderRepository.GetByID(e.OrderId);
            order.CompletedAt = DateTime.UtcNow;
            order.Status = EnumStatus.Success;
          
            unitOfWork.Save();
        }

        private void HandleOnOrderFailed(object sender, OrderEventArgs e)
        {

        }
    }
}

