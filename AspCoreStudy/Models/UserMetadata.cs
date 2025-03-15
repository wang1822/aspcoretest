using System.ComponentModel.DataAnnotations;

namespace AspCoreStudy.Models
{
    public class UserMetadata
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "账号不能为空")]
        [MaxLength(20, ErrorMessage = "账号长度不能超过20个字符")]
        public string Username { get; set; } = null!;
        [Required(ErrorMessage = "密码不能为空")]
        [MaxLength(20, ErrorMessage = "密码长度不能超过20个字符")]
        public string Password { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}