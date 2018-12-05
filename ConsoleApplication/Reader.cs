using System.IO;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    public class Reader
    {
        public async Task<string> ReadAsync(string path)
        {
            using (var reader = new StreamReader(path))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}