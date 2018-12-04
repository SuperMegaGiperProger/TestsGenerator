using System.Threading.Tasks.Dataflow;
using TestsGenerator.Pipeline.BlocksConfig;

namespace TestsGenerator.Pipeline.BlockBuilders
{
    public interface IBuilder
    {
        IDataflowBlock Create(BaseBlockConfig config);
    }
}