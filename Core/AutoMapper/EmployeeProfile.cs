using AutoMapper;
using Core.Entities;
using Core.Requests;

namespace Core.AutoMapper
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, CreateEmployee>().ReverseMap();
            CreateMap<Employee, UpdateEmployee>().ReverseMap();
        }
    }
}