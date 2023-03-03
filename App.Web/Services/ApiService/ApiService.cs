using System.Net.Http.Headers;
using System.Net;
using System.Net.Http.Json;
using App.Services.Authentication.Infrastructure.Grpc.CommandMessages;
using App.Services.Authentication.Infrastructure.Grpc.CommandResults;
using App.Services.Users.Infrastructure.Grpc.CommandMessages;
using App.Services.Users.Infrastructure.Grpc.CommandResults;
using App.Web.Stores;
using System.Diagnostics.CodeAnalysis;

namespace App.Web.Services.ApiService;

public partial class ApiService : IApiService
{
    private readonly HttpClient _client;

    private readonly ITokenStore _tokenStore;

    public ApiService(HttpClient client, ITokenStore tokenStore)
    {
        _client = client;
        _tokenStore = tokenStore;
    }

    private HttpRequestMessage _createRequestMessage(HttpMethod method, [StringSyntax(StringSyntaxAttribute.Uri)] string requestUri)
    {
        var requestMessage = new HttpRequestMessage(method, requestUri);

        if (!string.IsNullOrEmpty(_tokenStore.AccessToken))
        {
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _tokenStore.AccessToken);
        }

        return requestMessage;
    }
}

public partial interface IApiService
{
    
}
