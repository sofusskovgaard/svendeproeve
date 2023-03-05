using App.Services.Authentication.Infrastructure.Grpc.CommandMessages;
using App.Services.Authentication.Infrastructure.Grpc.CommandResults;
using ProtoBuf.Grpc.Configuration;

namespace App.Services.Authentication.Infrastructure.Grpc
{
    [Service("app.services.authentication")]
    public interface IAuthenticationGrpcService
    {
        /// <summary>
        /// This command allows the registering of new users in the system. It does <b>NOT</b> create a new user session.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [Operation]
        ValueTask<RegisterGrpcCommandResult> Register(RegisterGrpcCommandMessage message);

        /// <summary>
        /// This command allows a user to login. A user session will be created and they will receive an access token and a refresh token.
        /// </summary>
        /// <param name="message">the data required</param>
        /// <returns></returns>
        [Operation]
        ValueTask<LoginGrpcCommandResult> Login(LoginGrpcCommandMessage message);

        [Operation]
        ValueTask<RefreshTokenGrpcCommandResult> RefreshToken(RefreshTokenGrpcCommandMessage message);

        /// <summary>
        /// This command kills user sessions by removing refresh tokens from the database, effectively invalidating sessions when their current access token expires.
        /// </summary>
        /// <param name="message">the data required</param>
        /// <returns></returns>
        [Operation]
        ValueTask<KillUserSessionsGrpcCommandResult> KillSessions(KillUserSessionsGrpcCommandMessage message);

        /// <summary>
        /// This command allows checking for username availability, typically used when a user is looking for a new username or registering.
        /// </summary>
        /// <param name="message">the data required</param>
        /// <returns></returns>
        [Operation]
        ValueTask<CheckUsernameAvailabilityGrpcCommandResult> CheckUsernameAvailability(CheckUsernameAvailabilityGrpcCommandMessage message);

        /// <summary>
        /// This command allows checking for email availability, typically used when a user is changing their email or registering.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [Operation]
        ValueTask<CheckEmailAvailabilityGrpcCommandResult> CheckEmailAvailability(CheckEmailAvailabilityGrpcCommandMessage message);

        /// <summary>
        /// This command allows the user to change their username.
        /// </summary>
        /// <param name="message">the data required</param>
        /// <returns></returns>
        [Operation]
        ValueTask<ChangeUsernameGrpcCommandResult> ChangeUsername(ChangeUsernameGrpcCommandMessage message);

        /// <summary>
        /// This command allows the user to change their email.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [Operation]
        ValueTask<ChangeEmailGrpcCommandResult> ChangeEmail(ChangeEmailGrpcCommandMessage message);

        /// <summary>
        /// This command allows the user to change their password.
        /// </summary>
        /// <param name="message">the data required</param>
        /// <returns></returns>
        [Operation]
        ValueTask<ChangePasswordGrpcCommandResult> ChangePassword(ChangePasswordGrpcCommandMessage message);

        [Operation]
        ValueTask<GetPublicKeyGrpcCommandResult> PublicKey(GetPublicKeyGrpcCommandMessage message);
    }
}