

using System.ComponentModel.DataAnnotations;

namespace OrderApi.Application.DTO
{
    public record class AppUserDTO
        (
            int Id,
            [Required] string Name,
            [Required] string TelephoneNumber,
            [Required, EmailAddress] string Email,
            [Required] string Address,
            [Required] string Password,
            [Required] string Role
        );
}
