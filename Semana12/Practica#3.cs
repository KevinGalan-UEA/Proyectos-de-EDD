using System;
using System.Collections.Generic;
using System.Linq;

namespace BibliotecaApp
{
    // Clase que representa un libro
    public class Libro
    {
        public string ISBN { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int AñoPublicacion { get; set; }
        public bool Disponible { get; set; }

        public Libro(string isbn, string titulo, string autor, int año)
        {
            ISBN = isbn;
            Titulo = titulo;
            Autor = autor;
            AñoPublicacion = año;
            Disponible = true; // Por defecto disponible
        }

        public override string ToString()
        {
            return $"ISBN: {ISBN} | Título: {Titulo} | Autor: {Autor} | Año: {AñoPublicacion} | {(Disponible ? "Disponible" : "Prestado")}";
        }
    }

    // Clase que gestiona la colección de libros usando un Dictionary (mapa)
    public class Biblioteca
    {
        // Mapa: clave = ISBN (string), valor = objeto Libro
        private Dictionary<string, Libro> librosPorISBN;

        // Conjunto para almacenar títulos (opcional, para búsquedas rápidas por título exacto)
        // pero como puede haber títulos repetidos (ej. mismo libro diferente edición), no lo usaremos para evitar duplicados.
        // En su lugar, para búsqueda por título usaremos LINQ sobre el diccionario.

        public Biblioteca()
        {
            librosPorISBN = new Dictionary<string, Libro>();
        }

        // Agregar un nuevo libro (clave ISBN única)
        public void AgregarLibro(Libro nuevoLibro)
        {
            if (librosPorISBN.ContainsKey(nuevoLibro.ISBN))
            {
                Console.WriteLine("Error: Ya existe un libro con ese ISBN.");
            }
            else
            {
                librosPorISBN.Add(nuevoLibro.ISBN, nuevoLibro);
                Console.WriteLine("Libro agregado correctamente.");
            }
        }

        // Buscar libro por ISBN (búsqueda directa en el diccionario, O(1))
        public void BuscarPorISBN(string isbn)
        {
            if (librosPorISBN.TryGetValue(isbn, out Libro libro))
            {
                Console.WriteLine(libro);
            }
            else
            {
                Console.WriteLine("No se encontró ningún libro con ese ISBN.");
            }
        }

        // Buscar libros por título (búsqueda lineal, O(n))
        public void BuscarPorTitulo(string titulo)
        {
            var resultados = librosPorISBN.Values.Where(l => l.Titulo.Contains(titulo, StringComparison.OrdinalIgnoreCase)).ToList();
            if (resultados.Any())
            {
                Console.WriteLine($"Libros que contienen '{titulo}':");
                foreach (var libro in resultados)
                    Console.WriteLine(libro);
            }
            else
            {
                Console.WriteLine("No se encontraron libros con ese título.");
            }
        }

        // Listar todos los libros
        public void ListarTodos()
        {
            if (librosPorISBN.Count == 0)
            {
                Console.WriteLine("No hay libros registrados.");
                return;
            }

            Console.WriteLine("=== LISTADO COMPLETO DE LIBROS ===");
            foreach (var libro in librosPorISBN.Values)
            {
                Console.WriteLine(libro);
            }
        }

        // Prestar un libro (cambiar disponibilidad a false)
        public void PrestarLibro(string isbn)
        {
            if (librosPorISBN.TryGetValue(isbn, out Libro libro))
            {
                if (libro.Disponible)
                {
                    libro.Disponible = false;
                    Console.WriteLine($"El libro '{libro.Titulo}' ha sido prestado.");
                }
                else
                {
                    Console.WriteLine("El libro ya está prestado.");
                }
            }
            else
            {
                Console.WriteLine("ISBN no encontrado.");
            }
        }

        // Devolver un libro (cambiar disponibilidad a true)
        public void DevolverLibro(string isbn)
        {
            if (librosPorISBN.TryGetValue(isbn, out Libro libro))
            {
                if (!libro.Disponible)
                {
                    libro.Disponible = true;
                    Console.WriteLine($"El libro '{libro.Titulo}' ha sido devuelto.");
                }
                else
                {
                    Console.WriteLine("El libro ya estaba disponible.");
                }
            }
            else
            {
                Console.WriteLine("ISBN no encontrado.");
            }
        }

        // Eliminar un libro por ISBN
        public void EliminarLibro(string isbn)
        {
            if (librosPorISBN.Remove(isbn))
            {
                Console.WriteLine("Libro eliminado correctamente.");
            }
            else
            {
                Console.WriteLine("ISBN no encontrado.");
            }
        }
    }

    // Programa principal con menú interactivo
    class Program
    {
        static void Main(string[] args)
        {
            Biblioteca biblioteca = new Biblioteca();
            bool salir = false;

            while (!salir)
            {
                Console.WriteLine("\n=== SISTEMA DE BIBLIOTECA ===");
                Console.WriteLine("1. Agregar libro");
                Console.WriteLine("2. Buscar por ISBN");
                Console.WriteLine("3. Buscar por título");
                Console.WriteLine("4. Listar todos los libros");
                Console.WriteLine("5. Prestar libro");
                Console.WriteLine("6. Devolver libro");
                Console.WriteLine("7. Eliminar libro");
                Console.WriteLine("8. Salir");
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        AgregarLibroInteractivo(biblioteca);
                        break;
                    case "2":
                        Console.Write("Ingrese ISBN: ");
                        string isbn = Console.ReadLine();
                        biblioteca.BuscarPorISBN(isbn);
                        break;
                    case "3":
                        Console.Write("Ingrese título (o parte): ");
                        string titulo = Console.ReadLine();
                        biblioteca.BuscarPorTitulo(titulo);
                        break;
                    case "4":
                        biblioteca.ListarTodos();
                        break;
                    case "5":
                        Console.Write("Ingrese ISBN del libro a prestar: ");
                        string isbnPrestar = Console.ReadLine();
                        biblioteca.PrestarLibro(isbnPrestar);
                        break;
                    case "6":
                        Console.Write("Ingrese ISBN del libro a devolver: ");
                        string isbnDevolver = Console.ReadLine();
                        biblioteca.DevolverLibro(isbnDevolver);
                        break;
                    case "7":
                        Console.Write("Ingrese ISBN del libro a eliminar: ");
                        string isbnEliminar = Console.ReadLine();
                        biblioteca.EliminarLibro(isbnEliminar);
                        break;
                    case "8":
                        salir = true;
                        Console.WriteLine("Saliendo del sistema...");
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            }
        }

        static void AgregarLibroInteractivo(Biblioteca biblioteca)
        {
            Console.WriteLine("--- AGREGAR NUEVO LIBRO ---");
            Console.Write("ISBN: ");
            string isbn = Console.ReadLine();
            Console.Write("Título: ");
            string titulo = Console.ReadLine();
            Console.Write("Autor: ");
            string autor = Console.ReadLine();
            Console.Write("Año de publicación: ");
            if (int.TryParse(Console.ReadLine(), out int año))
            {
                Libro nuevo = new Libro(isbn, titulo, autor, año);
                biblioteca.AgregarLibro(nuevo);
            }
            else
            {
                Console.WriteLine("Año inválido. No se agregó el libro.");
            }
        }
    }
}