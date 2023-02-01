using UnityEngine;
using static Console.DevConsole;


namespace Console{
    public class CommandExample : ConsoleCommand
    {
        public sealed override string Name { get; protected set; }
        public sealed override string Command { get; protected set; }
        public sealed override string Description { get; protected set; }
        public sealed override string Help { get; protected set; }
        public sealed override string Example { get; protected set; }

        public CommandExample()
        {
            Name = ""; // Name of command
            Command = ""; // Command Call name
            Description = ""; // Description of command
            Help = ""; // Help Description of Command
            Example = ""; // Example of Command
            AddCommandToConsole();
        }

        public override void RunCommand(string[] data)
        {
            if (data.Length == 2) // Checks if right format
            {
               // data. is the data to use in command   
            } 
        }

        public static CommandExample CreateCommand()
        {
            return new CommandExample();
        }
        
    }
}

