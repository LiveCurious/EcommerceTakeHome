using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceTakeHome.Core.Domain
{


    public class Order
    {
        public delegate void OrderEventHandler(object sender, OrderEventArgs e);


        public static event OrderEventHandler OrderCreated ;
        public static event OrderEventHandler ContactDetailsCreated ;
        public static event OrderEventHandler ContactDetailsError;
        public static event OrderEventHandler PaymentCreated ;
        public static event OrderEventHandler PaymentError;
        public static event OrderEventHandler DeliveryCreated ;
        public static event OrderEventHandler DeliveryError;
        public static event OrderEventHandler OrderSuccess;
        public static event OrderEventHandler OrderFailed;

        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public DateTime? CompletedAt { get; set; }

        public List<OrderStep> Steps { get; set; }
        public EnumStatus Status { get; set; }
        public ContactDetail.ContactDetail ContactDetail { get; set; }
        public DeliveryAppointment.DeliveryAppointment DeliveryAppointment { get; set; }
        public PaymentDetail.PaymentDetail PaymentDetail { get; set; }

    


        public virtual void OnContactDetailsError(OrderEventArgs e)
        {
            OrderEventHandler handler = ContactDetailsError;
            Run(e, handler);
        }

        public virtual void OnOrderCreated(OrderEventArgs e)
        {
            OrderEventHandler handler = OrderCreated;
            Run(e, handler);
        }



        public virtual void OnContactDetailsCreated(OrderEventArgs e)
        {
            OrderEventHandler handler = ContactDetailsCreated;
            Run(e, handler);

        }

        public virtual void OnPaymentCreated(OrderEventArgs e)
        {
            OrderEventHandler handler = PaymentCreated;
            Run(e, handler);

        }
        public virtual void OnPaymentError(OrderEventArgs e)
        {
            OrderEventHandler handler = PaymentError;
            Run(e, handler);

        }

        public virtual void OnDeliveryCreated(OrderEventArgs e)
        {
            OrderEventHandler handler =DeliveryCreated;
            Run(e, handler);

        }

        public virtual void OnOrderSuccess(OrderEventArgs e)
        {
            OrderEventHandler handler = OrderSuccess;
            Run(e, handler);

        }

        public virtual void OnOrderFailed(OrderEventArgs e)
        {
            OrderEventHandler handler = OrderFailed;
            Run(e, handler);

        }

        public virtual void OnDeliveryError(OrderEventArgs e)
        {
            OrderEventHandler handler = DeliveryError;
            Run(e, handler);

        }
        private void Run(OrderEventArgs e, OrderEventHandler handler)
        {
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
