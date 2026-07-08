using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OrderApi.Application.DTO
{
    public record class OrderDetailsDTO
        (
            [Required] int OrderId,
            [Required] int ProductId,
            [Required] int Client,
            [Required] string ClientName,
            [Required, EmailAddress] string Email,
            [Required] string Address,
            [Required] string TelephoneNumber,
            [Required] string ProductName,
            [Required] int PurchaseQuantity,
            [Required, DataType(DataType.Currency)] decimal UnitPrice,
            [Required, DataType(DataType.Currency)] decimal TotalPrice,
            [Required] DateTime OrderedDate
         );

}
