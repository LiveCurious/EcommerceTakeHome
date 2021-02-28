using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EcommerceTakeHome.Core.Domain.DeliveryAppointment
{
    public class DeliveryAppointment
    {
        public Guid DeliveryAppointmentId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime AppointmentDateTime { get; set; }
    }
}
