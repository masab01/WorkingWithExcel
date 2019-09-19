using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Taskk.Models
{
    public class ClientViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public System.DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string SocialNumber { get; set; }
    }
}