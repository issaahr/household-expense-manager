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
}
