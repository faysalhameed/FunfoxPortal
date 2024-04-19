using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPortal.Models
{
    public class Classes
    {
        [Key]
        public int ClassId { get; set; }

        [Required]
        public string ClassName { get; set; }

        [Required]
        public int ProgramId { get; set; }

        [ValidateNever]
        public ProgramDetail Program { get; set; }

        [Required]
        public int Level { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = @"{0:hh\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan StartTime { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = @"{0:hh\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan EndTime { get; set; }

        [Required]
        public int MaximumSize { get; set; }

    }
}
