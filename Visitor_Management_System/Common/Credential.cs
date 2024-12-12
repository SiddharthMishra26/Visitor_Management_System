namespace Visitor_Management_System.Common
{
    public class Credential
    {
        public static readonly string cosmosUrl = Environment.GetEnvironmentVariable("CosmosURl");
        public static readonly string authkey = Environment.GetEnvironmentVariable("AuthToken");
        public static readonly string database = Environment.GetEnvironmentVariable("DatabaseName");
        public static readonly string container = Environment.GetEnvironmentVariable("ContainerName");

        public static readonly string visitorDocumentType = "Visitor";
        public static readonly string managerDocumentType = "Manager";
        public static readonly string securityDocumentType = "Security";
        public static readonly string officeDocumentType = "Office";
        public static readonly string loginDocumentType = "Login";
        public static readonly string passDocumentType = "Pass";
    }
}
