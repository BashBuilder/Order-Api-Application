using OrderApi.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderApi.Application.Services
{
    internal interface IOrderService
    {
        Task<IEnumerable<OrderDTO>> GetOrderByClientId(int clientId);
        Task<OrderDetailsDTO> GetOrderDetails(int orderId);
    }
}
