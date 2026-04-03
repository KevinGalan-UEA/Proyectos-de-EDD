using System;
using System.Collections.Generic;

class Nodo
{
    public string Valor;
    public List<Nodo> Hijos;

    public Nodo(string valor)
    {
        Valor = valor;
        Hijos = new List<Nodo>();
    }

    public void AgregarHijo(Nodo hijo)
    {
        Hijos.Add(hijo);
    }
}

class Program
{
    static void MostrarArbol(Nodo nodo, int nivel)
    {
        Console.WriteLine(new string(' ', nivel * 2) + "- " + nodo.Valor);

        foreach (Nodo hijo in nodo.Hijos)
        {
            MostrarArbol(hijo, nivel + 1);
        }
    }

    static void Main(string[] args)
    {
        // EJEMPLO 1: Árbol genealógico
        Nodo abuelo = new Nodo("Abuelo");
        Nodo padre = new Nodo("Padre");
        Nodo tio = new Nodo("Tío");
        Nodo hijo1 = new Nodo("Hijo 1");
        Nodo hijo2 = new Nodo("Hijo 2");
        Nodo primo = new Nodo("Primo");

        abuelo.AgregarHijo(padre);
        abuelo.AgregarHijo(tio);
        padre.AgregarHijo(hijo1);
        padre.AgregarHijo(hijo2);
        tio.AgregarHijo(primo);

        Console.WriteLine("Árbol genealógico");
        MostrarArbol(abuelo, 0);

        Console.WriteLine();

        // EJEMPLO 2: Árbol de carpetas
        Nodo pc = new Nodo("Disco C");
        Nodo documentos = new Nodo("Documentos");
        Nodo imagenes = new Nodo("Imágenes");
        Nodo tareas = new Nodo("Tareas");
        Nodo fotos = new Nodo("Fotos");

        pc.AgregarHijo(documentos);
        pc.AgregarHijo(imagenes);
        documentos.AgregarHijo(tareas);
        imagenes.AgregarHijo(fotos);

        Console.WriteLine("Árbol de carpetas");
        MostrarArbol(pc, 0);
    }
}