using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDinst.Base.DTO
{
    public class PostImageDTO
    {
     
        public int Post_Id { get; set; }

     
        public int Image_Id { get; set; }

        public List<ImageDTO> Images { get; set; }
    }
}