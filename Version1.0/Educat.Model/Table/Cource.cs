using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educat.Model.Table
{
    [Table("Cources")]
    public class Cource
    {
        public Cource()
        {
            Students = new List<Student>();
            Grades = new List<Grade>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int SchoolId { get; set; }
                
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }

        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Grade> Grades { get; set; }


    }
}
