using EcommerceTakeHome.Core.Abstractions;
using EcommerceTakeHome.Core.Domain;
using EcommerceTakeHome.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EcommerceTakeHome.Core.Domain.ContactDetail;
using EcommerceTakeHome.Core.Domain.DeliveryAppointment;
using EcommerceTakeHome.Core.Domain.PaymentDetail;
//using EcommerceTakeHome.WebHost.Events;
using EcommerceTakeHome.WebHost.Models;
using Microsoft.AspNetCore.Http;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EcommerceTakeHome.WebHost.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        // public event EventHandler  ContactDetailsCreated;

        public OrdersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<OrderResponse>> Get(Guid id)
        {
           var order= _unitOfWork.OrderRepository.Get(x => x.Id == id, null, "Steps").FirstOrDefault();
            return StatusCode(200, new OrderResponse(order));
        }


        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [HttpPut("contact/{id:guid}")]
        public async Task<ActionResult> Put(string id, [FromBody] OrderRequestContactDetails obj)
        {
            if (!ModelState.IsValid) return BadRequest(new ExceptionResponse { ErrorMessage = ModelState.Root.Errors[0].ErrorMessage, Status = StatusCodes.Status404NotFound.ToString() });


            var order = _unitOfWork.OrderRepository.GetByID(Guid.Parse(id));

            if (order == null) return NotFound(new ExceptionResponse { ErrorMessage = "Order not found", Status = StatusCodes.Status404NotFound.ToString() });

            order.ContactDetail = new ContactDetail { ContactDetailId = Guid.NewGuid(), Email = obj.Email };

            try
            {
                _unitOfWork.Save();
            }
            catch (ExceptionSaveToEF e)
            {
                order.OnContactDetailsError(new OrderEventArgs { OrderId = Guid.Parse(id) });
                return StatusCode(500);
            }

            order.OnContactDetailsCreated(new OrderEventArgs { OrderId = Guid.Parse(id) });
            return Ok(order.ContactDetail);
        }


        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [HttpPut("delivery/{id:guid}")]

        public async Task<ActionResult> Put(string id, [FromBody] OrderRequestDeliveryAppointment obj)
        {
            if (!ModelState.IsValid) return BadRequest(new ExceptionResponse { ErrorMessage = ModelState.Root.Errors[0].ErrorMessage, Status = StatusCodes.Status404NotFound.ToString() });

            var order = _unitOfWork.OrderRepository.GetByID(Guid.Parse(id));
            if (order == null) return NotFound(new ExceptionResponse { ErrorMessage = "Order not found", Status = StatusCodes.Status404NotFound.ToString() });

            order.DeliveryAppointment = new DeliveryAppointment() { AppointmentDateTime = DateTime.Parse(obj.DateTime), DeliveryAppointmentId = Guid.NewGuid() };
            try
            {
                _unitOfWork.Save();
            }
            catch (ExceptionSaveToEF e)
            {
                order.OnDeliveryError(new OrderEventArgs { OrderId = Guid.Parse(id) });
                return StatusCode(500);
            }
            order.OnDeliveryCreated(new OrderEventArgs { OrderId = Guid.Parse(id) });
            return Ok(order.DeliveryAppointment);
        }


        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [HttpPut("payment/{id:guid}")]

        public async Task<ActionResult> Put(string id, [FromBody] OrderRequestPaymentDetails obj)
        {
            if (!ModelState.IsValid) return BadRequest(new ExceptionResponse { ErrorMessage = ModelState.Root.Errors[0].ErrorMessage, Status = StatusCodes.Status404NotFound.ToString() });

            var order = _unitOfWork.OrderRepository.GetByID(Guid.Parse(id));
            if (order == null) return NotFound(new ExceptionResponse { ErrorMessage = "Order not found", Status = StatusCodes.Status404NotFound.ToString() });

            order.PaymentDetail = new PaymentDetail() { Amount = decimal.Parse(obj.Amount), PaymentDetailId = Guid.NewGuid() };
            try
            {
                _unitOfWork.Save();
            }
            catch (ExceptionSaveToEF e)
            {
                order.OnPaymentError(new OrderEventArgs { OrderId = Guid.Parse(id) });
                return StatusCode(500);
            }
            order.OnPaymentCreated(new OrderEventArgs { OrderId = Guid.Parse(id) });
            return Ok(order.PaymentDetail);
        }

        [HttpPost()]
        [ProducesResponseType(201)]
        public async Task<ActionResult<ShortOrderResponse>> Post()
        {
            var id = Guid.NewGuid();
            _unitOfWork.OrderRepository.Insert(new Order { Id = id, CreatedAt = DateTime.UtcNow, Status = EnumStatus.Started });
            try
            {
                _unitOfWork.Save();
            }
            catch (ExceptionSaveToEF e)
            {
                return StatusCode(500);
            }
            var order = _unitOfWork.OrderRepository.GetByID(id);
            order.OnOrderCreated(new OrderEventArgs { OrderId = id });
            return StatusCode(201,new ShortOrderResponse(order));
        }
        
    }
}
