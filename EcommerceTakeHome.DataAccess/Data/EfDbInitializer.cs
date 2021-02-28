using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;


namespace EcommerceTakeHome.DataAccess.Data
{
    public class EfDbInitializer
        : IDbInitializer
    {
        private readonly MyDbContext _dataContext;

        public EfDbInitializer(MyDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void InitializeDb()
        {
          //  _dataContext.Database.EnsureDeleted();
           _dataContext.Database.EnsureCreated();
            
        }
    }
}

