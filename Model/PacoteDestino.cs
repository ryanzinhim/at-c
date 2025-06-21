namespace EXPEXturism.Model
{
    public class PacoteDestino
    {
        public int PacoteId { get; set; }
        public Pacote_Turistico Pacote { get; set; }

        public int DestinoId { get; set; }
        public Destino Destino { get; set; }
    }
}
