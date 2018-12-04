using System.Net.NetworkInformation;
using TestsGenerator.Generators;
using TestsGenerator.Pipeline.BlocksConfig;

namespace TestsGenerator.Pipeline
{
    public class Config
    {
        public ReaderBlockConfig Reader;
        public GeneratorBlockConfig Generator;
        public WriterBlockConfig Writer;

        public Config(
            ReaderBlockConfig readerBlockConfig,
            GeneratorBlockConfig generatorBlockConfig,
            WriterBlockConfig writerBlockConfig
        )
        {
            Reader = readerBlockConfig;
            Generator = generatorBlockConfig;
            Writer = writerBlockConfig;
        }

        public Config(ITestsCodeGenerator generator, IWriter writer,
            int readersMaxNumber, int generatorsMaxNumber, int writersMaxNumber)
        {
            Reader = new ReaderBlockConfig(readersMaxNumber);
            Generator = new GeneratorBlockConfig(generator, generatorsMaxNumber);
            Writer = new WriterBlockConfig(writer, writersMaxNumber);
        }
    }
}