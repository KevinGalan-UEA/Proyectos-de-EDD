using System;
using System.Collections.Generic;

namespace BalanceoParentesis
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== VERIFICACIÓN DE PARÉNTESIS BALANCEADOS ===\n");
            
            // Expresión matemática para evaluar
            string expresion = "{7 + (8 * 5) - [(9 - 7) + (4 + 1)]}";
            Console.WriteLine($"Expresión a evaluar: {expresion}");
            
            if (VerificarParentesisBalanceados(expresion))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("✓ Fórmula CORRECTAMENTE balanceada.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("✗ Fórmula NO balanceada.");
                Console.ResetColor();
            }
            
            // Ejemplos adicionales para prueba
            Console.WriteLine("\n=== PRUEBAS ADICIONALES ===");
            
            string[] pruebas = {
                "(a + b) * (c - d)",          // Balanceada
                "{[(2 + 3) * 4] - 5}",        // Balanceada
                "{[(])}",                     // No balanceada
                "((())",                      // No balanceada
                "{[()()]}",                   // Balanceada
                "sin(x) + cos(y)",            // Balanceada (sin paréntesis extra)
                "([{()}])",                   // Balanceada
                "([{)]}",                     // No balanceada
                "",                           // Vacía (balanceada)
                "3 + 4 * 2"                   // Sin paréntesis (balanceada)
            };
            
            foreach (var prueba in pruebas)
            {
                bool resultado = VerificarParentesisBalanceados(prueba);
                Console.WriteLine($"{prueba,-30} → {(resultado ? "✓ Balanceada" : "✗ No balanceada")}");
            }
            
            // Permitir al usuario ingresar su propia expresión
            Console.WriteLine("\n=== INGRESE SU PROPIA EXPRESIÓN ===");
            Console.WriteLine("(Presione Enter sin texto para salir)");
            
            while (true)
            {
                Console.Write("\nIngrese una expresión matemática: ");
                string input = Console.ReadLine();
                
                if (string.IsNullOrWhiteSpace(input))
                    break;
                    
                bool resultado = VerificarParentesisBalanceados(input);
                
                if (resultado)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("✓ Expresión balanceada correctamente.");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("✗ Expresión NO balanceada.");
                    Console.ResetColor();
                }
            }
            
            Console.WriteLine("\nPrograma finalizado. Presione cualquier tecla para salir...");
            Console.ReadKey();
        }
        
        /// <summary>
        /// Verifica si los paréntesis, llaves y corchetes en una expresión
        /// están correctamente balanceados utilizando una pila (stack).
        /// 
        /// PRINCIPIO DE FUNCIONAMIENTO:
        /// 1. Recorre la expresión carácter por carácter
        /// 2. Si encuentra un símbolo de apertura ('(', '[', '{'), lo APILA
        /// 3. Si encuentra un símbolo de cierre (')', ']', '}'), verifica:
        ///    a. Si la pila está vacía → Error (cierre sin apertura)
        ///    b. Si el tope de la pila NO es el símbolo de apertura correspondiente → Error
        ///    c. Si coincide, DESAPILA el símbolo de apertura
        /// 4. Al final, si la pila está vacía → Expresión balanceada
        ///    Si quedan símbolos en la pila → Expresión no balanceada
        /// 
        /// COMPLEJIDAD:
        /// - Tiempo: O(n) donde n es la longitud de la expresión
        /// - Espacio: O(n) en el peor caso (cuando todos los caracteres son símbolos de apertura)
        /// </summary>
        /// <param name="expresion">Expresión matemática a evaluar</param>
        /// <returns>True si la expresión está balanceada, False en caso contrario</returns>
        static bool VerificarParentesisBalanceados(string expresion)
        {
            // Crear una pila para almacenar los símbolos de apertura
            // Stack<T> es una estructura de datos LIFO (Last In, First Out)
            Stack<char> pila = new Stack<char>();
            
            // Diccionario para mapear rápidamente símbolos de cierre con sus aperturas correspondientes
            // Esto permite verificar fácilmente si un cierre coincide con el último apertura
            Dictionary<char, char> paresCorrespondientes = new Dictionary<char, char>
            {
                {')', '('},  // El paréntesis de cierre ')' corresponde al de apertura '('
                {']', '['},  // El corchete de cierre ']' corresponde al de apertura '['
                {'}', '{'}   // La llave de cierre '}' corresponde a la de apertura '{'
            };
            
            // Recorrer cada carácter de la expresión
            foreach (char caracter in expresion)
            {
                // CASO 1: Símbolo de apertura
                if (caracter == '(' || caracter == '[' || caracter == '{')
                {
                    // Apilar el símbolo de apertura
                    // Push() añade un elemento al tope de la pila
                    pila.Push(caracter);
                }
                // CASO 2: Símbolo de cierre
                else if (caracter == ')' || caracter == ']' || caracter == '}')
                {
                    // SUBCASO 2.1: Pila vacía (no hay apertura para este cierre)
                    if (pila.Count == 0)
                    {
                        return false; // Expresión no balanceada
                    }
                    
                    // SUBCASO 2.2: Verificar si el último símbolo de apertura coincide
                    // Pop() elimina y devuelve el elemento en el tope de la pila
                    char ultimoApertura = pila.Pop();
                    
                    // Comparar con el símbolo de apertura esperado para este cierre
                    if (ultimoApertura != paresCorrespondientes[caracter])
                    {
                        return false; // Los símbolos no coinciden
                    }
                }
                // CASO 3: Otros caracteres (números, letras, operadores, espacios)
                // Estos se ignoran, no afectan el balanceo
            }
            
            // Al final del recorrido, la expresión está balanceada si y solo si:
            // 1. No quedan símbolos de apertura sin cerrar (pila vacía)
            // 2. No se encontraron cierres sin su apertura correspondiente (ya manejado arriba)
            return pila.Count == 0;
        }
    }
}