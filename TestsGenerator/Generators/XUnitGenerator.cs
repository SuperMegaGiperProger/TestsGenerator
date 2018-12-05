namespace TestsGenerator.Generators
{
    public class XUnitGenerator : ITestsCodeGenerator
    {
        public GeneratedFileDescription[] GenerateTestFiles(string sourceCodeText)
        {
            return new Services.XUnit(sourceCodeText).GenerateTestFiles();
        }
    }
}