using System;

namespace BSTApp
{
    // Clase que representa un nodo del árbol
    public class Nodo
    {
        public int Valor { get; set; }
        public Nodo Izquierdo { get; set; }
        public Nodo Derecho { get; set; }

        public Nodo(int valor)
        {
            Valor = valor;
            Izquierdo = null;
            Derecho = null;
        }
    }

    // Clase que implementa el Árbol Binario de Búsqueda
    public class ArbolBinarioBusqueda
    {
        private Nodo raiz;

        public ArbolBinarioBusqueda()
        {
            raiz = null;
        }

        // Insertar un valor
        public void Insertar(int valor)
        {
            raiz = InsertarRecursivo(raiz, valor);
        }

        private Nodo InsertarRecursivo(Nodo nodo, int valor)
        {
            if (nodo == null)
            {
                nodo = new Nodo(valor);
                return nodo;
            }

            if (valor < nodo.Valor)
                nodo.Izquierdo = InsertarRecursivo(nodo.Izquierdo, valor);
            else if (valor > nodo.Valor)
                nodo.Derecho = InsertarRecursivo(nodo.Derecho, valor);
            // Si el valor ya existe, no hacemos nada (se puede omitir o manejar)

            return nodo;
        }

        // Buscar un valor
        public bool Buscar(int valor)
        {
            return BuscarRecursivo(raiz, valor);
        }

        private bool BuscarRecursivo(Nodo nodo, int valor)
        {
            if (nodo == null)
                return false;

            if (valor == nodo.Valor)
                return true;
            else if (valor < nodo.Valor)
                return BuscarRecursivo(nodo.Izquierdo, valor);
            else
                return BuscarRecursivo(nodo.Derecho, valor);
        }

        // Eliminar un valor
        public void Eliminar(int valor)
        {
            raiz = EliminarRecursivo(raiz, valor);
        }

        private Nodo EliminarRecursivo(Nodo nodo, int valor)
        {
            if (nodo == null)
                return null;

            if (valor < nodo.Valor)
                nodo.Izquierdo = EliminarRecursivo(nodo.Izquierdo, valor);
            else if (valor > nodo.Valor)
                nodo.Derecho = EliminarRecursivo(nodo.Derecho, valor);
            else
            {
                // Nodo encontrado
                // Caso 1: sin hijos o un solo hijo
                if (nodo.Izquierdo == null)
                    return nodo.Derecho;
                else if (nodo.Derecho == null)
                    return nodo.Izquierdo;

                // Caso 2: dos hijos -> buscar el sucesor inorden (el mínimo del subárbol derecho)
                Nodo sucesor = ObtenerMinimoNodo(nodo.Derecho);
                nodo.Valor = sucesor.Valor;
                nodo.Derecho = EliminarRecursivo(nodo.Derecho, sucesor.Valor);
            }
            return nodo;
        }

        // Obtener el nodo con el valor mínimo (para uso interno)
        private Nodo ObtenerMinimoNodo(Nodo nodo)
        {
            while (nodo.Izquierdo != null)
                nodo = nodo.Izquierdo;
            return nodo;
        }

        // Recorrido Preorden (raíz, izquierdo, derecho)
        public void RecorridoPreorden()
        {
            PreordenRecursivo(raiz);
            Console.WriteLine();
        }

        private void PreordenRecursivo(Nodo nodo)
        {
            if (nodo != null)
            {
                Console.Write(nodo.Valor + " ");
                PreordenRecursivo(nodo.Izquierdo);
                PreordenRecursivo(nodo.Derecho);
            }
        }

        // Recorrido Inorden (izquierdo, raíz, derecho)
        public void RecorridoInorden()
        {
            InordenRecursivo(raiz);
            Console.WriteLine();
        }

        private void InordenRecursivo(Nodo nodo)
        {
            if (nodo != null)
            {
                InordenRecursivo(nodo.Izquierdo);
                Console.Write(nodo.Valor + " ");
                InordenRecursivo(nodo.Derecho);
            }
        }

        // Recorrido Postorden (izquierdo, derecho, raíz)
        public void RecorridoPostorden()
        {
            PostordenRecursivo(raiz);
            Console.WriteLine();
        }

        private void PostordenRecursivo(Nodo nodo)
        {
            if (nodo != null)
            {
                PostordenRecursivo(nodo.Izquierdo);
                PostordenRecursivo(nodo.Derecho);
                Console.Write(nodo.Valor + " ");
            }
        }

        // Valor mínimo
        public int? ObtenerMinimo()
        {
            if (raiz == null)
                return null;

            Nodo actual = raiz;
            while (actual.Izquierdo != null)
                actual = actual.Izquierdo;
            return actual.Valor;
        }

        // Valor máximo
        public int? ObtenerMaximo()
        {
            if (raiz == null)
                return null;

            Nodo actual = raiz;
            while (actual.Derecho != null)
                actual = actual.Derecho;
            return actual.Valor;
        }

        // Altura del árbol
        public int Altura()
        {
            return AlturaRecursivo(raiz);
        }

        private int AlturaRecursivo(Nodo nodo)
        {
            if (nodo == null)
                return -1; // Altura de árbol vacío es -1 (o 0 según convención)
            int alturaIzq = AlturaRecursivo(nodo.Izquierdo);
            int alturaDer = AlturaRecursivo(nodo.Derecho);
            return Math.Max(alturaIzq, alturaDer) + 1;
        }

        // Limpiar completamente el árbol
        public void Limpiar()
        {
            raiz = null;
        }
    }

    // Programa principal con menú interactivo
    class Program
    {
        static void Main(string[] args)
        {
            ArbolBinarioBusqueda arbol = new ArbolBinarioBusqueda();
            int opcion;
            int valor;

            do
            {
                Console.Clear();
                Console.WriteLine("=== MENÚ ÁRBOL BINARIO DE BÚSQUEDA ===");
                Console.WriteLine("1. Insertar valor");
                Console.WriteLine("2. Buscar valor");
                Console.WriteLine("3. Eliminar valor");
                Console.WriteLine("4. Mostrar recorrido Preorden");
                Console.WriteLine("5. Mostrar recorrido Inorden");
                Console.WriteLine("6. Mostrar recorrido Postorden");
                Console.WriteLine("7. Mostrar valor mínimo");
                Console.WriteLine("8. Mostrar valor máximo");
                Console.WriteLine("9. Mostrar altura del árbol");
                Console.WriteLine("10. Limpiar árbol");
                Console.WriteLine("0. Salir");
                Console.Write("Seleccione una opción: ");

                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    opcion = -1;
                }

                switch (opcion)
                {
                    case 1:
                        Console.Write("Ingrese el valor a insertar: ");
                        if (int.TryParse(Console.ReadLine(), out valor))
                        {
                            arbol.Insertar(valor);
                            Console.WriteLine($"Valor {valor} insertado.");
                        }
                        else
                        {
                            Console.WriteLine("Valor inválido.");
                        }
                        break;

                    case 2:
                        Console.Write("Ingrese el valor a buscar: ");
                        if (int.TryParse(Console.ReadLine(), out valor))
                        {
                            bool encontrado = arbol.Buscar(valor);
                            Console.WriteLine(encontrado ? $"El valor {valor} SÍ está en el árbol." : $"El valor {valor} NO está en el árbol.");
                        }
                        else
                        {
                            Console.WriteLine("Valor inválido.");
                        }
                        break;

                    case 3:
                        Console.Write("Ingrese el valor a eliminar: ");
                        if (int.TryParse(Console.ReadLine(), out valor))
                        {
                            arbol.Eliminar(valor);
                            Console.WriteLine($"Valor {valor} eliminado (si existía).");
                        }
                        else
                        {
                            Console.WriteLine("Valor inválido.");
                        }
                        break;

                    case 4:
                        Console.Write("Recorrido Preorden: ");
                        arbol.RecorridoPreorden();
                        break;

                    case 5:
                        Console.Write("Recorrido Inorden: ");
                        arbol.RecorridoInorden();
                        break;

                    case 6:
                        Console.Write("Recorrido Postorden: ");
                        arbol.RecorridoPostorden();
                        break;

                    case 7:
                        int? min = arbol.ObtenerMinimo();
                        if (min.HasValue)
                            Console.WriteLine($"Valor mínimo: {min.Value}");
                        else
                            Console.WriteLine("El árbol está vacío.");
                        break;

                    case 8:
                        int? max = arbol.ObtenerMaximo();
                        if (max.HasValue)
                            Console.WriteLine($"Valor máximo: {max.Value}");
                        else
                            Console.WriteLine("El árbol está vacío.");
                        break;

                    case 9:
                        int altura = arbol.Altura();
                        Console.WriteLine($"Altura del árbol: {altura}");
                        break;

                    case 10:
                        arbol.Limpiar();
                        Console.WriteLine("Árbol limpiado completamente.");
                        break;

                    case 0:
                        Console.WriteLine("Saliendo...");
                        break;

                    default:
                        Console.WriteLine("Opción no válida. Intente de nuevo.");
                        break;
                }

                if (opcion != 0)
                {
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                }

            } while (opcion != 0);
        }
    }
}