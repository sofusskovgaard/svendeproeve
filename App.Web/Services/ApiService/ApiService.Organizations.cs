using App.Services.Organizations.Infrastructure.Grpc.CommandMessages;
using App.Services.Organizations.Infrastructure.Grpc.CommandResults;
using System.Net.Http.Json;
using App.Services.Gateway.Common;

namespace App.Web.Services.ApiService
{
    public partial class ApiService
    {
        public async Task<GetOrganizationByIdGrpcCommandResult> GetOrganizationById(string id)
        {
            var request = await _createRequestMessage(HttpMethod.Get, $"api/organization/{id}");

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<GetOrganizationByIdGrpcCommandResult>();
        }

        public async Task<GetOrganizationsByNameGrpcCommandResult> GetOrganizationsByName(string name)
        {
            var request = await _createRequestMessage(HttpMethod.Get, $"api/organization/{name}/name");

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<GetOrganizationsByNameGrpcCommandResult>();
        }

        public async Task<GetOrganizationsByAddressGrpcCommandResult> GetOrganizationsByAddress(string address)
        {
            var request = await _createRequestMessage(HttpMethod.Get, $"api/organization/{address}/address");

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<GetOrganizationsByAddressGrpcCommandResult>();
        }

        public async Task<GetOrganizationsGrpcCommandResult> GetOrganizations()
        {
            var request = await _createRequestMessage(HttpMethod.Get, "api/organizations");

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<GetOrganizationsGrpcCommandResult>();
        }

        public async Task<CreateOrganizationGrpcCommandResult> CreateOrganization(CreateOrganizationModel data)
        {
            var request = await _createRequestMessage(HttpMethod.Post, "api/organizations");
            request.Content = JsonContent.Create(data);

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<CreateOrganizationGrpcCommandResult>();
        }

        public async Task<UpdateOrganizationGrpcCommandResult> UpdateOrganization(string id, UpdateOrganizaitonModel data)
        {
            var request = await _createRequestMessage(HttpMethod.Put, $"api/organizations/{id}");
            request.Content = JsonContent.Create(data);

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<UpdateOrganizationGrpcCommandResult>();
        }

        public async Task<DeleteOrganizationGrpcCommandResult> DeleteOrganization(string id)
        {
            var request = await _createRequestMessage(HttpMethod.Delete, $"api/organization/{id}");

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<DeleteOrganizationGrpcCommandResult>();
        }
    }

    public partial interface IApiService
    {
        Task<GetOrganizationByIdGrpcCommandResult> GetOrganizationById(string id);

        Task<GetOrganizationsByNameGrpcCommandResult> GetOrganizationsByName(string name);

        Task<GetOrganizationsByAddressGrpcCommandResult> GetOrganizationsByAddress(string address);

        Task<GetOrganizationsGrpcCommandResult> GetOrganizations();

        Task<CreateOrganizationGrpcCommandResult> CreateOrganization(CreateOrganizationModel data);

        Task<UpdateOrganizationGrpcCommandResult> UpdateOrganization(string id, UpdateOrganizaitonModel data);

        Task<DeleteOrganizationGrpcCommandResult> DeleteOrganization(string id);

    }
}
