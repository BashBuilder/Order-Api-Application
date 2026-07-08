using Ecommerce.SharedLibrary.Responses;
using Microsoft.AspNetCore.Mvc;
using OrderApi.Application.DTO;
using OrderApi.Application.DTO.Mapping;
using OrderApi.Application.Interfaces;
using OrderApi.Application.Services;

namespace OrderApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(
            IOrder orderInterface, 
            IOrderService orderService
        ) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders()
        {
            var orders = await orderInterface.GetAllAsync();
            if (!orders.Any()) return NotFound("No order found");

            var list = orders.FromEntity();
            return !list.Any() ? NotFound("No orders found") : Ok(list);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrderDTO>> GetOrder(int id)
        {
            var order = await orderInterface.FindByIdAsync(id);

            return order is null ? NotFound("Order not found") : Ok(order);
        }

        [HttpGet("client/{clientId:Int}")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetClientOrders(int clientId)
        {
            if (clientId <= 0) return BadRequest("Invalid data");
            var orders = await orderService.GetOrderByClientId(clientId);
            return orders.Any() ? Ok(orders) : NotFound(null);
        }

        [HttpGet("details/{orderId:Int}")]
        public async Task<ActionResult<OrderDetailsDTO>> GetOrderDetails( int orderId)
        {
            if (orderId <= 0) return BadRequest("Invalid data provided");
            var orderDetail = await orderService.GetOrderDetails(orderId);

            return orderDetail.OrderId > 0 ? Ok(orderDetail) : NotFound("No order found");
        }


        [HttpPost]
        public async Task<ActionResult<Response>> CreateOrder(OrderDTO orderDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var getEntity = orderDto.ToEntity();
            var response = await orderInterface.CreateAsync(getEntity);

            return response.Flag ? Ok(Response) : BadRequest(response);
        }

        [HttpPut]
        public async Task<ActionResult<Response>> UpdateOrder( OrderDTO newOrder)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var order = newOrder.ToEntity();
            var response = await orderInterface.UpdateAsync(order);
            return response.Flag ? Ok(response) : BadRequest(response);
        }

        [HttpDelete]
        public async Task<ActionResult<Response>> DeleteOrders(OrderDTO orderDto)
        {
            var order = orderDto.ToEntity();
            var response = await orderInterface.DeleteAsync(order);
            return response.Flag ? Ok(response) : BadRequest(response);
        }



    }
}
