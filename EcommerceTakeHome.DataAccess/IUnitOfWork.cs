using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using EcommerceTakeHome.Core.Abstractions;
using EcommerceTakeHome.Core.Domain;
using EcommerceTakeHome.Core.Domain.ContactDetail;
using EcommerceTakeHome.Core.Domain.DeliveryAppointment;
using EcommerceTakeHome.Core.Domain.PaymentDetail;

namespace EcommerceTakeHome.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        MyDbContext Context { get;  } 
        IRepository<Order> OrderRepository { get;  }
        IRepository<OrderStep> OrderStepRepository { get;  }
        public void Save();
     //   protected void Dispose(bool disposing);

        public void Dispose();

    }
}
