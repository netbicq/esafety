using ESafety.Core.Model.DB;
using ESafety.Core.Model.DB.Platform;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.ORM
{
    public class ESFdb:DbContext
    {


        
        public ESFdb() : base("dbconn")
        {
        }


        public virtual DbSet<Auth_Role> Auth_Role { get; set; }
        public virtual DbSet<Auth_User> Auth_User { get; set; }

        public virtual DbSet<Auth_UserRole> Auth_UserRole { get; set; }

        public virtual DbSet<Auth_RoleAuthScope> Auth_RoleAuthScope { get; set; }

        public virtual DbSet<Auth_Key>  Auth_Key { get; set; }

        public virtual DbSet<Auth_KeyDetail>  Auth_KeyDetail { get; set; }

        public virtual DbSet<Auth_UserProfile>  Auth_UserProfile { get; set; }

        public virtual DbSet<AccountInfo>  AccountInfo { get; set; }




        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();

        }
         
    }
     
 
}
