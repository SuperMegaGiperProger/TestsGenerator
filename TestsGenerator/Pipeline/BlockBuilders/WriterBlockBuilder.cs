using System;
using System.Threading.Tasks.Dataflow;
using TestsGenerator.Generators;
using TestsGenerator.Pipeline.BlocksConfig;

namespace TestsGenerator.Pipeline.BlockBuilders
{
    public class WriterBlockBuilder : IBuilder
    {
        public IDataflowBlock Create(BaseBlockConfig config)
        {
            var writerBlockConfig = (WriterBlockConfig) config;

            Action<GeneratedFileDescription> action = fileDescription =>
                writerBlockConfig.Writer.WriteAsync(fileDescription.Name, fileDescription.Text);

            return new ActionBlock<GeneratedFileDescription>(action, writerBlockConfig.ToOptions());
        }
    }
}