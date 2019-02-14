using ESafety.Core.Model.DB;
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

        public virtual DbSet<Sys_Log> Sys_Log { get; set; }

        public virtual DbSet<RPTAccountScope> RPTAccountScope { get; set; }

        public virtual DbSet<RPTChildrenColumn> RPTChildrenColumn { get; set; }

        public virtual DbSet<RPTChildrenTable> RPTChildrenTable { get; set; }

        public virtual DbSet<RPTColumn > RPTColumn { get; set; }

        public virtual DbSet<RPTInfo> RPTInfo { get; set; }

        public virtual DbSet<RPTListParameterColumn> RPTListParameterColumn { get; set; }

        public virtual DbSet<RPTParameter> RPTParameter { get; set; }


        public virtual DbSet<Basic_Dict> Basic_Dict { get; set; }
        public virtual DbSet<Basic_Employee> Basic_Employee { get; set; }
        public virtual DbSet<Basic_Org> Basic_Org { get; set; }
        public virtual DbSet<Basic_UserDefined> Basic_UserDefined { get; set; }
        public virtual DbSet<Basic_UserDefinedValue> Basic_UserDefinedValue { get; set; }
        public virtual DbSet<Flow_Points> Flow_Points { get; set; }
        public virtual DbSet<Flow_PointUsers> Flow_PointUsers { get; set; }
        public virtual DbSet<Flow_Result> Flow_Result { get; set; }
        public virtual DbSet<Flow_Task> Flow_Task { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();

        }
         
    }
     
 
}
