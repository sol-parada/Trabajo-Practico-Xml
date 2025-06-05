using System.IO;
using System.Text.RegularExpressions;

namespace TP_XML.Utils
{
    public static class XmlCleaner
    {
        //Limpieza general de un archivo XML
        public static void LimpiarArchivoXml(string rutaArchivo)
        {
            if (!File.Exists(rutaArchivo))
                return;

            string xml = File.ReadAllText(rutaArchivo);

            //Limpia etiquetas <ano/> y <ano></ano> por <ano>0</ano>
            xml = Regex.Replace(xml, @"<ano\s*/>", "<ano>0</ano>");
            xml = Regex.Replace(xml, @"<ano>\s*</ano>", "<ano>0</ano>");

            

            File.WriteAllText(rutaArchivo, xml);
        }

        //Limpieza específica solo de etiquetas <ano/>
        public static void LimpiarAno(string rutaArchivo)
        {
            if (!File.Exists(rutaArchivo))
                return;

            string xml = File.ReadAllText(rutaArchivo);

            
            xml = Regex.Replace(xml, @"<ano\s*/>", "<ano>0</ano>");
            xml = Regex.Replace(xml, @"<ano>\s*</ano>", "<ano>0</ano>");

            File.WriteAllText(rutaArchivo, xml);
        }
    }
}