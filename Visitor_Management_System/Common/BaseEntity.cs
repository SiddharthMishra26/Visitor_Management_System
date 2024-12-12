using Newtonsoft.Json;
using System.Drawing;

namespace Visitor_Management_System.Common
{
    public class BaseEntity
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        public string UId { get; set; }
        public string dType { get; set; }
        public int Version { get; set; }
        public bool Active { get; set; }
        public bool Archived { get; set; }
        public DateTime CreatedOn { get; set; } 
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedByName { get; set; }

        public void Initialize (bool isNew, string documentType, string createdOrUpdatedBy, string createdorUpdatedByName)
        {
            dType = documentType;
            Id = Guid.NewGuid().ToString();
            Active = true;
            Archived = false;

            if(isNew)
            {
                UId = Id;
                Version = 1;
                CreatedBy = createdOrUpdatedBy;
                CreatedByName = createdorUpdatedByName;
                UpdatedBy = createdOrUpdatedBy;
                UpdatedByName = createdorUpdatedByName;
                CreatedOn = DateTime.Now;
                UpdatedOn = CreatedOn;
            }
            else
            {
                Version++;
                UpdatedOn = DateTime.Now;
                UpdatedBy = createdOrUpdatedBy;
                UpdatedByName = createdorUpdatedByName;
            }
        }
    }
}
