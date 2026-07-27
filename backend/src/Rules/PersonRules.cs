namespace HouseholdExpenseManager.Api.Rules;

/// <summary>
/// Contém regras de negócio relacionadas a pessoas.
/// </summary>
public static class PersonRules
{
    private const int MinimumAdultAge = 18;

    /// <summary>
    /// Calcula a idade de uma pessoa com base na data de nascimento.
    /// Considera se o aniversário já ocorreu no ano atual.
    /// </summary>
    /// <param name="birthDate">Data de nascimento da pessoa.</param>
    /// <returns>A idade atual em anos completos.</returns>
    public static int CalculateAge(DateOnly birthDate)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        int age = today.Year - birthDate.Year;

        if (birthDate > today.AddYears(-age))
        {
            age--;
        }

        return age;
    }

    /// <summary>
    /// Verifica se a pessoa possui idade inferior à maioridade definida pela aplicação.
    /// </summary>
    /// <param name="birthDate">Data de nascimento da pessoa.</param>
    /// <returns>
    /// Verdadeiro quando a pessoa é menor de idade; caso contrário, falso.
    /// </returns>
    public static bool IsMinor(DateOnly birthDate)
    {
        return CalculateAge(birthDate) < MinimumAdultAge;
    }
}
