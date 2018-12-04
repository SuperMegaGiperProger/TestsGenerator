using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using TestsGenerator.Generators;
using TestsGenerator.Pipeline.BlockBuilders;

namespace TestsGenerator.Pipeline
{
    public class Pipeline
    {
        private readonly Config _config;

        private ITargetBlock<Task<string>> _firstBlock;
        private IDataflowBlock _lastBlock;

        public Pipeline(Config config)
        {
            _config = config;

            InitBlocks();
        }

        public bool Post(Task<string> readTask)
        {
            return _firstBlock.Post(readTask);
        }

        public void PostRange(IEnumerable<Task<string>> readTasks)
        {
            foreach (Task<string> readTask in readTasks)
            {
                Post(readTask);
            }
        }

        public void Complete()
        {
            _firstBlock.Complete();
        }

        public Task Completion => _lastBlock.Completion;

        private void InitBlocks()
        {
            var readerBlock = new ReaderBlockBuilder().Create(_config.Reader);
            var generatorBlock = new GeneratorBlockBuilder().Create(_config.Generator);
            var writerBlock = new WriterBlockBuilder().Create(_config.Writer);
            
            var linkOptions = new DataflowLinkOptions { PropagateCompletion = true };

            (readerBlock as ISourceBlock<string>).LinkTo(generatorBlock as ITargetBlock<string>, linkOptions);
            
            (generatorBlock as ISourceBlock<GeneratedFileDescription>)
                .LinkTo(writerBlock as ITargetBlock<GeneratedFileDescription>, linkOptions);

            _firstBlock = readerBlock as ITargetBlock<Task<string>>;
            _lastBlock = writerBlock;
        }
    }
}