using EXPEXturism.Model;

namespace EXPEXturism.Data
{
    public static class DbInitializer
    {
        public static void Inicializar(EXPEXturismContexto contexto)
        {
            contexto.Database.EnsureCreated();

            if (contexto.Clientes.Any() || contexto.Destinos.Any() || contexto.Pacotes_Turisticos.Any())
                return;
        }
    }
}
