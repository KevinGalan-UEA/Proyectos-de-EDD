using System;

namespace GestionEstudiantes
{
    // Clase Estudiante basada en el ejemplo del PDF (página 16)
    public class Estudiante
    {
        // Propiedades
        public string ID { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public string[] Telefonos { get; set; } // Array para 3 teléfonos

        // Constructor
        public Estudiante(string id, string nombres, string apellidos, string direccion, string[] telefonos)
        {
            ID = id;
            Nombres = nombres;
            Apellidos = apellidos;
            Direccion = direccion;
            
            // Validar que el array tenga exactamente 3 elementos
            if (telefonos.Length == 3)
            {
                Telefonos = telefonos;
            }
            else
            {
                // Si no tiene 3, creamos un array de 3 y copiamos los disponibles
                Telefonos = new string[3];
                for (int i = 0; i < 3 && i < telefonos.Length; i++)
                {
                    Telefonos[i] = telefonos[i];
                }
            }
        }

        // Método para mostrar información del estudiante
        public void MostrarInformacion()
        {
            Console.WriteLine("\n=== INFORMACIÓN DEL ESTUDIANTE ===");
            Console.WriteLine($"ID: {ID}");
            Console.WriteLine($"Nombre completo: {Nombres} {Apellidos}");
            Console.WriteLine($"Dirección: {Direccion}");
            
            Console.WriteLine("\nTeléfonos registrados:");
            for (int i = 0; i < Telefonos.Length; i++)
            {
                Console.WriteLine($"  Teléfono {i + 1}: {Telefonos[i]}");
            }
        }

        // Método para calcular cantidad de teléfonos válidos (no vacíos)
        public int CantidadTelefonosValidos()
        {
            int contador = 0;
            foreach (string telefono in Telefonos)
            {
                if (!string.IsNullOrEmpty(telefono))
                {
                    contador++;
                }
            }
            return contador;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== GESTIÓN DE ESTUDIANTES CON POO ===\n");
            
            // Array para almacenar los teléfonos
            string[] telefonosEstudiante = new string[3];
            
            Console.WriteLine("Ingrese los datos del estudiante:");
            Console.Write("ID: ");
            string id = Console.ReadLine();
            
            Console.Write("Nombres: ");
            string nombres = Console.ReadLine();
            
            Console.Write("Apellidos: ");
            string apellidos = Console.ReadLine();
            
            Console.Write("Dirección: ");
            string direccion = Console.ReadLine();
            
            Console.WriteLine("\nIngrese los 3 números de teléfono:");
            for (int i = 0; i < 3; i++)
            {
                Console.Write($"Teléfono {i + 1}: ");
                telefonosEstudiante[i] = Console.ReadLine();
            }
            
            // Crear objeto Estudiante usando el constructor
            Estudiante estudiante = new Estudiante(id, nombres, apellidos, direccion, telefonosEstudiante);
            
            // Mostrar información
            estudiante.MostrarInformacion();
            
            // Demostrar operaciones con arrays
            Console.WriteLine($"\nTotal de teléfonos registrados: {estudiante.CantidadTelefonosValidos()}");
            
            // Ejemplo de acceso individual a teléfonos
            Console.WriteLine("\nAcceso individual a elementos del array:");
            Console.WriteLine($"Primer contacto: {estudiante.Telefonos[0]}");
            Console.WriteLine($"Segundo contacto: {estudiante.Telefonos[1]}");
            Console.WriteLine($"Tercer contacto: {estudiante.Telefonos[2]}");
            
            Console.WriteLine("\n=== CONCEPTOS APLICADOS (Del PDF) ===");
            Console.WriteLine("✓ Array unidimensional (para teléfonos)");
            Console.WriteLine("✓ Clase y objeto (Programación Orientada a Objetos)");
            Console.WriteLine("✓ Propiedades y métodos");
            Console.WriteLine("✓ Constructor con parámetros");
            Console.WriteLine("✓ Iteración con bucle for");
            
            Console.WriteLine("\nPresione cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}