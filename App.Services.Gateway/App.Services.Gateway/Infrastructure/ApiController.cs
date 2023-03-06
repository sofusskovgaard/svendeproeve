using System.Linq.Expressions;
using System.Net;
using System.Security.Claims;
using App.Common.Grpc;
using Microsoft.AspNetCore.Mvc;

namespace App.Services.Gateway.Infrastructure;

public abstract class ApiController : ControllerBase
{
    protected CurrentUser? CurrentUser => User.HasClaim(claim => claim.Type == "id")
        ? new CurrentUser(User.FindFirst("id")!.Value,
            User.FindFirst(ClaimTypes.NameIdentifier)!.Value,
            User.FindFirst(ClaimTypes.Email)!.Value,
            bool.TryParse(User.FindFirst("isAdmin")?.Value, out var _res) && _res)
        : null;

    protected T CreateCommandMessage<T>(Action<T>? data = null) where T : IGrpcCommandMessage
    {
        var command = Activator.CreateInstance<T>();

        data?.Invoke(command);

        if (CurrentUser != null)
        {
            command.Metadata = new GrpcCommandMessageMetadata()
            {
                UserId = CurrentUser.Id,
                IsAdmin = CurrentUser.IsAdmin
            };
        }

        return command;
    }

    /// <summary>
    ///     Try running a piece of synchronous business logic in a task or create a proper error response and log error.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="func">The business logic to run</param>
    /// <returns></returns>
    protected Task<IActionResult> TryAsync<T>(Func<T> func, bool async = false) where T : IGrpcCommandResult, new()
    {
        return TryAsync(() => Task.Run(func), async);
    }

    /// <summary>
    ///     Try running a piece of asynchronous business logic or create a proper error response and log error.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="func">The business logic to run</param>
    /// <returns></returns>
    protected async Task<IActionResult> TryAsync<T>(Func<Task<T>> func, bool async = false) where T : IGrpcCommandResult, new()
    {
        try
        {
            var response = await func.Invoke();

            return async switch
            {
                true => Accepted(response),
                false => Ok(response)
            };
        }
        catch (Exception ex)
        {
            return HandleException<T>(ex);
        }
    }

    /// <summary>
    ///     Try running a piece of asynchronous business logic or create a proper error response and log error.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="func">The business logic to run</param>
    /// <returns></returns>
    protected async Task<IActionResult> TryAsync<T>(Func<ValueTask<T>> func, bool async = false) where T : IGrpcCommandResult, new()
    {
        try
        {
            var response = await func.Invoke();

            return (async, response) switch
            {
                (true, { Metadata.Success: true }) => Accepted(response),
                (false, { Metadata.Success: true }) => Ok(response),
                (_, { Metadata.Success: false }) => BadRequest(response)
            };
        }
        catch (Exception ex)
        {
            return HandleException<T>(ex);
        }
    }

    private IActionResult HandleException<T>(Exception ex)
        where T : IGrpcCommandResult, new()
    {
        return ex switch
        {
            AggregateException aex => new ObjectResult(new T
            {
                Metadata = new GrpcCommandResultMetadata
                {
                    Success = false,
                    Message = aex.Message,
                    Errors = aex.InnerExceptions.Select(e => e.Message).ToArray()
                }
            })
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            },
            _ => new ObjectResult(new T
            {
                Metadata = new GrpcCommandResultMetadata
                {
                    Success = false,
                    Message = ex.Message
                }
            })
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            }
        };
    }
}

public record CurrentUser(string Id, string Username, string Email, bool IsAdmin);