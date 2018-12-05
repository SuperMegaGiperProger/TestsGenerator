using System.IO;
using System.Text;
using System.Threading.Tasks;
using TestsGenerator;

namespace ConsoleApplication
{
    public class Writer : IWriter
    {
        private string _destinationFolderPath;

        public Writer(string destinationFolderPath)
        {
            _destinationFolderPath = destinationFolderPath;
        }

        public async Task WriteAsync(string filePath, string fileText)
        {
            string path = getFullPath(filePath);
            byte[] buffer = Encoding.UTF8.GetBytes(fileText);

            using (var writer = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                await writer.WriteAsync(buffer, 0, buffer.Length);
            }
        }

        private string getFullPath(string filePath)
        {
            return Path.Join(_destinationFolderPath, filePath);
        }
    }
}