using System.Collections.Generic;
using System.Threading.Tasks;
using TestsGenerator.Pipeline;

namespace TestsGenerator
{
    public class TestsGenerator
    {
        private readonly Config _pipelineConfig;

        public TestsGenerator(Config pipelineConfig)
        {
            _pipelineConfig = pipelineConfig;
        }

        public Task Generate(IEnumerable<Task<string>> readFileTasks)
        {
            Pipeline.Pipeline pipeline = new Pipeline.Pipeline(_pipelineConfig);
            
            pipeline.PostRange(readFileTasks);
            pipeline.Complete();

            return pipeline.Completion;
        }
   }
}