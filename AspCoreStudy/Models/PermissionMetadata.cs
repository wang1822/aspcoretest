using System.ComponentModel.DataAnnotations;

namespace AspCoreStudy.Models
{
    public class PermissionMetadata
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "权限名称不能为空")]
        [MaxLength(50, ErrorMessage = "权限名称长度不能超过50个字符")]
        public string Name { get; set; } = null!;

        [MaxLength(200, ErrorMessage = "描述长度不能超过200个字符")]
        public string? Description { get; set; }
    }
}