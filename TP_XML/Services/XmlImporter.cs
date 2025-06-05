using System.Text;
using System.Xml.Serialization;
using TP_XML.Models;

namespace TP_XML.Services
{
    public static class XmlImporter
    {
        public static List<T> ImportXml<T>(string filePath)
        {
            try
            {
                return DeserializeXml<VfpDataWrapper<T>>(filePath)?.Items ?? new List<T>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió un error al importar el XML {filePath}:\n{ex.Message}");
                return new List<T>();
            }
        }

        public static List<Localidad> ImportLocalidades(string filePath)
        {
            try
            {
                return DeserializeXml<VfpDataLocalidad>(filePath)?.Items ?? new List<Localidad>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió un error al importar localidades:\n{ex.Message}");
                return new List<Localidad>();
            }
        }

        //Deserializacion
        private static T? DeserializeXml<T>(string filePath) where T : class
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using var reader = new StreamReader(filePath, Encoding.UTF8);
            var serializer = new XmlSerializer(typeof(T));
            return serializer.Deserialize(reader) as T;
        }
    }
}