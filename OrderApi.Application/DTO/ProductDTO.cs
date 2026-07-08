using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OrderApi.Application.DTO
{
    public record class ProductDTO(
            int Id,
            [Required] string Name,
            [Required, Range(1, int.MaxValue)] int Quantity,
            [Required, DataType(DataType.Currency), Range(0.01, double.MaxValue)] decimal Price
        );
}
