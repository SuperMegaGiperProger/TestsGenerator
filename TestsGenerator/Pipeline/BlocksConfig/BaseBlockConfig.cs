using System.Threading.Tasks.Dataflow;

namespace TestsGenerator.Pipeline.BlocksConfig
{
    public class BaseBlockConfig
    {
        private int _maxDegreeOfParallelism;

        public BaseBlockConfig(int maxDegreeOfParallelism)
        {
            _maxDegreeOfParallelism = maxDegreeOfParallelism;
        }
        
        public ExecutionDataflowBlockOptions ToOptions()
        {
            return new ExecutionDataflowBlockOptions() {MaxDegreeOfParallelism = _maxDegreeOfParallelism};
        }
    }
}