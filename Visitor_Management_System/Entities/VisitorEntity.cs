using Visitor_Management_System.Common;

namespace Visitor_Management_System.Entities
{
    public class VisitorEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string VisitingTo { get; set; }
        public string Purpose { get; set; }
        public string EntryTime { get; set; }
        public string ExitTime { get; set; }
    }
}
