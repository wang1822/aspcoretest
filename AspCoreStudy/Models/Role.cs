using Microsoft.AspNetCore.Mvc;

namespace AspCoreStudy.Models
{
    [ModelMetadataType(typeof(RoleMetadata))]
    public partial class Role
    {
        public Role()
        {
            Permissions = new HashSet<Permission>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
