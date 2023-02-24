using System.Net;
using App.Infrastructure.Grpc;
using Microsoft.AspNetCore.Mvc;

namespace App.Services.Gateway.Infrastructure;

public abstract class ApiController : ControllerBase
{
    /// <summary>
    /// Try running a piece of synchronous business logic in a task or create a proper error response and log error.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="func">The business logic to run</param>
    /// <returns></returns>
    protected ValueTask<IActionResult> TryAsync<T>(Func<T> func, bool async = false) where T : IGrpcCommandResult
    {
        return this.TryAsync(() => Task.Run(func), async);
    }

    /// <summary>
    /// Try running a piece of asynchronous business logic or create a proper error response and log error.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="func">The business logic to run</param>
    /// <returns></returns>
    protected async ValueTask<IActionResult> TryAsync<T>(Func<Task<T>> func, bool async = false) where T : IGrpcCommandResult
    {
        try
        {
            var response = await func.Invoke();

            return async switch
            {
                true => this.Accepted(response),
                false => this.Ok(response)
            };
        }
        catch (AggregateException ex)
        {
            var response = Activator.CreateInstance<T>();

            response.Metadata = new GrpcCommandResultMetadata()
            {
                Success = false,
                Message = ex.Message,
                Errors = ex.InnerExceptions.Select(e => e.Message).ToArray()
            };

            return new ObjectResult(response)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
        }
        catch (Exception ex)
        {
            var response = Activator.CreateInstance<T>();

            response.Metadata = new GrpcCommandResultMetadata()
            {
                Success = false,
                Message = ex.Message
            };

            return new ObjectResult(response)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
        }
    }
}