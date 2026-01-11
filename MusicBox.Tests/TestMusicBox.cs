using Xunit;
using Music_box;
using System.Collections.Generic;

namespace Music_box.Tests
{
    public class NotamusicalTests
    {
        // Test 1: Frecuencias correctas de todas las notas
        [Fact]
        public void FrecuenciaCorrecta_DeTodasLasNotas()
        {
            Assert.Equal(261.63, new Notamusical(Nota.DO, Figura.NEGRA, 1.0).frecuencia, 2);
            Assert.Equal(293.66, new Notamusical(Nota.RE, Figura.NEGRA, 1.0).frecuencia, 2);
            Assert.Equal(329.63, new Notamusical(Nota.MI, Figura.NEGRA, 1.0).frecuencia, 2);
            Assert.Equal(349.23, new Notamusical(Nota.FA, Figura.NEGRA, 1.0).frecuencia, 2);
            Assert.Equal(392.00, new Notamusical(Nota.SOL, Figura.NEGRA, 1.0).frecuencia, 2);
            Assert.Equal(440.00, new Notamusical(Nota.LA, Figura.NEGRA, 1.0).frecuencia, 2);
            Assert.Equal(493.88, new Notamusical(Nota.SI, Figura.NEGRA, 1.0).frecuencia, 2);
        }

        // Test 2: Duración correcta de todas las figuras
        [Fact]
        public void DuracionCorrecta_DeTodasLasFiguras()
        {
            double negra = 1.0;

            Assert.Equal(4.0, new Notamusical(Nota.DO, Figura.REDONDA, negra).duracion, 2);
            Assert.Equal(2.0, new Notamusical(Nota.DO, Figura.BLANCA, negra).duracion, 2);
            Assert.Equal(1.0, new Notamusical(Nota.DO, Figura.NEGRA, negra).duracion, 2);
            Assert.Equal(0.5, new Notamusical(Nota.DO, Figura.CORCHEA, negra).duracion, 2);
            Assert.Equal(0.25, new Notamusical(Nota.DO, Figura.SEMICORCHEA, negra).duracion, 2);
        }
    }

    public class ListaDobleEnlazadaTests
    {
        // Test 3: La lista inicia vacía
        [Fact]
        public void Lista_IniciaVacia()
        {
            var lista = new ListaDobleEnlazada<int>();
            Assert.True(lista.EstaVacia());
        }

        // Test 4: ObtenerCantidad inicia en cero
        [Fact]
        public void ObtenerCantidad_IniciaEnCero()
        {
            var lista = new ListaDobleEnlazada<int>();
            Assert.Equal(0, lista.ObtenerCantidad());
        }

        // Test 5: Agregar un elemento aumenta la cantidad
        [Fact]
        public void Agregar_Elemento_AumentaCantidad()
        {
            var lista = new ListaDobleEnlazada<int>();
            lista.Agregar(10);
            Assert.Equal(1, lista.ObtenerCantidad());
        }

        // Test 6: Enumerador devuelve elementos en orden
        [Fact]
        public void Enumerador_DevuelveElementosEnOrden()
        {
            var lista = new ListaDobleEnlazada<int>();
            lista.Agregar(1);
            lista.Agregar(2);
            lista.Agregar(3);

            var resultado = new List<int>(lista.ObtenerEnumerador());

            Assert.Equal(new List<int> { 1, 2, 3 }, resultado);
        }
    }

    public class NotamusicalExtraTests
    {
        // Test 7: La frecuencia nunca es cero
        [Fact]
        public void Frecuencia_EsMayorQueCero()
        {
            var nota = new Notamusical(Nota.DO, Figura.NEGRA, 1.0);
            Assert.True(nota.frecuencia > 0);
        }

        // Test 8: La duración siempre es positiva
        [Fact]
        public void Duracion_EsMayorQueCero()
        {
            var nota = new Notamusical(Nota.DO, Figura.CORCHEA, 1.0);
            Assert.True(nota.duracion > 0);
        }

        // Test 9: ToString no devuelve texto vacío
        [Fact]
        public void ToString_NoEstaVacio()
        {
            var nota = new Notamusical(Nota.DO, Figura.NEGRA, 1.0);
            Assert.False(string.IsNullOrWhiteSpace(nota.ToString()));
        }

        // Test 10: Enumerador inverso devuelve el orden correcto
        [Fact]
        public void EnumeradorInverso_DevuelveOrdenInverso()
        {
            var lista = new ListaDobleEnlazada<int>();
            lista.Agregar(1);
            lista.Agregar(2);
            lista.Agregar(3);

            var resultado = new List<int>(lista.ObtenerEnumeradorInverso());

            Assert.Equal(new List<int> { 3, 2, 1 }, resultado);
        }
    }
}
