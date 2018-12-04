using System;
using System.Threading.Tasks.Dataflow;
using TestsGenerator.Generators;
using TestsGenerator.Pipeline.BlocksConfig;

namespace TestsGenerator.Pipeline.BlockBuilders
{
    public class GeneratorBlockBuilder : IBuilder
    {
        public IDataflowBlock Create(BaseBlockConfig config)
        {
            var generatorBlockConfig = (GeneratorBlockConfig) config;

            return new TransformManyBlock<string, GeneratedFileDescription>(
                new Func<string, GeneratedFileDescription[]>(generatorBlockConfig.CodeGenerator.GenerateTestFiles),
                generatorBlockConfig.ToOptions()
             );
        }
    }
}