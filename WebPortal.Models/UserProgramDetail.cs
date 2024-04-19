using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPortal.Models
{
    public class UserProgramDetail
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [ValidateNever]
        public ApplicationUser User { get; set; }

        [Required]
        public int ProgramDetailId { get; set; }

        [ValidateNever]
        public ProgramDetail ProgramDetail { get; set; }

    }
}
