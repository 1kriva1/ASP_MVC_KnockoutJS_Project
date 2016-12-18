using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educat.Model.Tables
{
    public class StudentsToSchools
    {       
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int SchoolsId { get; set; }
    }
}
