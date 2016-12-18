using Educat.Model;
using Educat.Model.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educat.Logic.Dto
{
    public class SchoolDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Student> Students { get; set; }
        public ICollection<Subject> Subjects { get; set; }

        public SchoolDto()
        {
            Students = new List<Student>();
            Subjects = new List<Subject>();
        }
    }
}
