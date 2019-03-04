namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model12")
        {
        }

        public virtual DbSet<Doc_Certificate> Doc_Certificate { get; set; }
        public virtual DbSet<Doc_Institution> Doc_Institution { get; set; }
        public virtual DbSet<Doc_Meeting> Doc_Meeting { get; set; }
        public virtual DbSet<Doc_Solution> Doc_Solution { get; set; }
        public virtual DbSet<Doc_TrainEmpoyees> Doc_TrainEmpoyees { get; set; }
        public virtual DbSet<Doc_Training> Doc_Training { get; set; }
        public virtual DbSet<Heal_Docment> Heal_Docment { get; set; }
        public virtual DbSet<Heal_Records> Heal_Records { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
