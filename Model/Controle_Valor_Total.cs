namespace EXPEXturism.Model
{
    public class Controle_Valor_Total
    {
        readonly static Func<int, int, decimal> calculaTotal = (diarias, valorDiaria) => {
            return diarias * (decimal)valorDiaria;
        };

        public decimal CalcularTotal(int diarias, int valorDiaria)
        {
            if (diarias <= 0 || valorDiaria <= 0)
            {
                throw new ArgumentException("Diárias e valor da diária devem ser maiores que zero.");
            }
            return calculaTotal(diarias, valorDiaria);
        }
    }
}
