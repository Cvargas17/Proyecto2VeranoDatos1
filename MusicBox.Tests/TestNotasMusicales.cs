using Xunit;
using Music_box;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics.Contracts;
using Microsoft.VisualBasic;
using System.Reflection;
using System.Security.Cryptography;




//prueba las  frecuencias de todas las notas, si una esta mal todo falla
public class NotamusicalTests
{
    [Fact]
    public void FrecuenciaCorrecta()
{
    //DO
    var doNota = new Notamusical(Nota.DO, Figura.NEGRA, 1.0);
    Assert.Equal(261.63, doNota.frecuencia, 2);

    //RE
    var reNota = new Notamusical(Nota.RE, Figura.NEGRA, 1.0);
    Assert.Equal(293.66, reNota.frecuencia, 2);

    //MI
    var miNota = new Notamusical(Nota.MI, Figura.NEGRA, 1.0);
    Assert.Equal(329.63, miNota.frecuencia, 2);

    //FA
    var faNota = new Notamusical(Nota.FA, Figura.NEGRA, 1.0);
    Assert.Equal(349.23, faNota.frecuencia, 2);

    //SOL
    var solNota = new Notamusical(Nota.SOL, Figura.NEGRA, 1.0);
    Assert.Equal(392.00, solNota.frecuencia, 2);

    //LA
    var laNota = new Notamusical(Nota.LA, Figura.NEGRA, 1.0);
    Assert.Equal(440.00, laNota.frecuencia, 2);

    //SI
    var siNota = new Notamusical(Nota.SI, Figura.NEGRA, 1.0);
    Assert.Equal(493.88, siNota.frecuencia, 2);
}
}


//prueba el tiempo de todas las figuras, si una esta mal todo falla
public class TestFiguras
{
    [Fact]
    public void TiempoCorrecto()
    {
        double negra = 1.0;  //se establece el tiempo de la negra

        //REDONDA
        var redonda = new Notamusical(Nota.DO, Figura.REDONDA, negra);
        Assert.Equal(4.0, redonda.duracion, 2);

        //BLANCA
        var blanca = new Notamusical(Nota.DO, Figura.BLANCA, negra);
        Assert.Equal(2.0, blanca.duracion, 2);

        //NEGRA
        var negraNota = new Notamusical(Nota.DO, Figura.NEGRA, negra);
        Assert.Equal(1.0, negraNota.duracion, 2);

        //CORCHEA
        var corchea = new Notamusical(Nota.DO, Figura.CORCHEA, negra);
        Assert.Equal(0.5, corchea.duracion, 2);

        //SEMICORCHEA
        var semicorchea = new Notamusical(Nota.DO, Figura.SEMICORCHEA, negra);
        Assert.Equal(0.25, semicorchea.duracion, 2);
    }
}