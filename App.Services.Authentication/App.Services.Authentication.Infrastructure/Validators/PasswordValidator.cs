using System.Text.RegularExpressions;

namespace App.Services.Authentication.Infrastructure.Validators;

public static class PasswordValidator
{
    public static void Validate(string password, string confirmPassword)
    {
        var exceptions = new List<Exception>();

        if (password != confirmPassword)
        {
            exceptions.Add(new Exception($"Passwords do not match"));
        }

        if (password.Length < 8)
        {
            exceptions.Add(new Exception($"{nameof(password)} should be at least 8 characters long"));
        }

        var atLeastOneUpperCaseLetter = new Regex("^(?=.*?[A-Z]).{0,}$");
        if (!atLeastOneUpperCaseLetter.IsMatch(password))
        {
            exceptions.Add(new Exception($"{nameof(password)} should have at least one upper case character"));
        }

        var atLeastOneLowerCaseLetter = new Regex("^(?=.*?[a-z]).{0,}$");
        if (!atLeastOneLowerCaseLetter.IsMatch(password))
        {
            exceptions.Add(
                new Exception($"{nameof(password)} should have at least one lower case character"));
        }

        var atLeastOneDigit = new Regex("^(?=.*?[0-9]).{0,}$");
        if (!atLeastOneDigit.IsMatch(password))
        {
            exceptions.Add(new Exception($"{nameof(password)} should have at least one digit"));
        }

        var atLeastOneSpecialCharacter = new Regex("^(?=.*?[#?!@$%^&*-]).{0,}$");
        if (!atLeastOneSpecialCharacter.IsMatch(password))
        {
            exceptions.Add(
                new Exception($"{nameof(password)} should have at least one special character"));
        }

        if (exceptions.Count > 0)
        {
            throw exceptions.Count > 1 ? new AggregateException(exceptions) : exceptions[0];
        }
    }
}