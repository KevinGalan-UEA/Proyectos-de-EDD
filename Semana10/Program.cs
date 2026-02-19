using System;
using System.Collections.Generic;
using System.Linq;

class ProgramaVacunacion
{
    static void Main(string[] args)
    {
        // ==============================
        // 1. Crear universo de ciudadanos
        // ==============================
        HashSet<string> ciudadanos = new HashSet<string>();

        for (int i = 1; i <= 500; i++)
        {
            ciudadanos.Add("Ciudadano " + i);
        }

        // ==============================
        // 2. Crear conjunto Pfizer
        // ==============================
        HashSet<string> pfizer = new HashSet<string>();

        for (int i = 1; i <= 75; i++)
        {
            pfizer.Add("Ciudadano " + i);
        }

        // ==============================
        // 3. Crear conjunto AstraZeneca
        // (Incluye algunos repetidos para simular ambas dosis)
        // ==============================
        HashSet<string> astraZeneca = new HashSet<string>();

        for (int i = 50; i < 125; i++)
        {
            astraZeneca.Add("Ciudadano " + i);
        }

        // ==============================
        // 4. Operaciones de conjuntos
        // ==============================

        // Unión (P ∪ A)
        HashSet<string> vacunados = new HashSet<string>(pfizer);
        vacunados.UnionWith(astraZeneca);

        // No vacunados (U − (P ∪ A))
        HashSet<string> noVacunados = new HashSet<string>(ciudadanos);
        noVacunados.ExceptWith(vacunados);

        // Ambas dosis (P ∩ A)
        HashSet<string> ambasDosis = new HashSet<string>(pfizer);
        ambasDosis.IntersectWith(astraZeneca);

        // Solo Pfizer (P − A)
        HashSet<string> soloPfizer = new HashSet<string>(pfizer);
        soloPfizer.ExceptWith(astraZeneca);

        // Solo AstraZeneca (A − P)
        HashSet<string> soloAstraZeneca = new HashSet<string>(astraZeneca);
        soloAstraZeneca.ExceptWith(pfizer);

        // ==============================
        // 5. Mostrar resultados
        // ==============================

        Console.WriteLine("===== RESULTADOS CAMPAÑA COVID-19 =====\n");

        Console.WriteLine("Ciudadanos NO vacunados: " + noVacunados.Count);
        Console.WriteLine("Ciudadanos con ambas dosis: " + ambasDosis.Count);
        Console.WriteLine("Ciudadanos solo Pfizer: " + soloPfizer.Count);
        Console.WriteLine("Ciudadanos solo AstraZeneca: " + soloAstraZeneca.Count);

        Console.WriteLine("\nPresione cualquier tecla para salir...");
        Console.ReadKey();
    }
}
