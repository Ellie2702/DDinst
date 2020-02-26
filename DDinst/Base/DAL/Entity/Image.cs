using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DDinst.Base.DAL.Entity
{
    public class Image
    {
        [Key]
        public int Image_Id { get; set; }
        public string ImageName { get; set; }
        public string ImageType { get; set; }
        public int Owner { get; set; }

        public virtual User User { get; set; }
    }
}