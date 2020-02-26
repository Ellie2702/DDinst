using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DDinst.Base.DAL.Entity
{
    public class Post
    {
        public Post()
        {
            PostImages = new HashSet<PostImage>();
        }
        [Key]
        public int Post_Id { get; set; }
        [StringLength(450)]
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
   
        public int User_Id { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<PostImage> PostImages { get; set; }

    }
}