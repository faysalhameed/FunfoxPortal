using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPortal.Models.ViewModels
{
    public class DeleteClassVM
    {

        public Classes Cls { get; set; } = new Classes();

        public bool UserExists { get; set; }

    }
}
