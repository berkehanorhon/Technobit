using System.ComponentModel.DataAnnotations;

namespace TechnoBit.DTOs;

    public class TokenDTO
    {
        [Required]
        public string AccessToken { get; set; }
        [Required]
        public string RefreshToken { get; set; }
    }
