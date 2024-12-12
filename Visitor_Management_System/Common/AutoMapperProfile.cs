using AutoMapper;
using Visitor_Management_System.Entities;
using Visitor_Management_System.Models;
namespace Visitor_Management_System.Common
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ManagerEntity, ManagerModel>().ReverseMap();
            CreateMap<OfficeEntity, OfficeModel>().ReverseMap();
            CreateMap<VisitorEntity, VisitorModel>().ReverseMap();
            CreateMap<SecurityEntity, SecurityModel>().ReverseMap();
            CreateMap<PassEntity, PassModel>().ReverseMap();
            CreateMap<LoginEntity, LoginModel>().ReverseMap();
        }
    }
}
