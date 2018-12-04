using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using TestsGenerator.Pipeline.BlocksConfig;

namespace TestsGenerator.Pipeline.BlockBuilders
{
    public class ReaderBlockBuilder : IBuilder
    {
        public IDataflowBlock Create(BaseBlockConfig config)
        {
            return new TransformBlock<Task<string>, string>(async task => await task, config.ToOptions()); 
        }
    }
}