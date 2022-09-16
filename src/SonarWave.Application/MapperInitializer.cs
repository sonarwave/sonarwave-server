using AutoMapper;
using SonarWave.Application.Entities;
using SonarWave.Application.Models.User;

namespace SonarWave.Application
{
    /// <summary>
    /// Used for mapping entities to DTO's.
    /// </summary>
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<User, CreateUserRequest>().ReverseMap();
        }
    }
}