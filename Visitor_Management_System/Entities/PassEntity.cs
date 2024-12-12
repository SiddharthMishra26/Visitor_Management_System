using Visitor_Management_System.Common;

namespace Visitor_Management_System.Entities
{
    public class PassEntity : BaseEntity
    {
        public string VisitorName { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
    }
}
