using System.IO;
using System.Text;
using TP_XML.Helpers;
using Xunit;

public class EncodingHelperTests
{
    [Fact]
    public void ConvertFileToUtf8_CreatesUtf8File_WithCorrectContent()
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        var tempInput = Path.GetTempFileName();
        var tempOutput = Path.GetTempFileName();
        var textoOriginal = "áéíóú ñ ¿¡";
        File.WriteAllBytes(tempInput, Encoding.GetEncoding("Windows-1252").GetBytes(textoOriginal));

        
        EncodingHelper.ConvertFileToUtf8(tempInput, tempOutput);

        
        var contenidoUtf8 = File.ReadAllText(tempOutput, Encoding.UTF8);
        Assert.Equal(textoOriginal, contenidoUtf8);

        File.Delete(tempInput);
        File.Delete(tempOutput);
    }
}