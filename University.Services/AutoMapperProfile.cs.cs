using AutoMapper;
using University.Entity.Entities;
using University.Entity.Dto;


namespace University.Services;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Course, CourseDto>()
        .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.CourseName));

        CreateMap<User, UserDto>();
    }
}
