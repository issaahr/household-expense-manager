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
    Task<PersonResponse> CreateAsync(CreatePersonRequest request);

    /// <summary>
    /// Lista todas as pessoas cadastradas.
    /// </summary>
    Task<IReadOnlyList<PersonResponse>> GetAllAsync();

    /// <summary>
    /// Remove uma pessoa pelo identificador.
    /// </summary>
    Task DeleteAsync(int id);
}
