using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Pui_Madalina_Proiect.Models
{
    public class Corporations
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Corporation Name")]
        [StringLength(50)]
        public string CorporationName { get; set; }
        [StringLength(70)]
        public string Adress { get; set; }
        public ICollection<PublishedGame> PublishedGames { get; set; }
    }
}
