using System.ComponentModel.DataAnnotations;

namespace Api.RequestModels
{
    public class FounderAddRequest
    {
        [Required]
        public long Itn {  get; set; }

        [Required]
        public string Fullname { get; set; } = null!;
    }
}
