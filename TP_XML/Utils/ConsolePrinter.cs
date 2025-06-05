using System;
using System.Collections.Generic;
using System.Reflection;
using TP_XML.Models;

namespace TP_XML.Utils
{
    public static class ConsolePrinter
    {
        //Metodo generico para la mayoría de entidades
        public static void ImportarYMostrar<T>(string nombreEntidad, string xmlPath)
        {
            try
            {
                var lista = Services.XmlImporter.ImportXml<T>(xmlPath);
                MostrarResultados(lista, nombreEntidad);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError al importar {nombreEntidad.ToLower()}:");
                Console.WriteLine(ex.Message);
            }
        }

        //Metodo especifico para localidades
        public static void ImportarYMostrarLocalidades(string nombreEntidad, string xmlPath)
        {
            try
            {
                var lista = Services.XmlImporter.ImportLocalidades(xmlPath);
                MostrarResultadosLocalidades(lista, nombreEntidad);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError al importar {nombreEntidad.ToLower()}:");
                Console.WriteLine(ex.Message);
            }
        }

        //Metodo privado para mostrar resultados genericos
        private static void MostrarResultados<T>(List<T> lista, string nombreEntidad)
        {
            Console.WriteLine($"\n=== {nombreEntidad} importadas ===");

            foreach (var item in lista)
            {
                var idProp = typeof(T).GetProperty("Id");
                var nombreProp = typeof(T).GetProperty("Nombre");

                var id = idProp?.GetValue(item);
                var nombre = nombreProp?.GetValue(item);

                Console.WriteLine($"Id: {id}, Nombre: {nombre}");
            }

            Console.WriteLine($"\nTotal: {lista.Count} {nombreEntidad.ToLower()} importadas correctamente.");
        }

        //Metodo privado especifico para mostrar localidades con mas detalle
        private static void MostrarResultadosLocalidades(List<Localidad> lista, string nombreEntidad)
        {
            Console.WriteLine($"\n=== {nombreEntidad} importadas ===");

            foreach (var localidad in lista)
            {
                Console.WriteLine($"Id: {localidad.Id}, Ciudad: {localidad.Ciudad}, Provincia: {localidad.Provincia}, País: {localidad.Pais}");
            }

            Console.WriteLine($"\nTotal: {lista.Count} {nombreEntidad.ToLower()} importadas correctamente.");
        }
    }
}