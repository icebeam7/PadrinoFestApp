using System.Text;

namespace PadrinoFestApp.Helpers
{
    public class FileAccessHelper
    {
        public static string GetLocalFilePath(string filename) =>
            Path.Combine(FileSystem.AppDataDirectory, filename);

        public static async Task<List<string>> ReadFile(string filename)
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync(filename);
            using var reader = new StreamReader(stream, Encoding.UTF8);

            string line;
            var lines = new List<string>();
            while ((line = reader.ReadLine()) != null)
                lines.Add(line);
            return lines;
        }
    }
}
