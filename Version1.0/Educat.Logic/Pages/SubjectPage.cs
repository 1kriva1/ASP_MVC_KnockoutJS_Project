using Educat.Model;
using Educat.Model.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educat.Logic.Pages
{
    public class SubjectPage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SchoolId { get; set; }
        public string SchoolName { get; set; }
        public List<School> Schools { get; set; }
    }
}
