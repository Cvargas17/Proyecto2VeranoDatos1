using System;
using System.Collections.Generic;

namespace Miusic_box
{
    public class Nodo<T>
{
    public T Datos { get; set; }
    public Nodo<T>? Siguiente { get; set; }
    public Nodo<T>? Anterior { get; set; }

    public Nodo(T datos)
    {
        Datos = datos;
        Siguiente = null;
        Anterior = null;
    }
}

public class ListaDobleEnlazada<T>
{
    private Nodo<T>? cabeza;
    private Nodo<T>? cola;
    private int cantidad;

    public ListaDobleEnlazada()
    {
        cabeza = null;
        cola = null;
        cantidad = 0;
    }

    // Agregar al final
    public void Agregar(T datos)
    {
        Nodo<T> nuevoNodo = new Nodo<T>(datos);

        if (cabeza == null)
        {
            cabeza = nuevoNodo;
            cola = nuevoNodo;
        }
        else
        {
            cola!.Siguiente = nuevoNodo;
            nuevoNodo.Anterior = cola;
            cola = nuevoNodo;
        }
        cantidad++;
    }

    // Eliminar en índice
    public void EliminarEnIndice(int indice)
    {
        if (indice < 0 || indice >= cantidad)
            throw new IndexOutOfRangeException("Índice fuera de rango");

        Nodo<T>? actual = cabeza;
        for (int i = 0; i < indice; i++)
        {
            actual = actual!.Siguiente;
        }

        if (actual!.Anterior != null)
            actual.Anterior.Siguiente = actual.Siguiente;
        else
            cabeza = actual.Siguiente;

        if (actual.Siguiente != null)
            actual.Siguiente.Anterior = actual.Anterior;
        else
            cola = actual.Anterior;

        cantidad--;
    }

    // Obtener cantidad de elementos
    public int ObtenerCantidad()
    {
        return cantidad;
    }

    // Verificar si está vacía
    public bool EstaVacia()
    {
        return cantidad == 0;
    }

    // Limpiar lista
    public void Limpiar()
    {
        cabeza = null;
        cola = null;
        cantidad = 0;
    }

    // Recorrer hacia adelante
    public IEnumerable<T> ObtenerEnumerador()
    {
        Nodo<T>? actual = cabeza;
        while (actual != null)
        {
            yield return actual.Datos;
            actual = actual.Siguiente;
        }
    }

    // Recorrer hacia atrás
    public IEnumerable<T> ObtenerEnumeradorInverso()
    {
        Nodo<T>? actual = cola;
        while (actual != null)
        {
            yield return actual.Datos;
            actual = actual.Anterior;
        }
    }
}