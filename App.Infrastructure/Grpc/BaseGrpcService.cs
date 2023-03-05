using App.Common.Grpc;

namespace App.Infrastructure.Grpc;

public abstract class BaseGrpcService
{
    /// <summary>
    /// Try running a piece of synchronous business logic in a task or create a proper error response and log error.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="func">The business logic to run</param>
    /// <returns></returns>
    //protected ValueTask<T> TryAsync<T>(Func<T> func) where T : IGrpcCommandResult
    //{
    //    return this.TryAsync(() => );
    //}

    /// <summary>
    /// Try running a piece of asynchronous business logic or create a proper error response and log error.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="func">The business logic to run</param>
    /// <returns></returns>
    protected async ValueTask<T> TryAsync<T>(Func<ValueTask<T>> func) where T : IGrpcCommandResult
    {
        try
        {
            var response = await func();
            return response;
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

            return response;
        }
        catch (Exception ex)
        {
            var response = Activator.CreateInstance<T>();

            response.Metadata = new GrpcCommandResultMetadata()
            {
                Success = false,
                Message = ex.Message
            };

            return response;
        }
    }
}