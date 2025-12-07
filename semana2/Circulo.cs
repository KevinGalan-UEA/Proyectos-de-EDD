using System;

public class Circulo
{
    private double radio;
    
    public Circulo(double r) 
    { 
        radio = r; 
    }
    
    public double Radio
    {
        get { return radio; }
        set { radio = value; }
    }
    
    public double CalcularArea()
    {
        return Math.PI * radio * radio;
    }
    
    public double CalcularPerimetro()
    {
        return 2 * Math.PI * radio;
    }
}