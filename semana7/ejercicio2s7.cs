using System;
using System.Collections.Generic;

namespace TorresHanoi
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== PROBLEMA DE LAS TORRES DE HANOI ===\n");
            
            // Configuración inicial
            int numeroDiscos = 3;
            char torreOrigen = 'A';
            char torreDestino = 'C';
            char torreAuxiliar = 'B';
            
            // Solicitar número de discos al usuario
            Console.Write("Ingrese el número de discos (recomendado 3-5): ");
            string input = Console.ReadLine();
            
            if (!string.IsNullOrWhiteSpace(input) && int.TryParse(input, out int discos) && discos > 0)
            {
                numeroDiscos = discos;
            }
            else
            {
                Console.WriteLine($"Usando valor por defecto: {numeroDiscos} discos\n");
            }
            
            // Mostrar información del problema
            Console.WriteLine($"\nCONFIGURACIÓN INICIAL:");
            Console.WriteLine($"- Discos: {numeroDiscos}");
            Console.WriteLine($"- Torres: {torreOrigen} (Origen), {torreAuxiliar} (Auxiliar), {torreDestino} (Destino)");
            Console.WriteLine($"- Movimientos mínimos requeridos: {Math.Pow(2, numeroDiscos) - 1}");
            Console.WriteLine("\n" + new string('=', 50));
            
            // Método 1: Solución recursiva clásica
            Console.WriteLine("\nMÉTODO 1: SOLUCIÓN RECURSIVA CLÁSICA");
            Console.WriteLine("--------------------------------------");
            
            int contadorMovimientos = 0;
            ResolverHanoiRecursivo(numeroDiscos, torreOrigen, torreDestino, torreAuxiliar, ref contadorMovimientos);
            
            Console.WriteLine($"\n✓ Total de movimientos: {contadorMovimientos}");
            
            // Método 2: Solución con pilas explícitas
            Console.WriteLine("\n\nMÉTODO 2: SOLUCIÓN CON ESTRUCTURAS DE PILA");
            Console.WriteLine("--------------------------------------------");
            
            Console.WriteLine("\nEstado inicial de las torres:");
            ResolverHanoiConPilas(numeroDiscos);
            
            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("\nPresione cualquier tecla para salir...");
            Console.ReadKey();
        }
        
        /// <summary>
        /// Resuelve el problema de las Torres de Hanoi usando recursividad.
        /// </summary>
        static void ResolverHanoiRecursivo(int n, char origen, char destino, char auxiliar, ref int contador)
        {
            // CASO BASE: Si solo hay un disco, moverlo directamente
            if (n == 1)
            {
                contador++;
                Console.WriteLine($"{contador}. Mover disco 1 de {origen} a {destino}");
                return;
            }
            
            // PASO 1: Mover n-1 discos de ORIGEN a AUXILIAR
            ResolverHanoiRecursivo(n - 1, origen, auxiliar, destino, ref contador);
            
            // PASO 2: Mover el disco N (el más grande) de ORIGEN a DESTINO
            contador++;
            Console.WriteLine($"{contador}. Mover disco {n} de {origen} a {destino}");
            
            // PASO 3: Mover n-1 discos de AUXILIAR a DESTINO
            ResolverHanoiRecursivo(n - 1, auxiliar, destino, origen, ref contador);
        }
        
        /// <summary>
        /// Resuelve el problema de las Torres de Hanoi usando estructuras de pila explícitas.
        /// </summary>
        static void ResolverHanoiConPilas(int numDiscos)
        {
            // Crear tres pilas para representar las torres
            Stack<int> torreA = new Stack<int>();  // Origen
            Stack<int> torreB = new Stack<int>();  // Auxiliar
            Stack<int> torreC = new Stack<int>();  // Destino
            
            // Inicializar la torre A con los discos
            // Los discos se apilan del más grande (numDiscos) al más pequeño (1)
            for (int i = numDiscos; i >= 1; i--)
            {
                torreA.Push(i);
            }
            
            // Mostrar estado inicial
            MostrarTorres(torreA, torreB, torreC);
            
            // Resolver el problema
            int movimientos = 0;
            MoverDiscos(numDiscos, ref torreA, ref torreC, ref torreB, 'A', 'C', 'B', ref movimientos);
            
            // Mostrar estado final
            Console.WriteLine("\nEstado final:");
            MostrarTorres(torreA, torreB, torreC);
            Console.WriteLine($"\nTotal de movimientos: {movimientos}");
        }
        
        /// <summary>
        /// Función recursiva que mueve discos entre torres usando pilas
        /// </summary>
        static void MoverDiscos(int n, ref Stack<int> origen, ref Stack<int> destino, 
                               ref Stack<int> auxiliar, char nombreOrigen, 
                               char nombreDestino, char nombreAuxiliar, ref int movimientos)
        {
            if (n == 1)
            {
                // Mover disco de origen a destino
                int disco = origen.Pop();
                destino.Push(disco);
                movimientos++;
                
                Console.WriteLine($"\nMovimiento {movimientos}: Disco {disco} de {nombreOrigen} a {nombreDestino}");
                MostrarTorres(
                    nombreOrigen == 'A' ? origen : (nombreDestino == 'A' ? destino : auxiliar),
                    nombreOrigen == 'B' ? origen : (nombreDestino == 'B' ? destino : auxiliar),
                    nombreOrigen == 'C' ? origen : (nombreDestino == 'C' ? destino : auxiliar)
                );
                return;
            }
            
            // Mover n-1 discos de origen a auxiliar
            MoverDiscos(n - 1, ref origen, ref auxiliar, ref destino, nombreOrigen, nombreAuxiliar, nombreDestino, ref movimientos);
            
            // Mover el disco n de origen a destino
            int discoGrande = origen.Pop();
            destino.Push(discoGrande);
            movimientos++;
            
            Console.WriteLine($"\nMovimiento {movimientos}: Disco {discoGrande} de {nombreOrigen} a {nombreDestino}");
            MostrarTorres(
                nombreOrigen == 'A' ? origen : (nombreDestino == 'A' ? destino : auxiliar),
                nombreOrigen == 'B' ? origen : (nombreDestino == 'B' ? destino : auxiliar),
                nombreOrigen == 'C' ? origen : (nombreDestino == 'C' ? destino : auxiliar)
            );
            
            // Mover n-1 discos de auxiliar a destino
            MoverDiscos(n - 1, ref auxiliar, ref destino, ref origen, nombreAuxiliar, nombreDestino, nombreOrigen, ref movimientos);
        }
        
        /// <summary>
        /// Muestra el estado actual de las tres torres
        /// </summary>
        static void MostrarTorres(Stack<int> torreA, Stack<int> torreB, Stack<int> torreC)
        {
            Console.WriteLine($"\nTorre A: [{string.Join(", ", torreA.ToArray())}]");
            Console.WriteLine($"Torre B: [{string.Join(", ", torreB.ToArray())}]");
            Console.WriteLine($"Torre C: [{string.Join(", ", torreC.ToArray())}]");
        }
    }
}