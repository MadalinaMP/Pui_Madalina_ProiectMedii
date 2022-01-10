using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pui_Madalina_Proiect.Models
{
    public class PublishedGame
    {
        public int CorporationID { get; set; }
        public int GameID { get; set; }
        public Corporations Corporations { get; set; }
        public Game Game { get; set; }
    }
}
