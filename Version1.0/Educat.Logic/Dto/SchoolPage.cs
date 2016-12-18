using Educat.Model.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educat.Logic.Dto
{
    public class SchoolPage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Cource> Cources { get; set; }
    }
}
