using System.ComponentModel.DataAnnotations;

namespace Inventra.WebUI.Dtos.ProfileDtos
{
    public class ChangePasswordDto
    {
        [Display(Name = "Mevcut Şifre")]
        [Required(ErrorMessage = "Mevcut şifre zorunludur.")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; } = string.Empty;

        [Display(Name = "Yeni Şifre")]
        [Required(ErrorMessage = "Yeni şifre zorunludur.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Yeni şifre en az 8 karakter olmalıdır.")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = string.Empty;

        [Display(Name = "Yeni Şifre (Tekrar)")]
        [Required(ErrorMessage = "Yeni şifre tekrarı zorunludur.")]
        [Compare(nameof(NewPassword), ErrorMessage = "Yeni şifreler eşleşmiyor.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
