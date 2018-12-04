namespace TestsGenerator.Generators
{
    public interface ITestsCodeGenerator
    {
        GeneratedFileDescription[] GenerateTestFiles(string sourceCodeText);
    }
}