using System;
using System.Collections.Generic;

namespace EjerciciosPOO
{
    public class GestorAsignaturas
    {
        private List<string> asignaturas;

        public GestorAsignaturas()
        {
            asignaturas = new List<string>();
        }

        public void CargarAsignaturas()
        {
            asignaturas.AddRange(new string[] { 
                "Matemáticas", 
                "Física", 
                "Química", 
                "Historia", 
                "Lengua" 
            });
        }

        public void MostrarAsignaturas()
        {
            Console.WriteLine("Asignaturas del curso:");
            foreach (var asignatura in asignaturas)
            {
                Console.WriteLine($"- {asignatura}");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Ejercicio 1 ===");
            var gestor1 = new GestorAsignaturas();
            gestor1.CargarAsignaturas();
            gestor1.MostrarAsignaturas();
        }
    }
}