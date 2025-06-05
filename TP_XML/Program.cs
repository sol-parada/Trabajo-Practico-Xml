using System.Text;
using TP_XML.Models;
using TP_XML.Utils;
using TP_XML.Services;
using TP_XML.Helpers;
using System.IO;

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("Iniciando importación de datos SYSACAD\n");


string[] archivos = {
    "Archivos/universidad.xml",
    "Archivos/grados.xml",
    "Archivos/facultades.xml",
    "Archivos/materias.xml",
    "Archivos/especialidades.xml",
    "Archivos/orientaciones.xml",
    "Archivos/planes.xml",
    "Archivos/paises.xml",
    "Archivos/localidades.xml"
};

//Convertir todos los XML a UTF-8
foreach (var archivo in archivos)
{
    string tempFile = archivo + ".utf8";
    EncodingHelper.ConvertFileToUtf8(archivo, tempFile);
    File.Copy(tempFile, archivo, true); // Sobrescribe el original
    File.Delete(tempFile);
}


XmlChange.CorregirValorNoNumericoEnXml("Archivos/materias.xml", "ano", "2024");

//Validar y limpiar todos los XML antes de importar
if (!XmlFileChecker.ValidarYListarArchivos(archivos))
    return;


ConsolePrinter.ImportarYMostrar<Universidad>("Universidades", "Archivos/universidad.xml");
ConsolePrinter.ImportarYMostrar<Grado>("Grados", "Archivos/grados.xml");
ConsolePrinter.ImportarYMostrar<Facultad>("Facultades", "Archivos/facultades.xml");
ConsolePrinter.ImportarYMostrar<Materia>("Materias", "Archivos/materias.xml");
ConsolePrinter.ImportarYMostrar<Especialidad>("Especialidades", "Archivos/especialidades.xml");
ConsolePrinter.ImportarYMostrar<Orientacion>("Orientaciones", "Archivos/orientaciones.xml");
ConsolePrinter.ImportarYMostrar<Plan>("Planes", "Archivos/planes.xml");
ConsolePrinter.ImportarYMostrar<Pais>("Países", "Archivos/paises.xml");


ConsolePrinter.ImportarYMostrarLocalidades("Localidades", "Archivos/localidades.xml");

Console.WriteLine("\nProceso de importación completado.");
Console.WriteLine("Presiona cualquier tecla para continuar...");
Console.ReadKey(true);

int errores = DatabaseSeeder.ImportarTodo();
if (errores == 0)
    Console.WriteLine("Datos importados a la base de datos correctamente.");
else
    Console.WriteLine($"Importación finalizada con {errores} errores. Revisa la consola para más detalles.");