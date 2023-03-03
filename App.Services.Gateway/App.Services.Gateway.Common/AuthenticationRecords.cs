namespace App.Services.Gateway.Common;

public record LoginModel(string Username, string Password);

public record RegisterModel(string Firstname, string Lastname, string Username, string Email, string Password, string ConfirmPassword);

public record RefreshTokenModel(string RefreshToken);

public record ChangeUsernameModel(string Username);

public record ChangeEmailModel(string Email);

public record ChangePasswordModel(string Password, string ConfirmPassword);