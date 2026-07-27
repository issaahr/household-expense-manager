using HouseholdExpenseManager.Api.DTOs.Person.Request;
using HouseholdExpenseManager.Api.DTOs.Person.Response;

namespace HouseholdExpenseManager.Api.Services.Interfaces;

/// <summary>
/// Define os casos de uso relacionados ao cadastro de pessoas.
/// </summary>
public interface IPersonService
{
    /// <summary>
    /// Cria uma nova pessoa.
    /// </summary>
    /// <param name="request">Dados da pessoa.</param>
    /// <returns>A pessoa criada.</returns>
    Task<PersonResponse> CreateAsync(CreatePersonRequest request);

    /// <summary>
    /// Lista todas as pessoas cadastradas.
    /// </summary>
    /// <returns>Lista de pessoas cadastradas.</returns>
    Task<IReadOnlyList<PersonResponse>> GetAllAsync();

    /// <summary>
    /// Retorna o resumo financeiro das pessoas cadastradas.
    /// </summary>
    /// <returns>
    /// Lista de pessoas com seus totais financeiros e os totais gerais.
    /// </returns>
    Task<PersonFinancialSummaryResponse> GetFinancialSummaryAsync();

    /// <summary>
    /// Remove uma pessoa pelo identificador.
    /// </summary>
    /// <param name="id">Identificador da pessoa.</param>
    Task DeleteAsync(int id);
}
