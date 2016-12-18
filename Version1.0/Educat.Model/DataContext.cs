using Educat.Model.Table;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educat.Model
{
    public class DataContext: DbContext
    {
        public DataContext() : base("name=DataConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false; 
        }

        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Cource> Cources { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                   .HasMany<School>(s => s.Schools)
                   .WithMany(c => c.Students)
                   .Map(cs =>
                   {
                       cs.MapLeftKey("StudentId");
                       cs.MapRightKey("SchoolId");
                       cs.ToTable("SchoolToStudents");
                   });

            modelBuilder.Entity<Student>()
                   .HasMany<Cource>(s => s.Cources)
                   .WithMany(c => c.Students)
                   .Map(cs =>
                   {
                       cs.MapLeftKey("StudentId");
                       cs.MapRightKey("CourceId");
                       cs.ToTable("StudentToCources");
                   });
            

            modelBuilder.Entity<Cource>()
                        .HasMany<Grade>(s => s.Grades)
                        .WithRequired(s => s.Cource)
                        .HasForeignKey(s => s.CourceId);

            modelBuilder.Entity<School>()
                       .HasMany<Subject>(s => s.Subjects)
                       .WithRequired(s => s.School)
                       .HasForeignKey(s => s.SchoolId);
        } 
    }
}
