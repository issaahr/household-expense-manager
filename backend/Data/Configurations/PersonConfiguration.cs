using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HouseholdExpenseManager.Api.Entities;

namespace HouseholdExpenseManager.Api.Data.Configurations;

/// <summary>
/// Mapeamento da entidade Person para a tabela do banco.
/// Define restrições de coluna e o relacionamento com Transaction.
/// </summary>
public sealed class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.Property(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(p => p.BirthDate)
            .IsRequired()
            .HasColumnType("date"); // Armazena apenas a data, sem hora.

        builder.Property(p => p.CreatedAt)
            .IsRequired();

        // Relacionamento 1:N — uma Person possui várias Transactions.
        // Cascade: ao deletar a pessoa, todas as suas transações são
        // apagadas automaticamente pelo banco.
        builder.HasMany(p => p.Transactions)
            .WithOne(t => t.Person)
            .HasForeignKey(t => t.PersonId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
