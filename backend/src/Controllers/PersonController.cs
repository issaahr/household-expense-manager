using HouseholdExpenseManager.Api.DTOs.Person.Request;
using HouseholdExpenseManager.Api.DTOs.Person.Response;
using HouseholdExpenseManager.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HouseholdExpenseManager.Api.Controllers;

/// <summary>
/// Gerencia operações relacionadas às pessoas.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public sealed class PersonController(IPersonService service) : ControllerBase
{
    /// <summary>
    /// Cria uma nova pessoa.
    /// </summary>
    /// <param name="request">Dados da pessoa.</param>
    /// <returns>A pessoa criada.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(PersonResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create(
        CreatePersonRequest request)
    {
        PersonResponse response = await service.CreateAsync(request);

        return CreatedAtAction(
            nameof(Create),
            new { id = response.Id },
            response
        );
    }

    /// <summary>
    /// Retorna todas as pessoas cadastradas.
    /// </summary>
    /// <returns>Lista de pessoas.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<PersonResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        IReadOnlyList<PersonResponse> response =
            await service.GetAllAsync();

        return Ok(response);
    }

    /// <summary>
    /// Remove uma pessoa pelo identificador.
    /// </summary>
    /// <param name="id">Identificador da pessoa.</param>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        await service.DeleteAsync(id);

        return NoContent();
    }
}
