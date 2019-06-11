using ESafety.Core.Model.DB;
using ESafety.Core.Model.DB.Account;
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

        public virtual DbSet<Flow_Master> Flow_Masters { get; set; }
        public virtual DbSet<Flow_Points> Flow_Points { get; set; }
        public virtual DbSet<Flow_PointUsers> Flow_PointUsers { get; set; }
        public virtual DbSet<Flow_Result> Flow_Result { get; set; }
        public virtual DbSet<Flow_Task> Flow_Task { get; set; }

        public virtual DbSet<Auth_WxBinds> Auth_WxBinds { get; set; }

        #region "account"

        public virtual DbSet<Basic_DangerPoint> Basic_DangerPoint { get; set; }
        public virtual DbSet<Basic_DangerPointRelation> Basic_DangerPointRelation { get; set; }
        public virtual DbSet<Doc_Certificate> Doc_Certificate { get; set; }
        public virtual DbSet<Doc_Institution> Doc_Institution { get; set; }
        public virtual DbSet<Doc_Meeting> Doc_Meeting { get; set; }
        public virtual DbSet<Doc_Solution> Doc_Solution { get; set; }
        public virtual DbSet<Doc_TrainEmpoyees> Doc_TrainEmpoyees { get; set; }
        public virtual DbSet<Doc_Training> Doc_Training { get; set; }
        public virtual DbSet<Heal_Docment> Heal_Docment { get; set; }
        public virtual DbSet<Heal_Records> Heal_Records { get; set; }

        public virtual DbSet<Basic_Danger> Basic_Danger { get; set; }
        public virtual DbSet<Basic_DangerRelation> Basic_DangerRelation { get; set; }
        public virtual DbSet<Basic_DangerSafetyStandards> Basic_DangerSafetyStandards { get; set; }
        public virtual DbSet<Basic_DangerSort> Basic_DangerSort { get; set; }
        public virtual DbSet<Basic_Facilities> Basic_Facilities { get; set; }
        public virtual DbSet<Basic_FacilitiesSort> Basic_FacilitiesSort { get; set; }
        public virtual DbSet<Basic_Opreation> Basic_Opreation { get; set; }
        public virtual DbSet<Basic_OpreationFlow> Basic_OpreationFlow { get; set; }
        public virtual DbSet<Basic_Post> Basic_Post { get; set; }
        public virtual DbSet<Basic_PostEmployees> Basic_PostEmployees { get; set; }
        public virtual DbSet<Basic_SafetyStandard> Basic_SafetyStandard { get; set; }

        public virtual DbSet<Bll_AttachFile> AttachFile { get; set; }



        public virtual DbSet<Basic_Vedio> Basic_Vedio { get; set; }
        public virtual DbSet<Basic_VedioSubject> Basic_VedioSubject { get; set; }
        public virtual DbSet<Bll_InspectTask> Bll_InspectTask { get; set; }
        public virtual DbSet<Bll_InspectTaskSubject> Bll_InspectTaskSubject { get; set; }
        public virtual DbSet<Bll_OpreateionBillFlow> Bll_OpreateionBillFlow { get; set; }
        public virtual DbSet<Bll_OpreationBill> Bll_OpreationBill { get; set; }
        public virtual DbSet<Bll_TaskBill> Bll_TaskBill { get; set; }
        public virtual DbSet<Bll_TaskBillSubjects> Bll_TaskBillSubjects { get; set; }
        public virtual DbSet<Bll_TroubleControl> Bll_TroubleControl { get; set; }
        public virtual DbSet<Bll_TroubleControlDetails> Bll_TroubleControlDetails { get; set; }
        public virtual DbSet<Bll_TroubleControlFlows> Bll_TroubleControlFlows { get; set; }

        #endregion



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();

        }
         
    }
     
 
}
