using HouseholdExpenseManager.Api.Data;
using HouseholdExpenseManager.Api.DTOs.Transaction.Request;
using HouseholdExpenseManager.Api.DTOs.Transaction.Response;
using HouseholdExpenseManager.Api.Enums;
using HouseholdExpenseManager.Api.Entities;
using HouseholdExpenseManager.Api.Exceptions;
using HouseholdExpenseManager.Api.Rules;
using HouseholdExpenseManager.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HouseholdExpenseManager.Api.Services;

/// <summary>
/// Responsável pelos casos de uso relacionados às transações.
/// </summary>
public sealed class TransactionService(AppDbContext context)
    : ITransactionService
{
    /// <summary>
    /// Cria uma nova transação associada a uma pessoa.
    /// </summary>
    /// <param name="request">Dados da transação.</param>
    /// <returns>A transação criada.</returns>
    public async Task<TransactionResponse> CreateAsync(
        CreateTransactionRequest request)
    {
        Person person = await context.People
            .FirstOrDefaultAsync(p => p.Id == request.PersonId)
            ?? throw new NotFoundException(
                "Pessoa não encontrada."
            );


        if (
            PersonRules.IsMinor(person.BirthDate)
            && request.Type == TransactionType.Income
        )
        {
            throw new BusinessException(
                "Menores de idade podem cadastrar apenas despesas."
            );
        }


        Transaction transaction = new()
        {
            Description = request.Description.Trim(),
            Amount = request.Amount,
            Type = request.Type,
            PersonId = person.Id
        };


        context.Transactions.Add(transaction);

        await context.SaveChangesAsync();


        return MapToResponse(transaction);
    }


    /// <summary>
    /// Converte uma entidade para o DTO de resposta.
    /// </summary>
    private static TransactionResponse MapToResponse(
        Transaction transaction)
    {
        return new TransactionResponse
        {
            Id = transaction.Id,
            Description = transaction.Description,
            Amount = transaction.Amount,
            Type = transaction.Type,
            PersonId = transaction.PersonId
        };
    }
}
