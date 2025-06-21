using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EXPEXturism.Model
{
    public class Reserva
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ClienteId { get;  set; }

        [BindNever]
        [ValidateNever]
        [ForeignKey("ClienteId")]
        public Cliente Cliente { get; set; } = null!;

        [Required]
        public int PacoteId { get;  set; }

        [BindNever]
        [ValidateNever]
        [ForeignKey("PacoteId")]
        public Pacote_Turistico PacoteTuristico { get; set; } = null!;

        [DataType(DataType.Date)]
        public DateTime DataReserva { get; set; }

        public bool isDeleted { get; set; } = false;



        public event Action<Pacote_Turistico> CapacityReached;

        public Reserva()
        {

        }


        public void ExecutarReserva()
        {

            if (Cliente == null || PacoteTuristico == null)
            {
                throw new InvalidOperationException("Cliente ou PacoteTuristico não foram inicializados.");
            }

            string MensagemCapacidadeMaxima = $"Capacidade máxima atingida para o pacote {PacoteTuristico.Nome}, cliente {Cliente.ClienteNome}.";
            string MensagemDataAFrente = $"Evento do pacote {PacoteTuristico.Nome} já ocorreu, reserva indisponível, cliente {Cliente.ClienteNome}.";
            string MensagemReservaEfetuada = $"Reserva efetuada para o pacote {PacoteTuristico.Nome}, cliente {Cliente.ClienteNome}.";

            if (PacoteTuristico.ReservasAtuais + 1 > PacoteTuristico.CapacidadeMaxima)
            {
                Logger.LogToConsole(MensagemCapacidadeMaxima);
                Logger.LogToFile(MensagemCapacidadeMaxima);
                Logger.LogToMemory(MensagemCapacidadeMaxima);
                CapacityReached?.Invoke(PacoteTuristico);

            }
            else if (PacoteTuristico.DataInicio < DateTime.Now)
            {
                Logger.LogToConsole(MensagemDataAFrente);
                Logger.LogToFile(MensagemDataAFrente);
                Logger.LogToMemory(MensagemDataAFrente);
            }
            else
            {
                Logger.LogToConsole(MensagemReservaEfetuada);
                Logger.LogToFile(MensagemReservaEfetuada);
                Logger.LogToMemory(MensagemReservaEfetuada);
            }
        }
    }
}
