namespace TestsGenerator.Pipeline.BlocksConfig
{
    public class WriterBlockConfig : BaseBlockConfig
    {
        public const int DEFAULT_WRITERS_MAX_NUMBER = 10;
        
        public readonly IWriter Writer;

        public WriterBlockConfig(
            IWriter writer, int writersMaxNumber = DEFAULT_WRITERS_MAX_NUMBER
        ) : base(writersMaxNumber)
        {
            Writer = writer;
        }
    }
}