using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educat.Model.Table
{
    public class Student
    {
        public Student() 
        {
            Schools = new List<School>();
            Cources = new List<Cource>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }
        public string Email { get; set; }

        public virtual ICollection<School> Schools { get; set; }
        public virtual ICollection<Cource> Cources { get; set; }
    }
}
