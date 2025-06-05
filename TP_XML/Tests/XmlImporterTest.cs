using Xunit;
using System.Collections.Generic;
using System.IO;
using TP_XML.Services;
using TP_XML.Models;

public class XmlImporterTest
{
    private string CreateTempXmlFile(string content)
    {
        var tempFile = Path.GetTempFileName();
        File.WriteAllText(tempFile, content, System.Text.Encoding.GetEncoding("Windows-1252"));
        return tempFile;
    }

    [Fact]
    public void ImportXml_ReturnsListOfItems_WhenXmlIsValid()
    {
        
        string xml = @"<?xml version=""1.0"" encoding=""Windows-1252"" standalone=""yes""?>
<VFPData>
    <_expxml>
        <especialidad>1</especialidad>
        <plan>71</plan>
        <orientacion>1</orientacion>
        <nombre>Test Orientacion</nombre>
    </_expxml>
    <_expxml>
        <especialidad>2</especialidad>
        <plan>72</plan>
        <orientacion>2</orientacion>
        <nombre>Otra Orientacion</nombre>
    </_expxml>
</VFPData>";
        string filePath = CreateTempXmlFile(xml);

        
        var result = XmlImporter.ImportXml<Orientacion>(filePath);

        
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal("Test Orientacion", result[0].Nombre);
        Assert.Equal(1, result[0].Especialidad);
        Assert.Equal(71, result[0].Plan);
        Assert.Equal(1, result[0].Id);
        Assert.Equal("Otra Orientacion", result[1].Nombre);

        File.Delete(filePath);
    }

    [Fact]
    public void ImportXml_ReturnsEmptyList_WhenXmlIsEmpty()
    {
        
        string xml = @"<?xml version=""1.0"" encoding=""Windows-1252"" standalone=""yes""?><VFPData></VFPData>";
        string filePath = CreateTempXmlFile(xml);

        
        var result = XmlImporter.ImportXml<Orientacion>(filePath);

        
        Assert.NotNull(result);
        Assert.Empty(result);

        File.Delete(filePath);
    }

    [Fact]
    public void ImportXml_ReturnsEmptyList_WhenFileDoesNotExist()
    {
        
        string filePath = Path.Combine(Path.GetTempPath(), "nonexistent.xml");

        
        var result = XmlImporter.ImportXml<Orientacion>(filePath);

        
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public void ImportXml_ReturnsEmptyList_WhenXmlIsMalformed()
    {
        
        string xml = @"<VFPData><_expxml><especialidad>1</especialidad>"; 
        string filePath = CreateTempXmlFile(xml);

        
        var result = XmlImporter.ImportXml<Orientacion>(filePath);

        
        Assert.NotNull(result);
        Assert.Empty(result);

        File.Delete(filePath);
    }
}