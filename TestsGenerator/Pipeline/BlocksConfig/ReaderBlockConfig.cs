namespace TestsGenerator.Pipeline.BlocksConfig
{
    public class ReaderBlockConfig : BaseBlockConfig
    {
        public const int DEFAULT_READERS_MAX_NUMBER = 10;
        
        public ReaderBlockConfig(int readersMaxNumber = DEFAULT_READERS_MAX_NUMBER) : base(readersMaxNumber) { }
    }
}