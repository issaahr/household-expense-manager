using FluentValidation;
using HouseholdExpenseManager.Api.DTOs.Transaction.Request;

namespace HouseholdExpenseManager.Api.Validators.Transaction;

/// <summary>
/// Valida as regras de entrada para criação de uma transação.
/// </summary>
public sealed class CreateTransactionRequestValidator
    : AbstractValidator<CreateTransactionRequest>
{
    public CreateTransactionRequestValidator()
    {
        RuleFor(x => x.Description)
            .Must(description => !string.IsNullOrWhiteSpace(description))
            .WithMessage("A descrição é obrigatória.")
            .MinimumLength(2)
            .WithMessage("A descrição deve possuir pelo menos 2 caracteres.")
            .MaximumLength(500)
            .WithMessage("A descrição deve possuir no máximo 500 caracteres.");

        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage("O valor da transação deve ser positivo.");

        RuleFor(x => x.Type)
            .IsInEnum()
            .WithMessage("O tipo da transação é inválido.");

        RuleFor(x => x.PersonId)
            .GreaterThan(0)
            .WithMessage("O ID da pessoa deve ser um número positivo.");
    }
}
