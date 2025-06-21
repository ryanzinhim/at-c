using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXPEXturism.Model
{
    public class Pacote_Turistico
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do pacote é obrigatório!")]
        [MinLength(3, ErrorMessage = "O nome do pacote deve ter no mínimo 3 caracteres!")]
        public string Nome { get; set; }

        [DataFutura(ErrorMessage = "A data de início deve ser uma data futura.")]
        [Required(ErrorMessage = "A data de início é obrigatória!")]
        public DateTime DataInicio { get; set; }

        [Required(ErrorMessage = "A capacidade do pacote é obrigatório!")]
        [Range(1, 100, ErrorMessage= "A capacidade máxima precisa ser positiva entre 1 e 100!")]
        public int CapacidadeMaxima { get; set; }

        [Required(ErrorMessage = "O preço do pacote é obrigatório!")]
        [Range(0.01, 99999.99, ErrorMessage = "O preço deve ser positivo.")]
        public decimal Preco { get; set; }
        public int ReservasAtuais { get; set; }

        [Required(ErrorMessage = "Ao menos um destino deve ser informado.")]
        public List<PacoteDestino> PacoteDestinos { get; set; } = new();

        public List<Destino> Destinos =>
            PacoteDestinos.Select(pd => pd.Destino).ToList();

        public Pacote_Turistico() 
        {

        }

        
    }
}
