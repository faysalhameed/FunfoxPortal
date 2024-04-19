using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPortal.Models
{
    public class UserClasses
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [ValidateNever]
        public ApplicationUser User { get; set; }

        [Required]
        public int ClassId { get; set; }

        [ValidateNever]
        public Classes Class { get; set; }
    }
}
