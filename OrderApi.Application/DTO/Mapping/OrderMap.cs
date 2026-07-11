using OrderApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderApi.Application.DTO.Mapping
{
    public static class OrderMap
    {
        public static Order ToEntity(this OrderDTO order) => new()
        {
            Id = order.Id,
            ClientId = order.ClientId,
            ProductId = order.ProductId,
            OrderedDate = order.OrderedDate,
            PurchaseQuantity = order.PurchaseQuantity
        };
        public static OrderDTO FromEntity(this Order order) => new(
                Id: order.Id,
                ClientId: order.ClientId,
                ProductId: order.ProductId,
                PurchaseQuantity: order.PurchaseQuantity,
                OrderedDate: order.OrderedDate
            );

        public static IEnumerable<OrderDTO> FromEntity(this IEnumerable<Order> orders) => orders.Select(order =>
            new OrderDTO(
                    Id: order.Id,
                    ClientId: order.ClientId,
                    ProductId: order.ProductId,
                    PurchaseQuantity: order.PurchaseQuantity,
                    OrderedDate: order.OrderedDate
                )
        );
    }

}
