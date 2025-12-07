using System;

public class Rectangulo
{
    private double largo;
    private double ancho;
    
    public Rectangulo(double l, double a)
    {
        largo = l;
        ancho = a;
    }
    
    public double Largo
    {
        get { return largo; }
        set { largo = value; }
    }
    
    public double Ancho
    {
        get { return ancho; }
        set { ancho = value; }
    }
    
    public double CalcularArea()
    {
        return largo * ancho;
    }
    
    public double CalcularPerimetro()
    {
        return 2 * (largo + ancho);
    }
}