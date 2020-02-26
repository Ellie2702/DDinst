using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DDinst.Models
{
    public class UserLogin
    {
        [Required]
        [Display(Name = "Почта:")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль:")]
        public string PassHash { get; set; }
    }
}