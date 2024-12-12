﻿using Visitor_Management_System.Common;

namespace Visitor_Management_System.Entities
{
    public class SecurityEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Age { get; set; }
    }
}
