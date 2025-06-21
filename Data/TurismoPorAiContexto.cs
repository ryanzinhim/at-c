using Microsoft.EntityFrameworkCore;
using EXPEXturism.Model;

namespace EXPEXturism.Data
{
    public class EXPEXturismContexto : DbContext
    {
        public EXPEXturismContexto()
        {
        }

        public EXPEXturismContexto(DbContextOptions<EXPEXturismContexto> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Reserva> Reservas { get; set; } = null!;
        public virtual DbSet<Destino> Destinos { get; set; } = null!;
        public virtual DbSet<Pacote_Turistico> Pacotes_Turisticos { get; set; } = null!;
        public virtual DbSet<PacoteDestino> PacoteDestinos { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PacoteDestino>()
                .HasKey(pd => new { pd.PacoteId, pd.DestinoId });

            modelBuilder.Entity<PacoteDestino>()
                .HasOne(pd => pd.Pacote)
                .WithMany(p => p.PacoteDestinos)
                .HasForeignKey(pd => pd.PacoteId);

            modelBuilder.Entity<PacoteDestino>()
                .HasOne(pd => pd.Destino)
                .WithMany(d => d.PacoteDestinos)
                .HasForeignKey(pd => pd.DestinoId);
        }
    }
}
