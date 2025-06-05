using System.Text;
using System.IO;

namespace TP_XML.Helpers
{
    public static class EncodingHelper
    {
        public static void ConvertFileToUtf8(string inputPath, string outputPath)
        {
            var windows1252 = Encoding.GetEncoding("Windows-1252");
            var bytes = File.ReadAllBytes(inputPath);
            var text = windows1252.GetString(bytes);

            //Reemplaza el encoding en la cabecera XML
            text = System.Text.RegularExpressions.Regex.Replace(
                text,
                @"<\?xml\s+version\s*=\s*[""']1\.0[""']\s+encoding\s*=\s*[""'][^""']*[""'](\s+standalone\s*=\s*[""'][^""']*[""'])?\s*\?>",
                @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes""?>",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase
            );

            File.WriteAllText(outputPath, text, Encoding.UTF8);
        }
    }
}