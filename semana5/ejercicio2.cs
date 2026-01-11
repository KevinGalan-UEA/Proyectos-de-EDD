using System;
using System.Collections.Generic;

namespace EjerciciosPOO
{
    public class Estudiante
    {
        private List<string> asignaturas;

        public Estudiante()
        {
            asignaturas = new List<string>();
        }

        public void InscribirAsignaturas()
        {
            asignaturas.AddRange(new string[] { 
                "Matemáticas", 
                "Física", 
                "Química", 
                "Historia", 
                "Lengua" 
            });
        }

        public void MostrarEstudio()
        {
            foreach (var asignatura in asignaturas)
            {
                Console.WriteLine($"Yo estudio {asignatura}");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n=== Ejercicio 2 ===");
            var estudiante = new Estudiante();
            estudiante.InscribirAsignaturas();
            estudiante.MostrarEstudio();
        }
    }
}