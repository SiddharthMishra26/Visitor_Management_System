using Visitor_Management_System.Common;

namespace Visitor_Management_System.Entities
{
    public class LoginEntity : BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
