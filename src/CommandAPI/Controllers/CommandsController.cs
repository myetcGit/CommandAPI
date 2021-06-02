using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CommandAPI.Data;
using CommandAPI.Models;

//the two namespaces below are for DTO mapping
using AutoMapper;
using CommandAPI.Dtos;

using Microsoft.AspNetCore.JsonPatch;

namespace CommandAPI.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        //Apply constructor Dependency Injection
        //Step 1
        private readonly ICommandAPIRepo _repository;
        
        //Constructor Dependency Injection for mapping DTO
        private readonly IMapper _mapper;
        
        //Step 2
        public CommandsController(ICommandAPIRepo repository, IMapper mapper){
            _repository = repository;
            _mapper = mapper;
        }

        ////Hardcoded
        // [HttpGet]
        // public ActionResult<IEnumerable<string>> Get()
        // {
        //     return new string[] {"this", "is", "hard", "coded"};
        // }
        
        ////Without using DTO
        // [HttpGet]
        // public ActionResult<IEnumerable<Command>> GetAllCommands(){
        //     var commandItems = _repository.GetAllCommands();
        //     return Ok(commandItems);
        // }

        //Using DTO
        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands(){
            var commandItems = _repository.GetAllCommands();
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }

        // //Without using DTO
        // [HttpGet("{id}")]
        //  //The method signature name 'GetCommandById' can be any name,
        //  //because the run time will call '_repository.GetCommandById(id)' inside this method
        // public ActionResult<Command> GetCommandById(int id){ 
        //     var commandItems = _repository.GetCommandById(id);
        //     if(commandItems == null){
        //         return NotFound();
        //     }
        //     return Ok(commandItems);
        // }

         //Using DTO
         //The 'GetCommandById' name is from CreateCommand method of the HttpPost below (see pdf page 221 for explanation)
        [HttpGet("{id}",Name ="GetCommandById")]
         //The method signature name 'GetCommandById' can be any name,
         //because the run time will call '_repository.GetCommandById(id)' inside this method
        public ActionResult<CommandReadDto> GetCommandById(int id){ 
            var commandItems = _repository.GetCommandById(id);
            if(commandItems == null){
                return NotFound();
            }
            return Ok(_mapper.Map<CommandReadDto>(commandItems));
        }

        [HttpPost]
        public ActionResult <CommandReadDto> CreateCommand (CommandCreateDto commandCreateDto)
        {
            var commandModel = _mapper.Map<Command>(commandCreateDto);
            _repository.CreateCommand(commandModel);
            _repository.SaveChanges();
            var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);
            return CreatedAtRoute(nameof(GetCommandById),new {Id = commandReadDto.Id}, commandReadDto);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
        {
            var commandModelFromRepo = _repository.GetCommandById(id);
            if (commandModelFromRepo == null)
            {
            return NotFound();
            }
            _mapper.Map(commandUpdateDto, commandModelFromRepo);
            _repository.UpdateCommand(commandModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

        //patch
        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id,JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            var commandModelFromRepo = _repository.GetCommandById(id);
            if(commandModelFromRepo == null)
            {
                return NotFound();
            }
            
            var commandToPatch = _mapper.Map<CommandUpdateDto>(commandModelFromRepo);
            patchDoc.ApplyTo(commandToPatch, ModelState);
            
            if(!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }
            
            _mapper.Map(commandToPatch, commandModelFromRepo);
            _repository.UpdateCommand(commandModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }//patch
        
        //Delete
        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var commandModelFromRepo = _repository.GetCommandById(id);
            
            if(commandModelFromRepo == null)
            {
                return NotFound();
            }
            
            _repository.DeleteCommand(commandModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

    }//class
}//namespace