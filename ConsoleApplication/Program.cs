using System;
using System.Linq;
using System.Threading.Tasks;
using TestsGenerator.Generators;
using TestsGenerator.Pipeline;

namespace ConsoleApplication
{
    class Program
    {
        public static readonly string[] REQUIRED_KEYS = {"dest", "max-read", "max-gen", "max-write"};

        static void Main(string[] args)
        {
            var arguments = new CommandLineArgumentCollection(args);
            
            if (!validateArgumentsPresence(arguments)) return;

            Console.WriteLine("Generation has started!");

            var pipelineConfig = new Config(
                new XUnitGenerator(),
                new Writer(arguments["dest"]),
                int.Parse(arguments["max-read"]),
                int.Parse(arguments["max-gen"]),
                int.Parse(arguments["max-write"])
            );

            var generator = new TestsGenerator.TestsGenerator(pipelineConfig);

            var reader = new Reader();

            Task generateTask = generator.Generate(arguments.Arguments.Select(path => reader.ReadAsync(path)));

            generateTask.Wait();

            Console.WriteLine("Generation has finished!");
        }

        private static bool validateArgumentsPresence(CommandLineArgumentCollection args)
        {
            bool hasAllKeys = REQUIRED_KEYS.All(args.Parameters.ContainsKey);

            if (!hasAllKeys)
            {
                string pattern = String.Join(" ",
                    REQUIRED_KEYS.Select(key => $"{CommandLineArgument.KEY_PREFIX}{key} [{key}-value]"));

                Console.WriteLine($"Error: key was missed! Pattern:\n\tConsoleApplication.dll [file-paths] {pattern}");
            }

            return hasAllKeys;
        }
    }
}