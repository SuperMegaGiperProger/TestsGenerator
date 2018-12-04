using TestsGenerator.Generators;

namespace TestsGenerator.Pipeline.BlocksConfig
{
    public class GeneratorBlockConfig : BaseBlockConfig
    {
        public const int DEFAULT_GENERATORS_MAX_NUMBER = 10;

        public readonly ITestsCodeGenerator CodeGenerator;

        public GeneratorBlockConfig(
            ITestsCodeGenerator codeGenerator, int generatorsMaxNumber = DEFAULT_GENERATORS_MAX_NUMBER
        ) : base(generatorsMaxNumber)
        {
            CodeGenerator = codeGenerator;
        }
    }
}