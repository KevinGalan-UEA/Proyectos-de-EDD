using System;
using System.Collections.Generic;
using System.Linq;

namespace EjerciciosPOO
{
    public class AnalizadorPrecios
    {
        private List<decimal> precios;

        public AnalizadorPrecios()
        {
            precios = new List<decimal>();
        }

        public void CargarPrecios()
        {
            precios.AddRange(new decimal[] { 
                50, 75, 46, 22, 80, 65, 8 
            });
        }

        public void MostrarExtremos()
        {
            decimal menor = precios.Min();
            decimal mayor = precios.Max();
            
            Console.WriteLine($"Precio menor: {menor}");
            Console.WriteLine($"Precio mayor: {mayor}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n=== Ejercicio 10 ===");
            var analizador = new AnalizadorPrecios();
            analizador.CargarPrecios();
            analizador.MostrarExtremos();
        }
    }
}