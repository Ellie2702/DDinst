using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DDinst.Base.DAL.Entity
{
    public class User
    {

        public User()
        {
            PostImages = new HashSet<PostImage>();
            Posts = new HashSet<Post>();
            Avatars = new HashSet<Avatar>();
        }
        [Key]
        public int User_Id { get; set; }
        [StringLength(50)]
        public string Nickname { get; set; }
        [StringLength(150)]
        public string Email { get; set; }
        public DateTime Birth { get; set; }
        public DateTime RegDate { get; set; }
        [StringLength(100)]
        public string PassHash { get; set; }
        [StringLength(50)]
        public string Salt { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<PostImage> PostImages { get; set; }
        public virtual ICollection<Avatar> Avatars { get; set; }
    }
}