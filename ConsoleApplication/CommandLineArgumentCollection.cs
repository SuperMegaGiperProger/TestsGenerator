using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication
{
    public class CommandLineArgumentCollection
    {
        public List<string> Arguments;
        public Dictionary<string, string> Parameters;

        public CommandLineArgumentCollection()
        {
            Arguments = new List<string>();
            Parameters = new Dictionary<string, string>();
        }

        public string this[int i] => Arguments[i];
        public string this[string key] => Parameters[key];
        
        public CommandLineArgumentCollection(string[] args)
        {
            List<CommandLineArgument> commandLineArguments = CommandLineArgument.CreateList(args);

            int firstKeyIndex = commandLineArguments.FindIndex(arg => arg.IsKey);

            Arguments = commandLineArguments.GetRange(0, firstKeyIndex).Select(arg => arg.Value).ToList();
            Parameters = commandLineArguments
                .GetRange(firstKeyIndex, commandLineArguments.Count - firstKeyIndex)
                .ToDictionary(arg => arg.Key, arg => arg.Value);
        }
    }
}