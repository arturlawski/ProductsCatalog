using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace MediaExpert
{
    /// <summary>
    /// Delegat rozszerzający tworzenie modelu
    /// </summary>
    /// <param name="modelBuilder">Budowniczy modelu <see cref="ModelBuilder"/></param>
    public delegate void ExtendedModelCreating(ModelBuilder modelBuilder);

    /// <summary>
    /// Budowniczy kontekstu bazy danych
    /// </summary>
    public class DbContextBuilder
    {
        private IList<Action<ModelBuilder>> _modelCreatingExtendedActions = new List<Action<ModelBuilder>>();

        /// <summary>
        /// Inicjalizuje nową instancję <see cref="DbContextBuilder"/>
        /// </summary>
        /// <param name="services">Zarejestrowane usługi <see cref="IServiceCollection"/></param>
        public DbContextBuilder(IServiceCollection services)
        {
            Services = services;
        }

        /// <summary>
        /// Zarejestrowane usługi
        /// </summary>
        public IServiceCollection Services { get; }

        /// <summary>
        /// Z rozszerzonym tworzeniem modelu
        /// </summary>
        /// <param name="extendedModelCreating">Akcja rozszerzająca tworzenie modelu</param>
        public DbContextBuilder WithExtendedModelCreating(Action<ModelBuilder> extendedModelCreating)
        {
            _modelCreatingExtendedActions.Add(extendedModelCreating);

            return this;
        }

        internal void ExtendedModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var modelCreatingExtendedAction in _modelCreatingExtendedActions)
            {
                modelCreatingExtendedAction(modelBuilder);
            }
        }
    }
}
