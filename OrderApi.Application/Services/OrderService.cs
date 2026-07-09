using OrderApi.Application.DTO;
using OrderApi.Application.DTO.Mapping;
using OrderApi.Application.Interfaces;
using Polly.Registry;
using System.Net.Http.Json;

namespace OrderApi.Application.Services
{
    public class OrderService(
            IOrder orderInterface,
            HttpClient httpClient,
            ResiliencePipelineProvider<string> resiliencePipeline
        ) : IOrderService
    {
        //get product from the product api
        public async Task<ProductDTO> GetProduct(int productId)
        {
            var getProduct = await httpClient.GetAsync($"/api/products/{productId}");
            if (!getProduct.IsSuccessStatusCode) return null!;

            var product = await getProduct.Content.ReadFromJsonAsync<ProductDTO>();

            return product!;
        } 
        
        //get user from the user api
        public async Task<AppUserDTO> GetUser(int userId)
        {
            var getProduct = await httpClient.GetAsync($"/api/product/{userId}");
            if (!getProduct.IsSuccessStatusCode) return null!;

            var user = await getProduct.Content.ReadFromJsonAsync<AppUserDTO>();

            return user!;
        }


        public async Task<IEnumerable<OrderDTO>> GetOrderByClientId(int clientId)
        {
            var orders = await orderInterface.GetOrdersAsync(order => order.ClientId == clientId);
            if (!orders.Any()) return null!;

            return orders.FromEntity();
        }

        public async Task<OrderDetailsDTO> GetOrderDetails(int orderId)
        {
            var order = await orderInterface.FindByIdAsync(orderId);
            if (order is null || order.Id <= 0) return null!;

            var retryPipeline = resiliencePipeline.GetPipeline("my-retry-pipeline");

            var productDTO = await retryPipeline.ExecuteAsync(async token => await GetProduct(order.ProductId));

            var appUserDTO = await retryPipeline.ExecuteAsync(async token => await GetUser(order.ClientId));

            return new OrderDetailsDTO(
                    order.Id,
                    productDTO.Id,
                    appUserDTO.Id,
                    appUserDTO.Name,
                    appUserDTO.Email,
                    appUserDTO.Address,
                    appUserDTO.TelephoneNumber,
                    productDTO.Name,
                    order.PurchaseQuantity,
                    productDTO.Price,
                    productDTO.Quantity * order.PurchaseQuantity,
                    order.OrderedDate
                );

        }
    }
}
