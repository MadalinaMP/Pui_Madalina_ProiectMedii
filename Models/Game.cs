using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pui_Madalina_Proiect.Models
{
    public class Game
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        [Column(TypeName = "decimal(6, 2)")]
        public decimal Price { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<PublishedGame> PublishedGames { get; set; }
    }
}
