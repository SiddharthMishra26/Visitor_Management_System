using Visitor_Management_System.Common;

namespace Visitor_Management_System.Entities
{
    public class OfficeEntity : BaseEntity
    {
        public string OfficeName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string FloorNumber { get; set; }
        public string Address { get; set; }
    }
}
