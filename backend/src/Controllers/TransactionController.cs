using HouseholdExpenseManager.Api.DTOs.Transaction.Request;
using HouseholdExpenseManager.Api.DTOs.Transaction.Response;
using HouseholdExpenseManager.Api.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace HouseholdExpenseManager.Api.Controllers;

/// <summary>
/// Gerencia operações relacionadas às transações.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public sealed class TransactionController(
    ITransactionService service
) : ControllerBase
{
    /// <summary>
    /// Cria uma nova transação.
    /// </summary>
    /// <param name="request">Dados da transação.</param>
    /// <returns>A transação criada.</returns>
    [HttpPost]
    [ProducesResponseType(
        typeof(TransactionResponse),
        StatusCodes.Status201Created
    )]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create(
        CreateTransactionRequest request)
    {
        TransactionResponse response =
            await service.CreateAsync(request);

        return StatusCode(
            StatusCodes.Status201Created,
            response
        );
    }


    /// <summary>
    /// Lista todas as transações cadastradas.
    /// </summary>
    /// <returns>Lista de transações.</returns>
    [HttpGet]
    [ProducesResponseType(
        typeof(IReadOnlyList<TransactionResponse>),
        StatusCodes.Status200OK
    )]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        IReadOnlyList<TransactionResponse> response =
            await service.GetAllAsync();

        return Ok(response);
    }


    /// <summary>
    /// Lista todas as transações de uma pessoa específica.
    /// </summary>
    /// <param name="personId">Identificador da pessoa.</param>
    /// <returns>Lista de transações vinculadas à pessoa.</returns>
    [HttpGet("person/{personId:int}")]
    [ProducesResponseType(
        typeof(IReadOnlyList<TransactionResponse>),
        StatusCodes.Status200OK
    )]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByPerson(
        int personId)
    {
        IReadOnlyList<TransactionResponse> response =
            await service.GetByPersonAsync(personId);

        return Ok(response);
    }
}
