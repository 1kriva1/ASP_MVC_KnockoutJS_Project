using Educat.Model.Table;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educat.Logic.Dto
{
    public class CourceDto
    {
        public CourceDto()
        {
            Students = new List<Student>();
            Grades = new List<Grade>();
        }

        public int Id { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int SchoolId { get; set; }

        public string StartDateString
        {
            get;
            set;
        }

        public string FinishDateString
        {
            get;
            set;
        }

        public ICollection<Student> Students { get; set; }
        public ICollection<Grade> Grades { get; set; }
    }
}
