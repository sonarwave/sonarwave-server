using AutoMapper;
using SonarWave.Application.Models;
using SonarWave.Core.Entities;

namespace SonarWave.Application
{
    /// <summary>
    /// Used for mapping entities to DTO's.
    /// </summary>
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<User, UserItem>().ReverseMap();

            CreateMap<Room, RoomItem>().ReverseMap();
        }
    }
}