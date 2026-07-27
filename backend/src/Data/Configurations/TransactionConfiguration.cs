using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HouseholdExpenseManager.Api.Entities;

namespace HouseholdExpenseManager.Api.Data.Configurations;

/// <summary>
/// Mapeamento da entidade Transaction para a tabela do banco.
/// </summary>
public sealed class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.Property(t => t.Id);

        builder.Property(t => t.Description)
            .IsRequired()
            .HasMaxLength(250);

        // Define precisão para valores monetários:
        // até 18 dígitos no total e 2 casas decimais. Para evitar truncamento ou arredondamento inesperado ao salvar no banco.
        builder.Property(t => t.Amount)
            .IsRequired()
            .HasPrecision(18, 2);

        // Persiste o enum como texto para melhorar legibilidade
        // dos dados diretamente no banco.
        builder.Property(t => t.Type)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(t => t.PersonId)
            .IsRequired();

        builder.Property(t => t.CreatedAt)
            .IsRequired();
    }
}
