//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace EcommerceTakeHome.WebHost.Events
//{
//    public class OrderSubscriber
//    {
//        private readonly IMessageHub _hub;
//        private readonly Guid _subscriptionToken;
        
//        public OrderSubscriber(IMessageHub hub)
//        {
//            _hub = hub;
//            _subscriptionToken = _hub.Subscribe<Order>(OnNewOrder);
//        }

//        public void Unsubscribe()
//        {
//            _hub.Unsubscribe(_subscriptionToken);
//        }

//        private void OnNewOrder(Order order)
//        {
//            /* do something with the order */
//        }
//    }

//}
