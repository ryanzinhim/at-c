namespace EXPEXturism.Model
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        public string ClienteNome { get; set; }
        public string ClienteEmail { get; set; }
        public string ClienteTelefone { get; set; }
        public List<Reserva> Reservas { get; set; } = new ();

        public Cliente()
        {

        }
        public Cliente(string nome, string email, string telefone)
        {
            this.ClienteNome = nome;
            this.ClienteEmail = email;
            this.ClienteTelefone = telefone;
        }
        public override string ToString()
        {
            return $"{ClienteId} {ClienteNome} ({ClienteEmail}, {ClienteTelefone})";
        }
    }
}
