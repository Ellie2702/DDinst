using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDinst.Base.DTO
{
    public class ImageDTO
    {
   
        public int Image_Id { get; set; }
   
        public string ImageType { get; set; }
        public string ImageName { get; set; }

        public int Owner { get; set; }
    }
}