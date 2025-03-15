using Microsoft.AspNetCore.Mvc;

namespace AspCoreStudy.Models
{
    [ModelMetadataType(typeof(UserMetadata))]
    public partial class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
