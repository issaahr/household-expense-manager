using FluentValidation;
using HouseholdExpenseManager.Api.DTOs.Person.Request;

namespace HouseholdExpenseManager.Api.Validators.Person;

/// <summary>
/// Validador responsável pelas regras de entrada para criação de uma pessoa.
/// </summary>
public sealed class CreatePersonRequestValidator
    : AbstractValidator<CreatePersonRequest>
{
    private const int MaxAge = 150;

    public CreatePersonRequestValidator()
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Today);

        RuleFor(x => x.Name)
            .Must(name => !string.IsNullOrWhiteSpace(name))
            .WithMessage("O nome é obrigatório.")
            .MinimumLength(2)
            .WithMessage("O nome deve possuir pelo menos 2 caracteres.")
            .MaximumLength(150)
            .WithMessage("O nome deve possuir no máximo 150 caracteres.");

        RuleFor(x => x.BirthDate)
            .LessThan(today)
            .WithMessage("A data de nascimento deve ser anterior à data atual.")
            .GreaterThan(today.AddYears(-MaxAge))
            .WithMessage($"A idade não pode ser superior a {MaxAge} anos.");
    }
}
