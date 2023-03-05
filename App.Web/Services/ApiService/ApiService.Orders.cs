using App.Services.Orders.Infrastructure.Grpc;
using App.Services.Orders.Infrastructure.Grpc.CommandMessages;
using App.Services.Orders.Infrastructure.Grpc.CommandResults;
using System.Net.Http.Json;
using App.Services.Gateway.Common;

namespace App.Web.Services.ApiService
{
    public partial class ApiService
    {
        public async Task<GetOrderByIdGrpcCommandResult> GetOrderById(string id)
        {
            var request = await _createRequestMessage(HttpMethod.Get, $"api/orders/{id}");

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<GetOrderByIdGrpcCommandResult>();
        }

        public async Task<GetProductsGrpcCommandResult> GetProducts()
        {
            var request = await _createRequestMessage(HttpMethod.Get, $"api/orders/products");
            
            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<GetProductsGrpcCommandResult>();
        }

        public async Task<GetProductByIdGrpcCommandResult> GetProductById(string id)
        {
            var request = await _createRequestMessage(HttpMethod.Get, $"api/orders/product/{id}");

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<GetProductByIdGrpcCommandResult>();
        }

        public async Task<CreateProductGrpcCommandResult> CreateProduct(CreateProductModel data)
        {
            var request = await _createRequestMessage(HttpMethod.Post, $"api/orders/product");
            request.Content = JsonContent.Create(data);

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<CreateProductGrpcCommandResult>();
        }
    }

    public partial interface IApiService
    {
        Task<GetOrderByIdGrpcCommandResult> GetOrderById(string id);

        Task<GetProductsGrpcCommandResult> GetProducts();

        Task<GetProductByIdGrpcCommandResult> GetProductById(string id);

        Task<CreateProductGrpcCommandResult> CreateProduct(CreateProductModel data);
    }
}
