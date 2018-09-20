using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactManagementWebapi.Models
{
    public class Contact
    {
        public Contact()
        {

        }
        public int Id { get; set; }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Status { get; set; }

        public string PhoneNumber { get; set; }
    }
}