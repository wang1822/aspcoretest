using Microsoft.AspNetCore.Mvc;

namespace AspCoreStudy.Models
{
    [ModelMetadataType(typeof(PermissionMetadata))]
    public partial class Permission
    {
        public Permission()
        {
            Roles = new HashSet<Role>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}
