﻿namespace App.Infrastructure.Grpc;

public abstract class BaseGrpcService
{
    /// <summary>
    /// Try running a piece of synchronous business logic in a task or create a proper error response and log error.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="func">The business logic to run</param>
    /// <returns></returns>
    protected Task<T> TryAsync<T>(Func<T> func) where T : IGrpcCommandResult
    {
        return this.TryAsync(() => Task.Run(func));
    }

    /// <summary>
    /// Try running a piece of asynchronous business logic or create a proper error response and log error.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="func">The business logic to run</param>
    /// <returns></returns>
    protected async Task<T> TryAsync<T>(Func<Task<T>> func) where T : IGrpcCommandResult
    {
        try
        {
            return await func.Invoke();
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