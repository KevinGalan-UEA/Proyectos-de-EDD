using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TraductorBasico
{
    class Program
    {
        // Diccionarios para ambas direcciones
        static Dictionary<string, string> inglesEspanol = new Dictionary<string, string>();
        static Dictionary<string, string> espanolIngles = new Dictionary<string, string>();

        static void Main(string[] args)
        {
            InicializarDiccionario();
            int opcion;

            do
            {
                MostrarMenu();
                string entrada = Console.ReadLine();
                if (!int.TryParse(entrada, out opcion))
                {
                    Console.WriteLine("Opción no válida. Intente de nuevo.\n");
                    continue;
                }

                switch (opcion)
                {
                    case 1:
                        TraducirFrase();
                        break;
                    case 2:
                        AgregarPalabra();
                        break;
                    case 0:
                        Console.WriteLine("¡Hasta luego!");
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Intente de nuevo.\n");
                        break;
                }
            } while (opcion != 0);
        }

        static void MostrarMenu()
        {
            Console.WriteLine("==================== MENÚ ====================");
            Console.WriteLine("1. Traducir una frase");
            Console.WriteLine("2. Agregar palabras al diccionario");
            Console.WriteLine("0. Salir");
            Console.Write("Seleccione una opción: ");
        }

        static void InicializarDiccionario()
        {
            // Lista base de al menos 10 palabras (inglés -> español)
            var palabrasBase = new List<(string ingles, string espanol)>
            {
                ("time", "tiempo"),
                ("person", "persona"),
                ("year", "año"),
                ("day", "día"),
                ("thing", "cosa"),
                ("man", "hombre"),
                ("world", "mundo"),
                ("life", "vida"),
                ("hand", "mano"),
                ("eye", "ojo")
            };

            foreach (var (ingles, espanol) in palabrasBase)
            {
                // Almacenar en minúsculas para búsqueda case-insensitive
                inglesEspanol[ingles.ToLower()] = espanol.ToLower();
                espanolIngles[espanol.ToLower()] = ingles.ToLower();
            }
        }

        static void TraducirFrase()
        {
            Console.Write("Ingrese la frase a traducir: ");
            string frase = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(frase))
            {
                Console.WriteLine("No ingresó ninguna frase.\n");
                return;
            }

            Console.WriteLine("¿Dirección de traducción?");
            Console.WriteLine("1. Inglés -> Español");
            Console.WriteLine("2. Español -> Inglés");
            Console.Write("Seleccione: ");
            string dir = Console.ReadLine();

            Dictionary<string, string> diccionarioOrigen;
            if (dir == "1")
                diccionarioOrigen = inglesEspanol;
            else if (dir == "2")
                diccionarioOrigen = espanolIngles;
            else
            {
                Console.WriteLine("Opción no válida. Volviendo al menú.\n");
                return;
            }

            // Expresión regular para encontrar palabras (letras, incluyendo acentos y ñ)
            string patron = @"\p{L}+";
            string resultado = Regex.Replace(frase, patron, match =>
            {
                string palabra = match.Value;
                string palabraLower = palabra.ToLower();
                if (diccionarioOrigen.ContainsKey(palabraLower))
                {
                    return diccionarioOrigen[palabraLower];
                }
                return palabra; // Si no está en el diccionario, se deja igual
            });

            Console.WriteLine($"\nFrase traducida: {resultado}\n");
        }

        static void AgregarPalabra()
        {
            Console.Write("Ingrese la palabra en inglés: ");
            string ingles = Console.ReadLine()?.Trim();
            Console.Write("Ingrese su traducción al español: ");
            string espanol = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(ingles) || string.IsNullOrEmpty(espanol))
            {
                Console.WriteLine("Ambas palabras son obligatorias.\n");
                return;
            }

            // Almacenar en minúsculas
            ingles = ingles.ToLower();
            espanol = espanol.ToLower();

            // Verificar si ya existe (opcional, se puede sobrescribir)
            if (inglesEspanol.ContainsKey(ingles))
            {
                Console.Write("La palabra en inglés ya existe. ¿Desea sobrescribirla? (s/n): ");
                if (Console.ReadLine()?.ToLower() != "s")
                {
                    Console.WriteLine("Operación cancelada.\n");
                    return;
                }
            }

            inglesEspanol[ingles] = espanol;
            espanolIngles[espanol] = ingles;
            Console.WriteLine("Palabra agregada correctamente.\n");
        }
    }
}