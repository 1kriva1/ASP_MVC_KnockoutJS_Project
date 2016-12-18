using Educat.Model.Table;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educat.Logic.Pages
{
    public class CourcePage
    {        
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int SchoolId { get; set; }
        public string SchoolName { get; set; }

        public string StartDateString { get; set; }
        public string FinishDateString { get; set; }

        public ICollection<Student> Students { get; set; }
        public ICollection Grades { get; set; }
    }
}
