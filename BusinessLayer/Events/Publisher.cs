//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace EcommerceTakeHome.WebHost.Events
//{


//    public class OrderPublisher
//    {

//        public event EventHandler OrderCreation;
//        public event EventHandler ContactDetailsCreated;
//        public event EventHandler ContactDetailsError;

//        public event EventHandler PaymentCreated;
//        public event EventHandler PaymentError;

//        public event EventHandler DeliveryCreated;
//        public event EventHandler DeliveryError;
//        public event EventHandler FetchAllSteps;

//        private readonly IMessageHub _hub;

//        public OrderPublisher(IMessageHub hub)
//        {
//            _hub = hub;
//        }

//        public void Publish()
//        {
//            _hub.Publish(new OrderPlaced());
//        }

//        public class Order { }
//        public sealed class OrderPlaced : Order { }
//    }

//}

