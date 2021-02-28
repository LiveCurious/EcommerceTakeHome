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
    public class UnitOfWork : IUnitOfWork
    {
        private MyDbContext context = new MyDbContext();
        private IRepository<Order> orderRepository;
        private IRepository<OrderStep> orderStepRepository;

        public IRepository<Order> OrderRepository
        {
            get
            {
                this.orderRepository ??= new EfRepository<Order>(context);
                return orderRepository;
            }
        }

        public IRepository<OrderStep> OrderStepRepository
        {
            get
            {
                this.orderStepRepository ??= new EfRepository<OrderStep>(context);
                return orderStepRepository;
            }
        }

        public MyDbContext Context
        {
            get
            {
                this.context = new MyDbContext();
                return  context;
            }
        }

        public void Save()
        {
            try
            {
                 context.SaveChanges();
            }
            catch (Exception e)

            {
                throw new ExceptionSaveToEF(e);
            }
  
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
