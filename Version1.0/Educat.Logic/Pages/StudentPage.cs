using Educat.Model;
using Educat.Model.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educat.Logic.Dto
{
    public class StudentPage
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }
        public string Email { get; set; }
        public List<School> Schools { get; set; }
        public int? Mark { get; set; }
    }
}
