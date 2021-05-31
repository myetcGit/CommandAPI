//Section 1: We use IEnumerable (from System.Collections..), as well as Command (from CommandAPI.Models) 
using System.Collections.Generic;
using CommandAPI.Models;

namespace CommandAPI.Data{
    //Stection 2: We specify a public interface and give it a name ,
    //starting with capital “I” to denote it’s an interface.
    public interface ICommandAPIRepo{
        //Section 3: We specify that our Repository should provide a “Save Changes”method.
        //we’ll revisit this when we come to talking about Entity Framework Core in Chapter 7 
        bool SaveChanges();

        //Section 4 defines all the other method signatures that consumers of this interface can use
        //to obtain and manipulate data.
        IEnumerable<Command> GetAllCommands();
        Command GetCommandById(int id);
        void CreateCommand(Command cmd);
        void UpdateCommand(Command cmd);
        void DeleteCommand(Command cmd);
    }
}