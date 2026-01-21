using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetRegister.Infrastructure.Caching;
using PetRegister.Infrastructure.Context;
using PetRegister.Infrastructure.Repositories;
using PetRegister.Infrastructure.Repositories.Interfaces;
using PetRegister.Infrastructure.Services;

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

            // 2. Registra os Repositories
            services.AddScoped<IPetRepository, PetRepository>();

            // 3. Registra o Redis Cache
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration["Redis:Configuration"];
                options.InstanceName = configuration["Redis:InstanceName"];
            });

            // 4. Registra o serviço de caching
            services.AddScoped<ICachingService, CachingService>();

            return services;
        }
    }
}
