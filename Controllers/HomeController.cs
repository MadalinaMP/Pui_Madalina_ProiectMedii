using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pui_Madalina_Proiect.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pui_Madalina_Proiect.Data;
using Pui_Madalina_Proiect.Models.CollectionViewModels;

namespace Pui_Madalina_Proiect.Controllers
{
    public class HomeController : Controller
    {
        private readonly CollectionContext _context;
        public HomeController(CollectionContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<ActionResult> Statistics()
        {
            IQueryable<OrderGroup> data =
            from order in _context.Orders
            group order by order.OrderDate into dateGroup
            select new OrderGroup()
            {
                OrderDate = dateGroup.Key,
                GameCount = dateGroup.Count()
            };
            return View(await data.AsNoTracking().ToListAsync());
        }

        public IActionResult Chat()
        {
            return View();
        }
    }
}
