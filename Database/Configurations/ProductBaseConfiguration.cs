using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MediaExpert.Domain.Products;

namespace MediaExpert.Database.Configurations
{
    /// <summary>
    /// Bazowa konfiguracja produktu na zapis w bazie danych.
    /// </summary>
    internal sealed class ProductBaseConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // Ustawienie klucza głównego
            builder.HasKey(p => p.Id);

            // Konfiguracja właściwości Id jako Guid
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd(); // Guid jest generowany automatycznie przy dodawaniu rekordu

            // Mapowanie do tabeli
            builder.ToTable("Product");
        }
    }
}
