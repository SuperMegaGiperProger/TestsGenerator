using System.Threading.Tasks;

namespace TestsGenerator
{
    public interface IWriter
    {
        Task WriteAsync(string fileName, string fileText);
    }
}