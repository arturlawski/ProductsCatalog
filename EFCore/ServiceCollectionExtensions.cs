using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Reflection;
using MediaExpert;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Rozszerzenia dodające mechanizm persystancji obiektów wykorzystując rozwiązanie "EntityFrameworkCore".
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Dodaj mechanizm persystancji obiektów wykorzystując rozwiązanie "EntityFrameworkCore".
        /// </summary>
        /// <typeparam name="TContext">Typ kontekstu.</typeparam>
        /// <param name="services">Kolekcja usług, dla których wołające "Assembly" ma być zarejestrowane.</param>
        /// <param name="configuration">Konfiguracja.</param>
        /// <param name="assemblyToScan">"Assembly" do przeskanowania.</param>
        /// <param name="optionsAction">Konfiguracja EF</param>
        /// <param name="sqlServerOptionsAction">Dodatkowa konfiguracja dla SQL Server</param>
        /// <returns>Zwraca kolekcję zarejestrowanych usług.</returns>
        public static DbContextBuilder AddEntityFrameworkCore<TContext>(
            this IServiceCollection services,
            IConfiguration configuration,
            Assembly assemblyToScan = null,
            Action<DbContextOptionsBuilder> optionsAction = null,
            Action<SqlServerDbContextOptionsBuilder> sqlServerOptionsAction = null)
            where TContext : DbContext
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            if (assemblyToScan is null)
            {
                assemblyToScan = Assembly.GetCallingAssembly();
            }
            var dbContextBuilder = new DbContextBuilder(services);
            var contextName = typeof(TContext).Name;
            services.AddDbContext<TContext>(options =>
            {
                optionsAction?.Invoke(options);

                options.UseInMemoryDatabase(contextName);
            });

            services.AddSingleton<ExtendedModelCreating>(dbContextBuilder.ExtendedModelCreating);

            services.AddTransient<DbContext>(sp => sp.GetRequiredService<TContext>());

            if (typeof(UnitOfWork).IsAssignableFrom(typeof(TContext)))
            {
                services.AddTransient(sp => (UnitOfWork)sp.GetRequiredService<TContext>());
            }
            services.AddTransient(typeof(IQueryable<>), typeof(DbNoTrackingQuery<>));

            return dbContextBuilder;
        }

        /// <summary>
        /// Dodaj mechanizm persystancji obiektów wykorzystując rozwiązanie "EntityFrameworkCore".
        /// </summary>
        /// <typeparam name="TContext">Typ kontekstu.</typeparam>
        /// <typeparam name="TUnitOfWork">Typ pod jakim kontekst będzie zarejetrowany jako "Unit of Work".</typeparam>
        /// <param name="services">Kolekcja usług, dla których wołające "Assembly" ma być zarejestrowane.</param>
        /// <param name="configuration">Konfiguracja.</param>
        /// <param name="assemblyToScan">"Assembly" do przeskanowania.</param>
        /// <param name="optionsAction">Konfiguracja EF</param>
        /// <param name="sqlServerOptionsAction">Dodatkowa konfiguracja dla SQL Server</param>
        /// <returns>Zwraca kolekcję zarejestrowanych usług.</returns>
        public static DbContextBuilder AddEntityFrameworkCore<TContext, TUnitOfWork>(
            this IServiceCollection services,
            IConfiguration configuration,
            Assembly assemblyToScan = null,
            Action<DbContextOptionsBuilder> optionsAction = null,
            Action<SqlServerDbContextOptionsBuilder> sqlServerOptionsAction = null)
            where TContext : DbContext, TUnitOfWork
            where TUnitOfWork : class, UnitOfWork
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            if (assemblyToScan is null)
            {
                assemblyToScan = Assembly.GetCallingAssembly();
            }

            services.AddScoped<TUnitOfWork>(sp => sp.GetRequiredService<TContext>());

            return services.AddEntityFrameworkCore<TContext>(configuration, assemblyToScan, optionsAction, sqlServerOptionsAction);
        }
    }
}
