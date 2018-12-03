using System;
using System.Collections.Generic;

namespace ConsoleApplication
{
    public class CommandLineArgument
    {
        public const string KEY_PREFIX = "-";
        
        private string _argument;
        private string _nextArgument;

        public bool IsKey => _argument.StartsWith(KEY_PREFIX);
        public bool IsArgument => !IsKey;

        public string Key
        {
            get
            {
                if (!IsKey) throw new Exception("Argument is not a key");

                return _argument.Substring(KEY_PREFIX.Length);
            }
        }

        public string Value => IsKey ? _nextArgument : _argument;

        public CommandLineArgument(string argument, string nextArgument = null)
        {
            _argument = argument;
            _nextArgument = nextArgument;
            
            _validate();
        }

        public static List<CommandLineArgument> CreateList(string[] args)
        {
            var result = new List<CommandLineArgument>();

            for (int i = 0; i < args.Length - 1; ++i)
            {
                var arg = new CommandLineArgument(args[i], args[i + 1]);

                if (arg.IsKey) ++i;
                
                result.Add(arg);
            }
            
            _validateCollection(result);

            return result;
        }

        private static void _validateCollection(IEnumerable<CommandLineArgument> collection)
        {
            bool isAnyKeyAppeared = false;

            foreach (CommandLineArgument arg in collection)
            {
                if (arg.IsKey)
                {
                    isAnyKeyAppeared = true;
                }
                else
                {
                    if (isAnyKeyAppeared) throw new Exception("Arguments can't be passed after keys");
                }
            }
        }
        
        private void _validate()
        {
            if (_argument.Length < 1) throw new Exception("Argument can't be blank");
            
            if (IsKey)
            {
                if (Key.Length < 1) throw new Exception("Key can't be blank");
                if (string.IsNullOrEmpty(Value)) throw new Exception("Value can't be blank");
            }
        }
    }
}