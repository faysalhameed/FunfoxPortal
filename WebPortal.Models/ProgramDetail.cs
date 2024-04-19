using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPortal.Models
{
    public class ProgramDetail
    {
        [Key]
        public int ProgramId { get; set; }

        [Required]
        public string ProgramName { get; set; }
    }
}
