using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ImageSaverService
{
    public class dbContext : DbContext
    {
        public dbContext() : base("dbcnn")
        {

        }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<ImageModel> Images { get; set; }
    }
}