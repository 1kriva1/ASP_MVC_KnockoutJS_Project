using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educat.Model.Table
{
    public class Grade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? Mark { get; set; }
        public int StudentId { get; set; }
        public int CourceId { get; set; }

        public virtual Cource Cource { get; set; }
    }
}
