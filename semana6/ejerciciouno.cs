using System;

public class Nodo
{
    public int Dato;
    public Nodo Siguiente;

    public Nodo(int dato)
    {
        Dato = dato;
        Siguiente = null;
    }
}

public class ListaEnlazada
{
    private Nodo cabeza;

    public ListaEnlazada()
    {
        cabeza = null;
    }

    // Método para agregar elementos al final (para probar)
    public void AgregarAlFinal(int dato)
    {
        Nodo nuevoNodo = new Nodo(dato);
        
        if (cabeza == null)
        {
            cabeza = nuevoNodo;
        }
        else
        {
            Nodo actual = cabeza;
            while (actual.Siguiente != null)
            {
                actual = actual.Siguiente;
            }
            actual.Siguiente = nuevoNodo;
        }
    }

    // EJERCICIO 1: Contar elementos de la lista
    public int ContarElementos()
    {
        int contador = 0;
        Nodo actual = cabeza;  // Comenzamos desde el primer nodo
        
        while (actual != null)
        {
            contador++;  // Incrementamos el contador por cada nodo
            actual = actual.Siguiente;  // Avanzamos al siguiente nodo
        }
        
        return contador;
    }

    // Método para mostrar la lista (opcional, para pruebas)
    public void MostrarLista()
    {
        Nodo actual = cabeza;
        while (actual != null)
        {
            Console.Write(actual.Dato + " -> ");
            actual = actual.Siguiente;
        }
        Console.WriteLine("null");
    }
}

class Program
{
    static void Main(string[] args)
    {
        ListaEnlazada lista = new ListaEnlazada();
        
        // Agregamos algunos elementos de prueba
        lista.AgregarAlFinal(10);
        lista.AgregarAlFinal(20);
        lista.AgregarAlFinal(30);
        lista.AgregarAlFinal(40);
        
        Console.WriteLine("Lista enlazada:");
        lista.MostrarLista();
        
        int cantidad = lista.ContarElementos();
        Console.WriteLine($"\nLa lista tiene {cantidad} elementos.");
    }
}