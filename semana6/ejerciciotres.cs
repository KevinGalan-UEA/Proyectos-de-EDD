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

    // Método para agregar elementos al final
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

    // EJERCICIO 3: Búsqueda de un dato en la lista
    public int BuscarDato(int datoABuscar)
    {
        int contador = 0;
        Nodo actual = cabeza;
        
        // Recorremos toda la lista
        while (actual != null)
        {
            // Si encontramos el dato, incrementamos el contador
            if (actual.Dato == datoABuscar)
            {
                contador++;
            }
            actual = actual.Siguiente;
        }
        
        // Mostramos el resultado según lo encontrado
        if (contador == 0)
        {
            Console.WriteLine($"El dato {datoABuscar} no fue encontrado en la lista.");
        }
        else
        {
            Console.WriteLine($"El dato {datoABuscar} aparece {contador} vez/veces en la lista.");
        }
        
        return contador;  // Retornamos el número de ocurrencias
    }

    // Método para mostrar la lista
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
        
        // Agregamos elementos con algunos repetidos para probar
        lista.AgregarAlFinal(10);
        lista.AgregarAlFinal(20);
        lista.AgregarAlFinal(30);
        lista.AgregarAlFinal(20);
        lista.AgregarAlFinal(40);
        lista.AgregarAlFinal(20);
        
        Console.WriteLine("Lista enlazada:");
        lista.MostrarLista();
        
        Console.WriteLine("\n--- Búsqueda de datos ---");
        
        // Buscamos datos existentes
        int resultado1 = lista.BuscarDato(20);
        int resultado2 = lista.BuscarDato(30);
        
        // Buscamos un dato no existente
        int resultado3 = lista.BuscarDato(99);
        
        Console.WriteLine($"\nResumen de búsquedas:");
        Console.WriteLine($"Dato 20 encontrado {resultado1} veces");
        Console.WriteLine($"Dato 30 encontrado {resultado2} veces");
        Console.WriteLine($"Dato 99 encontrado {resultado3} veces");
    }
}