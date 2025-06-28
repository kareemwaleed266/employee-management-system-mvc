using AutoMapper;
using Demo.DAL.Entities;
using Demo.PL.Controllers;
using Demo.PL.Models;
using System.Security.AccessControl;

namespace Demo.PL.Mapper
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
        }
    }
}
