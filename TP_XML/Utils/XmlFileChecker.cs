using System;
using System.Collections.Generic;

namespace TP_XML.Utils
{
    public static class XmlFileChecker
    {
        public static bool ValidarYListarArchivos(string[] archivos)
        {
            var archivosInvalidos = new List<string>();
            foreach (var archivo in archivos)
            {
                XmlCleaner.LimpiarArchivoXml(archivo);

                if (!XmlValidator.EsXmlValido(archivo))
                {
                    Console.WriteLine($"\nEl archivo {archivo} tiene un error de estructura (por ejemplo, etiqueta mal cerrada).");
                    archivosInvalidos.Add(archivo);
                }
            }

            if (archivosInvalidos.Count > 0)
            {
                Console.WriteLine("\nArchivos inválidos detectados:");
                foreach (var archivo in archivosInvalidos)
                    Console.WriteLine($" - {archivo}");
                Console.WriteLine("\nCorrige los archivos XML inválidos antes de continuar.");
                Console.WriteLine("Presiona cualquier tecla para salir...");
                Console.ReadKey();
                return false;
            }
            return true;
        }
    }
}