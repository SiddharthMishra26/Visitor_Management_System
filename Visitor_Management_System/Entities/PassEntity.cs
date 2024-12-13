using Visitor_Management_System.Common;

namespace Visitor_Management_System.Entities
{
    public class PassEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string VisitingTo { get; set; }
        public string Time { get; set; }
        public string Purpose { get; set; }
        public string Status { get; set; }
    }
}
