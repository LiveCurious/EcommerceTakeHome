using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using EcommerceTakeHome;
using EcommerceTakeHome.DataAccess;
using EcommerceTakeHome.WebHost.Controllers;
using EcommerceTakeHome.WebHost.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;


namespace EcommerceTest
{
    public class OrdersControllerTest
    {


        [Fact]
        public async Task Request_NonExistent_Order_Should_return_404()
        {
            var unitOfWork = new Mock<UnitOfWork>();
            unitOfWork.Setup()
         //   var mockRepo = new Mock<IBrainstormSessionRepository>();
            var controller = new OrdersController(unitOfWork.Object);
            var nonExistentOrderId = Guid.NewGuid();

            // Act
            var result = await controller.Get(nonExistentOrderId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<OrderResponse>>(result);
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
        }
    }
}
