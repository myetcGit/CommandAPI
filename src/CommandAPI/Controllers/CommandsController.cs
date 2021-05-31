using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CommandAPI.Data;
using CommandAPI.Models;

namespace CommandAPI.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        //Apply constructor Dependency Injection
        //Step 1
        private readonly ICommandAPIRepo _repository;
        
        //Step 2
        public CommandsController(ICommandAPIRepo repository){
            _repository = repository;
        }

        // [HttpGet]
        // public ActionResult<IEnumerable<string>> Get()
        // {
        //     return new string[] {"this", "is", "hard", "coded"};
        // }
        
        [HttpGet]
        public ActionResult<IEnumerable<Command>> GetAllCommands(){
            var commandItems = _repository.GetAllCommands();
            return Ok(commandItems);
        }

        [HttpGet("{id}")]
         //The method signature name 'GetCommandById' can be any name,
         //because the run time will call '_repository.GetCommandById(id)' inside this method
        public ActionResult<Command> GetCommandById(int id){ 
            var commandItems = _repository.GetCommandById(id);
            if(commandItems == null){
                return NotFound();
            }
            return Ok(commandItems);
        }        
    }
}