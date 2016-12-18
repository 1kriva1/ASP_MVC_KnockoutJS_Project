using Educat.Logic.Dto;
using Educat.Model;
using Educat.Model.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educat.Logic
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DobString { get; set; }
        public string Email { get; set; }
        public ICollection<School> Schools { get; set; }
        public ICollection<Cource> Cources { get; set; }

        public StudentDto()
        {
            Schools = new List<School>();
            Cources = new List<Cource>();
        }
    }
}
