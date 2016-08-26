using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UsersList.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Введите Ваше имя")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Неправильный формат имени")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Длина строки должна быть от 6 до 50 символов")]
        [Display(Name = "Имя")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Введите адрес электронной почты")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        [Display(Name = "Email")]
        public string UserEmail { get; set; }
        
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Некорректный логин")]
        [Display(Name = "Skype логин")]
        public string SkypeLogin { get; set; }

        [Display(Name = "Подпись")]
        public string Signature { get; set; }

        [Display(Name = "Аватар")]
        public byte[] UserAvatar { get; set; }

        public string ImageMimeType { get; set; }
    }
}