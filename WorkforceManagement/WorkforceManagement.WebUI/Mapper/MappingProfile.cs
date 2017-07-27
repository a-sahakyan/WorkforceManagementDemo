using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkforceManagement.BLL.DataProvider;
using WorkforceManagement.Domain.Entities;
using WorkforceManagement.ViewModel.ViewModels;

namespace WorkforceManagement.WebUI.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeModel, EmployeeAuthDataViewModel>();
        }
    }
}
