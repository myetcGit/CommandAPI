using AutoMapper;
using CommandAPI.Dtos;
using CommandAPI.Models;

namespace CommandAPI.Profiles{
    //inherits from Automapper.Profile
    public class CommandsProfile : Profile{
        public CommandsProfile(){
            //We use the CreateMap method to map our source object (Command) to our target object (CommandReadDto)
            //For GET
            CreateMap<Command,CommandReadDto>();
            //For POST
            CreateMap<CommandCreateDto,Command>();
            //For PUT
            CreateMap<CommandUpdateDto,Command>();
            //for PATCH
            CreateMap<Command,CommandUpdateDto>();
        }
    }
}//namespace