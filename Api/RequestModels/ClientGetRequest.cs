using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Api.RequestModels
{
    public class ClientGetRequest
    {
        [Required]
        public Guid Id { get; set; }
    }
}
