using DDinst.Base.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDinst.Models
{
    public class FeedModel
    { 
        public List<Post> Posts { get; set; }
        public List<Image> images { get; set; }

        public List<string> Nickname{ get; set; }
    }
}