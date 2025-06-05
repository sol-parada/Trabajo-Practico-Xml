using System;
using System.Xml;

namespace TP_XML.Utils
{
    public static class XmlValidator
    {
        public static bool EsXmlValido(string rutaXml)
        {
            try
            {
                var doc = new XmlDocument();
                doc.Load(rutaXml);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"El archivo {rutaXml} NO es un XML válido: {ex.Message}");
                return false;
            }
        }
    }
}