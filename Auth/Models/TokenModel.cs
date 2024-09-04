using System.ComponentModel.DataAnnotations;

namespace TechnoBit.Models;

    public class TokenModel
    {
        [Required]
        public string AccessToken { get; set; }
        [Required]
        public string RefreshToken { get; set; }
    }
