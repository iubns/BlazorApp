using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Objects
{
    public class User
    {
        [Required]
        [StringLength(30, ErrorMessage = "Name is too long.")]
        public string ID { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public string session { get; set; } = string.Empty;

    }
}
