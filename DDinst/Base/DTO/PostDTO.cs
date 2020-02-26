using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDinst.Base.DTO
{
    public class PostDTO
    {
        
        public int Post_Id { get; set; }
   
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
     
        public int Owner { get; set; }
        public List<PostImageDTO> PostImages { get; set; }
    }
}