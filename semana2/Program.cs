using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== CALCULADORA DE FIGURAS ===\n");
        
        // Crear y usar círculo
        Console.WriteLine("--- CÍRCULO ---");
        Circulo c = new Circulo(5);
        Console.WriteLine($"Radio: {c.Radio}");
        Console.WriteLine($"Área: {c.CalcularArea():F2}");
        Console.WriteLine($"Perímetro: {c.CalcularPerimetro():F2}");
        
        Console.WriteLine("\n--- RECTÁNGULO ---");
        Rectangulo r = new Rectangulo(4, 3);
        Console.WriteLine($"Largo: {r.Largo}, Ancho: {r.Ancho}");
        Console.WriteLine($"Área: {r.CalcularArea():F2}");
        Console.WriteLine($"Perímetro: {r.CalcularPerimetro():F2}");
        
        Console.WriteLine("\nPresione ENTER para salir...");
        Console.ReadLine();
    }
}