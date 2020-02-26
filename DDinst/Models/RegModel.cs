using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DDinst.Models
{
    public class RegModel
    {
        [Required]
        [Display(Name = "Почта:")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Ник:")]
        public string Nickname { get; set; }
        [Required]
        [Display(Name = "Дата рождения:")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime Birth { get; set; } 
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Придумайте пароль:")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Повторите пароль:")]
        [Compare(nameof(Password), ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string RetryPassword { get; set; }

    }
}