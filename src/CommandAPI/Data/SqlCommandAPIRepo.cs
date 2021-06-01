using System.Collections.Generic;
using CommandAPI.Models;

using System.Linq;

namespace CommandAPI.Data{
    public class SqlCommandAPIRepo : ICommandAPIRepo
    {
        //To begin, we’re going to use Constructor Dependency Injection to inject our DB Context into our SqlCommandAPIRepo class (so we can use it). 
        //Remember that we registered our DB Context class with the Service Container in the Startup class so it is available for “injection.”
        private readonly CommandContext _context;
        public SqlCommandAPIRepo(CommandContext context){
            _context = context;
        }
        public void CreateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }
        //Implementation 1
        public IEnumerable<Command> GetAllCommands()
        {
            return _context.CommandItems.ToList();
            //throw new System.NotImplementedException();
        }
        //Implementation 2
        public Command GetCommandById(int id)
        {
            //require using System.Linq
            return _context.CommandItems.FirstOrDefault(p => p.Id ==id);
            //throw new System.NotImplementedException();
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }
    }//class
}//namespace