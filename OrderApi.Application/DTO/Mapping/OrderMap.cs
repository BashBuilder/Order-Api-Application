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
                order.Id,
                order.ClientId,
                order.ProductId,
                order.PurchaseQuantity,
                order.OrderedDate
            );
        public static IEnumerable<OrderDTO> FromEntity(this IEnumerable<Order> orders) => orders.Select(order =>
            new OrderDTO (
                    order.Id,
                    order.ClientId,
                    order.ProductId,
                    order.PurchaseQuantity,
                    order.OrderedDate
                )
        );
    }

}
