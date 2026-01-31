using System;
using System.Collections.Generic;
using System.Threading;

namespace SistemaAuditorio
{
    // Clase para representar a cada persona que viene al congreso
    class Persona
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime HoraLlegada { get; set; }
        public int Asiento { get; set; }
        public string QuienLoRegistro { get; set; }
        
        public Persona(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
            HoraLlegada = DateTime.Now;
            Asiento = 0; // 0 significa que aún no tiene asiento
            QuienLoRegistro = "Nadie";
        }
    }

    // Clase principal que maneja todo el sistema del auditorio
    class ControlAuditorio
    {
        // La fila de personas esperando
        private Queue<Persona> filaEspera;
        
        // Para llevar control de qué asientos están ocupados
        private bool[] asientosOcupados;
        
        // Lista de todas las personas que ya tienen asiento
        private List<Persona> personasConAsiento;
        
        // Para evitar que los dos registradores asignen el mismo asiento
        private object candado = new object();
        
        // Contadores para las estadísticas
        private int totalAsientos;
        private int asientosAsignados;
        private int registradosPorUno;
        private int registradosPorDos;
        
        public ControlAuditorio(int cantidadAsientos)
        {
            totalAsientos = cantidadAsientos;
            filaEspera = new Queue<Persona>();
            asientosOcupados = new bool[cantidadAsientos];
            personasConAsiento = new List<Persona>();
            asientosAsignados = 0;
            registradosPorUno = 0;
            registradosPorDos = 0;
            
            Console.WriteLine("Sistema de auditorio creado con " + cantidadAsientos + " asientos");
        }
        
        // Método para que lleguen personas al congreso
        public void LlegaPersona(string nombre)
        {
            lock (candado)
            {
                // Le damos un ID basado en cuántas personas han llegado
                int nuevoId = filaEspera.Count + personasConAsiento.Count + 1;
                Persona nuevaPersona = new Persona(nuevoId, nombre);
                filaEspera.Enqueue(nuevaPersona);
                
                // Esto es solo para ver qué está pasando
                Console.WriteLine("Llegó " + nombre + " a la fila. Hay " + filaEspera.Count + " en espera.");
            }
        }
        
        // Esto es lo que hace cada registrador
        public void TrabajarRegistrador(string nombreRegistrador)
        {
            // Sigue trabajando mientras haya asientos
            while (asientosAsignados < totalAsientos)
            {
                Persona personaAtendida = null;
                
                lock (candado)
                {
                    // Ver si hay alguien en la fila y si hay asientos
                    if (filaEspera.Count > 0 && asientosAsignados < totalAsientos)
                    {
                        // Sacar a la primera persona de la fila
                        personaAtendida = filaEspera.Dequeue();
                        personaAtendida.QuienLoRegistro = nombreRegistrador;
                        
                        // Buscar un asiento libre
                        for (int i = 0; i < totalAsientos; i++)
                        {
                            if (!asientosOcupados[i])
                            {
                                // Marcar el asiento como ocupado
                                asientosOcupados[i] = true;
                                personaAtendida.Asiento = i + 1; // +1 porque los asientos empiezan en 1
                                asientosAsignados++;
                                personasConAsiento.Add(personaAtendida);
                                
                                // Llevar la cuenta de quién registró a quién
                                if (nombreRegistrador == "Registrador 1")
                                    registradosPorUno++;
                                else
                                    registradosPorDos++;
                                
                                break; // Ya encontramos asiento, salimos del for
                            }
                        }
                    }
                }
                
                if (personaAtendida != null)
                {
                    // Mostrar qué hizo el registrador
                    Console.WriteLine(nombreRegistrador + " le dio el asiento " + 
                                     personaAtendida.Asiento + " a " + personaAtendida.Nombre);
                    
                    // Simular que el registro toma un poco de tiempo
                    Thread.Sleep(new Random().Next(100, 400));
                }
                else
                {
                    // Si no hay nadie en la fila, esperar un poco
                    Thread.Sleep(50);
                }
            }
        }
        
        // Para ver cómo va la cosa
        public void MostrarEstado()
        {
            Console.WriteLine("\n=== Estado del Auditorio ===");
            Console.WriteLine("Asientos totales: " + totalAsientos);
            Console.WriteLine("Asientos ocupados: " + asientosAsignados);
            Console.WriteLine("Asientos libres: " + (totalAsientos - asientosAsignados));
            Console.WriteLine("Personas en fila: " + filaEspera.Count);
            Console.WriteLine("Registrador 1 ha atendido: " + registradosPorUno + " personas");
            Console.WriteLine("Registrador 2 ha atendido: " + registradosPorDos + " personas");
            
            if (asientosAsignados >= totalAsientos)
            {
                Console.WriteLine("¡AUDITORIO LLENO!");
            }
        }
        
        // Para ver cómo están los asientos
        public void MostrarAsientos()
        {
            Console.WriteLine("\n=== Mapa de Asientos ===");
            for (int i = 0; i < totalAsientos; i++)
            {
                if (i % 10 == 0) Console.WriteLine(); // Nueva línea cada 10 asientos
                
                if (asientosOcupados[i])
                    Console.Write("[X] ");
                else
                    Console.Write("[ ] ");
            }
            Console.WriteLine("\n(X = ocupado,   = libre)");
        }
        
        // Para ver la lista de todos los que tienen asiento
        public void MostrarListaAsistentes()
        {
            Console.WriteLine("\n=== Lista de Asistentes ===");
            
            if (personasConAsiento.Count == 0)
            {
                Console.WriteLine("Todavía no hay nadie registrado");
                return;
            }
            
            Console.WriteLine("ID\tNombre\t\tAsiento\tRegistrador");
            Console.WriteLine("----------------------------------------");
            
            // Ordenar por número de asiento para que se vea mejor
            personasConAsiento.Sort((a, b) => a.Asiento.CompareTo(b.Asiento));
            
            foreach (Persona p in personasConAsiento)
            {
                Console.WriteLine(p.Id + "\t" + p.Nombre + "\t\t" + 
                                p.Asiento + "\t" + p.QuienLoRegistro);
            }
        }
        
        // Para saber si ya se llenó el auditorio
        public bool EstaLleno()
        {
            return asientosAsignados >= totalAsientos;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("SISTEMA DE CONGRESO - AUDITORIO");
            Console.WriteLine("===============================\n");
            
            // Crear el auditorio con 100 asientos
            ControlAuditorio auditorio = new ControlAuditorio(100);
            
            // Algunos nombres para generar personas
            string[] nombres = {"Ana", "Luis", "Carlos", "Maria", "Pedro", "Laura", 
                              "Jorge", "Sofia", "Miguel", "Elena", "David", "Carmen"};
            
            Random aleatorio = new Random();
            
            // Iniciar los dos registradores en hilos separados
            Thread registrador1 = new Thread(() => auditorio.TrabajarRegistrador("Registrador 1"));
            Thread registrador2 = new Thread(() => auditorio.TrabajarRegistrador("Registrador 2"));
            
            registrador1.Start();
            registrador2.Start();
            
            // Simular la llegada de personas
            int contadorPersonas = 1;
            
            while (!auditorio.EstaLleno())
            {
                // Llegan entre 1 y 4 personas cada vez
                int cuantasLlegan = aleatorio.Next(1, 5);
                
                for (int i = 0; i < cuantasLlegan; i++)
                {
                    if (auditorio.EstaLleno()) break;
                    
                    string nombre = nombres[aleatorio.Next(nombres.Length)] + " " + contadorPersonas;
                    auditorio.LlegaPersona(nombre);
                    contadorPersonas++;
                }
                
                // Esperar un tiempo antes de que lleguen más personas
                Thread.Sleep(aleatorio.Next(300, 700));
                
                // Mostrar el estado cada cierto tiempo
                if (contadorPersonas % 15 == 0)
                {
                    auditorio.MostrarEstado();
                }
            }
            
            // Esperar a que los registradores terminen
            registrador1.Join();
            registrador2.Join();
            
            // Mostrar resultados finales
            Console.WriteLine("\n\n=== RESULTADOS FINALES ===");
            Console.WriteLine("=========================\n");
            
            auditorio.MostrarEstado();
            auditorio.MostrarAsientos();
            auditorio.MostrarListaAsistentes();
            
            Console.WriteLine("\nTodo listo. Presiona una tecla para salir...");
            Console.ReadKey();
        }
    }
}