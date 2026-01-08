using System;
public enum Nota
{
    DO, RE, MI, FA, SOL, LA, SI
}
public enum Figura
{
    REDONDA, BLANCA, NEGRA, CORCHEA, SEMICORCHEA
}
class Notamusical
{
    public Nota nombre;
    public Figura figura;
    public double duracion; //En segundos
    public double frecuencia; //En Hz

    public Notamusical(Nota nombre, Figura figura, double duracionNegra)
    {
        this.nombre = nombre;
        this.figura = figura;
        this.frecuencia = CalcularFrecuencia(nombre);
        this.duracion = CalcularDuracion(figura, duracionNegra);
    }
    public double CalcularFrecuencia(Nota nota)
    {
        switch (nota)
        {
            case Nota.DO: return 261.63;
            case Nota.RE: return 293.66;
            case Nota.MI: return 329.63;
            case Nota.FA: return 349.23;
            case Nota.SOL: return 392.00;
            case Nota.LA: return 440.00;
            case Nota.SI: return 493.88;
            default: return 0;
        }
    }
    private double CalcularDuracion(Figura figura, double negra)
    {
        switch (figura)
        {
            case Figura.REDONDA: return negra * 4;
            case Figura.BLANCA: return negra * 2;
            case Figura.NEGRA: return negra;
            case Figura.CORCHEA: return negra / 2;
            case Figura.SEMICORCHEA: return negra / 4;
            default: return negra;
        }
    }

    public override string ToString()
    {
        return $"{nombre} - {figura} - {duracion:0.###} s - {frecuencia:0.##} Hz";
    }
}

using System.Windows.Forms;

[STAThread]
Application.EnableVisualStyles();
Application.SetCompatibleTextRenderingDefault(false);
Application.Run(new Music_box.MainForm());
