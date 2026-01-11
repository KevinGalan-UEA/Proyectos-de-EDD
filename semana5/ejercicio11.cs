using System;
using System.Collections.Generic;

namespace EjerciciosPOO
{
    public class Vector3D
    {
        public List<int> Componentes { get; private set; }

        public Vector3D(int x, int y, int z)
        {
            Componentes = new List<int> { x, y, z };
        }

        public static int CalcularProductoEscalar(Vector3D v1, Vector3D v2)
        {
            if (v1.Componentes.Count != v2.Componentes.Count)
                throw new ArgumentException("Los vectores deben tener la misma dimensión");

            int producto = 0;
            for (int i = 0; i < v1.Componentes.Count; i++)
            {
                producto += v1.Componentes[i] * v2.Componentes[i];
            }
            return producto;
        }

        public override string ToString()
        {
            return $"({string.Join(", ", Componentes)})";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n=== Ejercicio 11 ===");
            
            var vector1 = new Vector3D(1, 2, 3);
            var vector2 = new Vector3D(-1, 0, 2);
            
            Console.WriteLine($"Vector 1: {vector1}");
            Console.WriteLine($"Vector 2: {vector2}");
            
            int producto = Vector3D.CalcularProductoEscalar(vector1, vector2);
            Console.WriteLine($"Producto escalar: {producto}");
        }
    }
}