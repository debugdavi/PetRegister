using Microsoft.EntityFrameworkCore;
using PetRegister.Domain.Entities;

namespace PetRegister.Infrastructure.Context
{
    /// <summary>
    /// DbContext é a classe principal do EF Core que gerencia a conexão com o banco.
    /// Cada DbSet representa uma tabela no banco de dados.
    /// </summary>
    public class PetRegisterContext : DbContext
    {
        // Construtor que recebe as opções de configuração (connection string, etc.)
        // Essas opções são injetadas automaticamente pelo container de DI
        public PetRegisterContext(DbContextOptions<PetRegisterContext> options)
            : base(options)
        {
        }

        // DbSet<Pet> = tabela "Pets" no banco de dados
        // Cada propriedade da classe Pet vira uma coluna
        public DbSet<Pet> Pets { get; set; }

        // OnModelCreating é onde você configura detalhes do mapeamento
        // Por exemplo: chaves primárias, índices, relacionamentos, etc.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Aplica todas as configurações de entidades que estiverem na pasta Configuration
            // (IEntityTypeConfiguration<T>)
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PetRegisterContext).Assembly);

            // Configuração básica da entidade Pet
            modelBuilder.Entity<Pet>(entity =>
            {
                // Define a tabela
                entity.ToTable("Pets");

                // Define a chave primária
                entity.HasKey(p => p.Id);

                // Configura propriedades
                entity.Property(p => p.Nome)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(p => p.Peso)
                    .HasPrecision(10, 2); // 10 dígitos, 2 decimais
            });
        }
    }
}
