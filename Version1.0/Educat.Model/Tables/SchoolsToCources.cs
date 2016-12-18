using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educat.Model.Tables
{
    public class SchoolsToCources
    {
        public int Id { get; set; }
        public int SchoolId { get; set; }
        public int CourceId { get; set; }
    }
}
