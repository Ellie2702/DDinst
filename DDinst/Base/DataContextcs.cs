using DDinst.Base.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DDinst.Base
{
    public class DataContextcs:DbContext
    {
        public DataContextcs(): base("InstDB")
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostImage> PostImages { get; set; }
        public DbSet<Avatar> Avatars { get; set; }

    }
}