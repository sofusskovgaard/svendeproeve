using System.Net;
using App.Infrastructure.Grpc;
using Microsoft.AspNetCore.Mvc;

namespace App.Services.Gateway.Infrastructure;

public abstract class ApiController : ControllerBase
{
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