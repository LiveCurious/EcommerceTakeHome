using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EcommerceTakeHome.Core.Domain.ContactDetail
{
   public class ContactDetail
   {
       public Guid ContactDetailId { get; set; }

       [EmailAddress]
       [Required()]
       public string Email { get; set; }
      
   }
}
