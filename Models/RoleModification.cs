using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Pui_Madalina_Proiect.Models
{
    public class RoleModification
    {
        [Required]
        public string RoleName { get; set; }
        public string RoleId { get; set; }
        public string[] AddIds { get; set; }
        public string[] DeleteIds { get; set; }
    }
}
