//This is a concrete class that implements our interface using our model.
//However, we’ll just be using “mock” data at this stage. 
//(we’ll create another implementation of our interface to use “real” data in the next chapter)

//Note: To demonstrate the core concepts of using interfaces and by extension Dependency Injection., 
//we are only going to write code for GetAllCommands(), and GetCommandById(int id)
using System.Collections.Generic;
using CommandAPI.Models;

namespace CommandAPI.Data{
    public class MockCommandAPIRepo : ICommandAPIRepo
    {
        public void CreateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        //Write our code here
        public IEnumerable<Command> GetAllCommands()
        {
            var commands = new List<Command>{
                new Command{
                    Id=0, HowTo="How to generate a migration",
                    CommandLine="dotnet ef migrations andd <Name of Migration>",
                    Platform=".Net Core EF"},
                new Command{
                    Id=1, HowTo="Run migrations",
                    CommandLine="dotnet ef ef database update",
                    Platform=".Net Core EF"},                                    
                new Command{
                    Id=2, HowTo="List active migrations",
                    CommandLine="dotnet ef ef migration list",
                    Platform=".Net Core EF"},                
            };
            return commands;
        }

        public Command GetCommandById(int id)
        {
            return new Command{
                Id=0, HowTo="How to generate a migration",
                CommandLine="dotnet ef migrations andd <Name of Migration>",
                Platform=".Net Core EF"};
        }


        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

    }
}