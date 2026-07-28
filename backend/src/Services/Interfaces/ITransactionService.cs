using HouseholdExpenseManager.Api.DTOs.Transaction.Request;
using HouseholdExpenseManager.Api.DTOs.Transaction.Response;

namespace HouseholdExpenseManager.Api.Services.Interfaces;

/// <summary>
/// Define os casos de uso relacionados ao cadastro de transações.
/// </summary>
public interface ITransactionService
{
    /// <summary>
    /// Cria uma nova transação.
    /// </summary>
    Task<TransactionResponse> CreateAsync(CreateTransactionRequest request);

    /// <summary>
    /// Lista todas as transações cadastradas.
    /// </summary>
    Task<IReadOnlyList<TransactionResponse>> GetAllAsync();


    /// <summary>
    /// Lista todas as transações de uma pessoa.
    /// </summary>
    Task<IReadOnlyList<TransactionResponse>> GetByPersonAsync(int personId);
}
