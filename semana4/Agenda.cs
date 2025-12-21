using System;
using System.Collections.Generic;

namespace AgendaTelefonicaPOO
{
    // ==================== CLASE PRINCIPAL: CONTACTO ====================
    public class Contacto
    {
        // Propiedades (atributos) del contacto
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }

        // Constructor
        public Contacto(string nombre, string telefono, string email, string direccion)
        {
            Nombre = nombre;
            Telefono = telefono;
            Email = email;
            Direccion = direccion;
        }

        // Método para mostrar información del contacto
        public void MostrarInformacion()
        {
            Console.WriteLine($"📞 Contacto: {Nombre}");
            Console.WriteLine($"   Teléfono: {Telefono}");
            Console.WriteLine($"   Email: {Email}");
            Console.WriteLine($"   Dirección: {Direccion}");
            Console.WriteLine("----------------------------------------");
        }
    }

    // ==================== CLASE: AGENDA TELEFÓNICA ====================
    public class AgendaTelefonica
    {
        // VECTOR (Array) para almacenar contactos
        private Contacto[] contactosArray;
        private int contadorContactos;

        // LISTA (List<>) para almacenar contactos (alternativa moderna)
        private List<Contacto> contactosLista;

        // MATRIZ para estadísticas por letra inicial
        private string[,] matrizEstadisticas;

        // CONSTRUCTOR
        public AgendaTelefonica(int capacidadMaxima)
        {
            contactosArray = new Contacto[capacidadMaxima];
            contactosLista = new List<Contacto>();
            contadorContactos = 0;
            matrizEstadisticas = new string[26, 2]; // 26 letras, 2 columnas
            InicializarMatriz();
        }

        // ==================== MÉTODOS PRINCIPALES ====================

        // 1. AGREGAR CONTACTO (usando Array)
        public bool AgregarContactoArray(Contacto nuevoContacto)
        {
            if (contadorContactos >= contactosArray.Length)
            {
                Console.WriteLine("❌ Agenda llena. No se puede agregar más contactos.");
                return false;
            }

            contactosArray[contadorContactos] = nuevoContacto;
            contadorContactos++;
            contactosLista.Add(nuevoContacto); // También agregar a la lista
            ActualizarEstadisticas(nuevoContacto.Nombre[0]);
            Console.WriteLine($"✅ Contacto '{nuevoContacto.Nombre}' agregado exitosamente.");
            return true;
        }

        // 2. ELIMINAR CONTACTO
        public bool EliminarContacto(string nombre)
        {
            for (int i = 0; i < contadorContactos; i++)
            {
                if (contactosArray[i].Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase))
                {
                    // Eliminar de array (desplazar elementos)
                    for (int j = i; j < contadorContactos - 1; j++)
                    {
                        contactosArray[j] = contactosArray[j + 1];
                    }
                    contadorContactos--;

                    // Eliminar de lista
                    Contacto contactoEliminar = contactosLista.Find(c => c.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
                    if (contactoEliminar != null)
                        contactosLista.Remove(contactoEliminar);

                    Console.WriteLine($"✅ Contacto '{nombre}' eliminado.");
                    return true;
                }
            }
            Console.WriteLine($"❌ Contacto '{nombre}' no encontrado.");
            return false;
        }

        // 3. BUSCAR CONTACTO
        public void BuscarContacto(string nombre)
        {
            bool encontrado = false;
            for (int i = 0; i < contadorContactos; i++)
            {
                if (contactosArray[i].Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase))
                {
                    contactosArray[i].MostrarInformacion();
                    encontrado = true;
                }
            }
            if (!encontrado)
                Console.WriteLine($"🔍 No se encontraron contactos con '{nombre}'.");
        }

        // 4. MOSTRAR TODOS LOS CONTACTOS (Array)
        public void MostrarTodosArray()
        {
            Console.WriteLine("\n📒 CONTACTOS (Array):");
            Console.WriteLine("========================================");
            if (contadorContactos == 0)
            {
                Console.WriteLine("La agenda está vacía.");
                return;
            }

            for (int i = 0; i < contadorContactos; i++)
            {
                Console.Write($"[{i + 1}] ");
                contactosArray[i].MostrarInformacion();
            }
        }

        // 5. MOSTRAR CONTACTOS (Lista)
        public void MostrarTodosLista()
        {
            Console.WriteLine("\n📒 CONTACTOS (Lista):");
            Console.WriteLine("========================================");
            if (contactosLista.Count == 0)
            {
                Console.WriteLine("La agenda está vacía.");
                return;
            }

            int index = 1;
            foreach (var contacto in contactosLista)
            {
                Console.Write($"[{index}] ");
                contacto.MostrarInformacion();
                index++;
            }
        }

        // ==================== MÉTODOS DE ESTADÍSTICAS (Matriz) ====================
        private void InicializarMatriz()
        {
            char letra = 'A';
            for (int i = 0; i < 26; i++)
            {
                matrizEstadisticas[i, 0] = letra.ToString();
                matrizEstadisticas[i, 1] = "0";
                letra++;
            }
        }

        private void ActualizarEstadisticas(char primeraLetra)
        {
            primeraLetra = char.ToUpper(primeraLetra);
            int indice = primeraLetra - 'A';
            if (indice >= 0 && indice < 26)
            {
                int cantidad = int.Parse(matrizEstadisticas[indice, 1]);
                matrizEstadisticas[indice, 1] = (cantidad + 1).ToString();
            }
        }

        public void MostrarEstadisticas()
        {
            Console.WriteLine("\n📊 ESTADÍSTICAS POR LETRA INICIAL:");
            Console.WriteLine("========================================");
            for (int i = 0; i < 26; i++)
            {
                if (matrizEstadisticas[i, 1] != "0")
                {
                    Console.WriteLine($"Letra {matrizEstadisticas[i, 0]}: {matrizEstadisticas[i, 1]} contactos");
                }
            }
        }

        // ==================== REGISTRO (Struct) ====================
        public struct RegistroOperacion
        {
            public DateTime Fecha;
            public string TipoOperacion;
            public string Detalle;

            public void MostrarRegistro()
            {
                Console.WriteLine($"[{Fecha:HH:mm:ss}] {TipoOperacion}: {Detalle}");
            }
        }

        private List<RegistroOperacion> registros = new List<RegistroOperacion>();

        public void AgregarRegistro(string tipo, string detalle)
        {
            RegistroOperacion nuevoRegistro = new RegistroOperacion
            {
                Fecha = DateTime.Now,
                TipoOperacion = tipo,
                Detalle = detalle
            };
            registros.Add(nuevoRegistro);
        }

        public void MostrarRegistros()
        {
            Console.WriteLine("\n📝 REGISTRO DE OPERACIONES:");
            Console.WriteLine("========================================");
            foreach (var registro in registros)
            {
                registro.MostrarRegistro();
            }
        }
    }

    // ==================== PROGRAMA PRINCIPAL ====================
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("      AGENDA TELEFÓNICA - C# POO       ");
            Console.WriteLine("========================================\n");

            // Crear agenda con capacidad para 100 contactos
            AgendaTelefonica agenda = new AgendaTelefonica(100);

            // Agregar algunos contactos de ejemplo
            agenda.AgregarContactoArray(new Contacto("Juan Pérez", "0991234567", "juan@email.com", "Calle 123"));
            agenda.AgregarRegistro("AGREGAR", "Juan Pérez");

            agenda.AgregarContactoArray(new Contacto("Ana Gómez", "0987654321", "ana@email.com", "Av. Principal"));
            agenda.AgregarRegistro("AGREGAR", "Ana Gómez");

            agenda.AgregarContactoArray(new Contacto("Carlos Ruiz", "0971234567", "carlos@email.com", "Calle 456"));
            agenda.AgregarRegistro("AGREGAR", "Carlos Ruiz");

            agenda.AgregarContactoArray(new Contacto("María López", "0967654321", "maria@email.com", "Av. Central"));
            agenda.AgregarRegistro("AGREGAR", "María López");

            // Menú principal
            bool salir = false;
            while (!salir)
            {
                Console.WriteLine("\n📱 MENÚ PRINCIPAL:");
                Console.WriteLine("1. Agregar contacto");
                Console.WriteLine("2. Mostrar todos los contactos (Array)");
                Console.WriteLine("3. Mostrar todos los contactos (Lista)");
                Console.WriteLine("4. Buscar contacto");
                Console.WriteLine("5. Eliminar contacto");
                Console.WriteLine("6. Ver estadísticas");
                Console.WriteLine("7. Ver registro de operaciones");
                Console.WriteLine("8. Salir");
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        Console.Write("Nombre: ");
                        string nombre = Console.ReadLine();
                        Console.Write("Teléfono: ");
                        string telefono = Console.ReadLine();
                        Console.Write("Email: ");
                        string email = Console.ReadLine();
                        Console.Write("Dirección: ");
                        string direccion = Console.ReadLine();

                        Contacto nuevo = new Contacto(nombre, telefono, email, direccion);
                        if (agenda.AgregarContactoArray(nuevo))
                            agenda.AgregarRegistro("AGREGAR", nombre);
                        break;

                    case "2":
                        agenda.MostrarTodosArray();
                        break;

                    case "3":
                        agenda.MostrarTodosLista();
                        break;

                    case "4":
                        Console.Write("Nombre a buscar: ");
                        string buscar = Console.ReadLine();
                        agenda.BuscarContacto(buscar);
                        agenda.AgregarRegistro("BUSCAR", buscar);
                        break;

                    case "5":
                        Console.Write("Nombre a eliminar: ");
                        string eliminar = Console.ReadLine();
                        if (agenda.EliminarContacto(eliminar))
                            agenda.AgregarRegistro("ELIMINAR", eliminar);
                        break;

                    case "6":
                        agenda.MostrarEstadisticas();
                        break;

                    case "7":
                        agenda.MostrarRegistros();
                        break;

                    case "8":
                        salir = true;
                        Console.WriteLine("👋 ¡Gracias por usar la Agenda Telefónica!");
                        break;

                    default:
                        Console.WriteLine("❌ Opción no válida.");
                        break;
                }
            }
        }
    }
}