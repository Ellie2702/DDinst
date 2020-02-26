using DDinst.Base.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDinst.Models
{
    public class PostModel
    {
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }

        public string Owner { get; set; }

        public virtual List<PostImage> PostImages { get; set; }
        public virtual List<Image> Images { get; set; }
    }
}