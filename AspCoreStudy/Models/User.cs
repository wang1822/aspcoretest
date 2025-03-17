using Microsoft.AspNetCore.Mvc;

namespace AspCoreStudy.Models
{
    [ModelMetadataType(typeof(UserMetadata))]
    public partial class User
    {
        public User()
        {
            Roles = new HashSet<Role>();
        }

        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string? Email { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}
