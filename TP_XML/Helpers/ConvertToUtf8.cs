using System.IO;
using TP_XML.Helpers;

class ConvertToUtf8
{
    static void Main()
    {
        string carpeta = "Archivos";
        var archivos = Directory.GetFiles(carpeta, "*.xml");
        foreach (var archivo in archivos)
        {
            string tempFile = archivo + ".utf8";
            EncodingHelper.ConvertFileToUtf8(archivo, tempFile);
            File.Copy(tempFile, archivo, true); //Sobrescribe el original
            File.Delete(tempFile);
            System.Console.WriteLine($"Convertido a UTF-8: {archivo}");
        }
        System.Console.WriteLine("Conversión a UTF-8 finalizada.");
    }
}