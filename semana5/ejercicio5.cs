using System;
using System.Collections.Generic;
using System.Linq;

namespace EjerciciosPOO
{
    public class NumerosInversos
    {
        private List<int> numeros;

        public NumerosInversos()
        {
            numeros = new List<int>();
        }

        public void GenerarNumeros()
        {
            for (int i = 1; i <= 10; i++)
            {
                numeros.Add(i);
            }
        }

        public void MostrarInverso()
        {
            var inverso = numeros.OrderByDescending(n => n).ToList();
            Console.WriteLine(string.Join(", ", inverso));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n=== Ejercicio 5 ===");
            var numeros = new NumerosInversos();
            numeros.GenerarNumeros();
            Console.WriteLine("Números en orden inverso:");
            numeros.MostrarInverso();
        }
    }
}