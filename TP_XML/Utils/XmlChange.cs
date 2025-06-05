using System.IO;
using System.Text.RegularExpressions;

namespace TP_XML.Utils
{
    public static class XmlChange
    {

        public static void CorregirValorNoNumericoEnXml(string rutaArchivo, string nombreEtiqueta, string valorReemplazo)
        {
            string xml = File.ReadAllText(rutaArchivo);

            string patron = $@"<{nombreEtiqueta}>[^0-9]+</{nombreEtiqueta}>";
            string reemplazo = $"<{nombreEtiqueta}>{valorReemplazo}</{nombreEtiqueta}>";

            string corregido = Regex.Replace(xml, patron, reemplazo);

            File.WriteAllText(rutaArchivo, corregido);
        }

        
        public static void CorregirValorNoNumericoEnTodosLosXml(string rutaCarpeta, string nombreEtiqueta, string valorReemplazo)
        {
            var archivos = Directory.GetFiles(rutaCarpeta, "*.xml");
            foreach (var archivo in archivos)
            {
                CorregirValorNoNumericoEnXml(archivo, nombreEtiqueta, valorReemplazo);
            }
        }
    }
}