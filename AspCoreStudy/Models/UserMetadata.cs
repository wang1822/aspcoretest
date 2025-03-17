using System.ComponentModel.DataAnnotations;

namespace AspCoreStudy.Models
{
  public class UserMetadata
  {

    public int Id { get; set; }

    [Required(ErrorMessage = "账号不能为空")]
    [StringLength(20, MinimumLength = 5, ErrorMessage = "账号长度必须在5到20个字符之间")]
    public string Username { get; set; } = null!;

    [Required(ErrorMessage = "密码不能为空")]
    [StringLength(20, MinimumLength = 5, ErrorMessage = "密码长度必须在5到20个字符之间")]
    public string PasswordHash { get; set; } = null!;

    [EmailAddress(ErrorMessage = "无效的电子邮件地址")]
    public string? Email { get; set; }

    public DateTime? CreatedAt { get; set; }
  }
}