using DDinst.Base.DAL.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DDinst.Models
{
    public class ProfileModel
    {
        [Display(Name = "Ник:")]
        public string Nickname { get; set; }
        [Display(Name = "Почта:")]
        public string Email { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        [Display(Name = "Дата рождения:")]
        public DateTime Birth { get; set; }

        public int Avatar_ID { get; set; }
        public string Avatar_Name { get; set; }

        public List<Post> Posts { get; set; }
        public List<Image> images { get; set; }
    }
}