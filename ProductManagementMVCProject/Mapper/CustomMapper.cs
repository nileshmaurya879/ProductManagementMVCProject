using AutoMapper;
using ProductManagementMVCProject.Dto;
using ProductManagementMVCProject.Models;

namespace ProductManagementMVCProject.Mapper
{
    public class CustomMapper: Profile
    {
        public CustomMapper() 
        {
            CreateMap<tblUserDto, TblUser>().ReverseMap();
            CreateMap<TblCategory, tblCategoryDto>().ReverseMap();
            CreateMap<tblProductDto, TblProduct>().ReverseMap();
        }
    }
}
