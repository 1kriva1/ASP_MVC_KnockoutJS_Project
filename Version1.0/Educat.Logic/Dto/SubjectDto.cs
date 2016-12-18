using Educat.Model.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educat.Logic.Dto
{
    public class SubjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? SchoolId { get; set; }
        public School School { get; set; }
    }
}
