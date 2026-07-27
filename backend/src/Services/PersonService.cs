using HouseholdExpenseManager.Api.Data;
using HouseholdExpenseManager.Api.DTOs.Person.Request;
using HouseholdExpenseManager.Api.DTOs.Person.Response;
using HouseholdExpenseManager.Api.Entities;
using HouseholdExpenseManager.Api.Services.Interfaces;
using HouseholdExpenseManager.Api.Rules;
using HouseholdExpenseManager.Api.Exceptions;

using Microsoft.EntityFrameworkCore;

namespace HouseholdExpenseManager.Api.Services;

/// <summary>
/// Responsável pelos casos de uso relacionados ao cadastro de pessoas.
/// </summary>
public sealed class PersonService(AppDbContext context) : IPersonService
{
    /// <summary>
    /// Cria uma nova pessoa.
    /// </summary>
    /// <param name="request">Dados da pessoa a ser criada.</param>
    /// <returns>A pessoa criada.</returns>
    public async Task<PersonResponse> CreateAsync(CreatePersonRequest request)
    {
        Person person = new()
        {
            Name = request.Name.Trim(),
            BirthDate = request.BirthDate
        };

        context.People.Add(person);

        await context.SaveChangesAsync();

        return MapToResponse(person);
    }

    /// <summary>
    /// Retorna todas as pessoas cadastradas.
    /// </summary>
    /// <returns>Lista de pessoas cadastradas.</returns>
    public async Task<IReadOnlyList<PersonResponse>> GetAllAsync()
    {
        List<Person> people = await context.People
            .AsNoTracking()
            .ToListAsync();

        return people
            .Select(MapToResponse)
            .ToList();
    }

    /// <summary>
    /// Remove uma pessoa pelo identificador.
    /// As transações relacionadas são removidas automaticamente
    /// pelo relacionamento Cascade configurado no Entity Framework.
    /// </summary>
    /// <param name="id">Identificador da pessoa.</param>
    public async Task DeleteAsync(int id)
    {
        Person person = await context.People
            .FirstOrDefaultAsync(p => p.Id == id)
            ?? throw new NotFoundException(
                "Pessoa não encontrada."
            );

        context.People.Remove(person);

        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Converte uma entidade de domínio para o DTO de resposta.
    /// </summary>
    private static PersonResponse MapToResponse(Person person)
    {
        return new PersonResponse
        {
            Id = person.Id,
            Name = person.Name,
            Age = PersonRules.CalculateAge(person.BirthDate),
            CreatedAt = person.CreatedAt
        };
    }
}
