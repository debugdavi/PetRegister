using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetRegister.Infrastructure.Context;

namespace PetRegister.Infrastructure
{
    /// <summary>
    /// Classe estática que contém métodos de extensão para registrar 
    /// os serviços da camada de Infrastructure no container de DI.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Registra todos os serviços da Infrastructure (DbContext, Repositories, etc.)
        /// Este método é chamado no Program.cs da API.
        /// </summary>
        /// <param name="services">O container de serviços do ASP.NET Core</param>
        /// <param name="configuration">A configuração da aplicação (appsettings.json)</param>
        /// <returns>O mesmo container para permitir encadeamento</returns>
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // 1. Registra o DbContext
            // AddDbContext registra o contexto como "Scoped" (uma instância por requisição HTTP)
            services.AddDbContext<PetRegisterContext>(options =>
            {
                // Pega a connection string do appsettings.json
                var connectionString = configuration.GetConnectionString("DefaultConnection");

                // Configura para usar PostgreSQL (Npgsql)
                options.UseNpgsql(connectionString);
            });

            // 2. Aqui você vai registrar os Repositories futuramente
            // Exemplo:
            // services.AddScoped<IPetRepository, PetRepository>();

            // 3. Aqui você vai registrar outros serviços da infra
            // Exemplo:
            // services.AddScoped<ICacheService, RedisCacheService>();

            return services;
        }
    }
}
