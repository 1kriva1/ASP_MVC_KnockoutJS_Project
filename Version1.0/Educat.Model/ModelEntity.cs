using Educat.Model.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educat.Model
{
    public class DataContext : DbContext, IDisposable
    {
        public DataContext()
            : base("name=DataConnection")
        {

        }

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<School> Schools { get; set; }
        public virtual DbSet<StudentsToSchools> StudentsToSchools { get; set; }
        public virtual DbSet<Cource> Cources { get; set; }
        public virtual DbSet<SchoolsToCources> SchoolsToCources { get; set; }
        public virtual DbSet<StudentsToCources> StudentsToCources { get; set; }
    }
    
}
