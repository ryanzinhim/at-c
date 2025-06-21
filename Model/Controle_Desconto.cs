namespace EXPEXturism.Model
{
    public delegate decimal CalculateDelegate(decimal valorDesconto);
    public class Controle_Desconto
    {
        public static decimal DescontoDez(decimal valor)
        {
            return valor * 0.9m;
        }
    }
}
