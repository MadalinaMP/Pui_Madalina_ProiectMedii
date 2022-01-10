using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pui_Madalina_Proiect.Models.CollectionViewModels
{
    public class CorporationIndexData
    {
        public IEnumerable<Corporations> Corporations { get; set; }
        public IEnumerable<Game> Games { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
