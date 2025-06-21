using System.ComponentModel.DataAnnotations;

namespace EXPEXturism.Model;

public class Destino
{

    public int Id { get; set; }

    [Required(ErrorMessage = "O nome da Cidade é obrigatório!")]
    [MinLength(3, ErrorMessage = "O nome da cidade deve ter no mínimo 3 caracteres!")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "A sigla do país é obrigatório!")]
    [MinLength(2, ErrorMessage = "Use 2 ou 3 letras.")]
    [MaxLength(3, ErrorMessage = "Use no máximo 3 letras.")]
    public string Pais { get; set; }

    public List<PacoteDestino> PacoteDestinos { get; set; } = new();

    public Destino() { }
    public Destino( string nome, string pais)
    {
        Nome = nome;
        Pais = pais;
    }

    public override string ToString()
    {
        return $"Id: {Id} ({Nome}, {Pais})";
    }
}
