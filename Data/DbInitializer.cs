using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pui_Madalina_Proiect.Models;

namespace Pui_Madalina_Proiect.Data
{
    public class DbInitializer
    {
        public static void Initialize(CollectionContext context)
        {
            context.Database.EnsureCreated();
            if (context.Games.Any())
            {
                return; // BD a fost creata anterior
            }
            var games = new Game[]
            {
                new Game{Title="Nier : Automata",Genre="Action RPG",Price=Decimal.Parse("19")},
                new Game{Title="The Sims 4",Genre="Sandbox",Price=Decimal.Parse("29")},
                new Game{Title="Tales Of Zestiria",Genre="Fantasy RPG",Price=Decimal.Parse("59")},
                new Game{Title="Nier : Replicant",Genre="Action RPG",Price=Decimal.Parse("19")},
                new Game{Title="The Sims 3",Genre="Sandbox",Price=Decimal.Parse("29")},
                new Game{Title="Tales Of Eternia",Genre="Fantasy RPG",Price=Decimal.Parse("59")}
            };
            foreach (Game g in games)
            {
                context.Games.Add(g);
            }
            context.SaveChanges();
            var customers = new Customer[]
            {
                new Customer{CustomerID=1050,Name="Mary Lee",BirthDate=DateTime.Parse("1979-09-01")},
                new Customer{CustomerID=1045,Name="Alan Parker",BirthDate=DateTime.Parse("1969-07-08")},
            };
            foreach (Customer c in customers)
            {
                context.Customers.Add(c);
            }
            context.SaveChanges();
            var orders = new Order[]
            {
                new Order{GameID=1,CustomerID=1050,OrderDate=DateTime.Parse("02-25-2020")},
                new Order{GameID=3,CustomerID=1045,OrderDate=DateTime.Parse("09-28-2020")},
                new Order{GameID=1,CustomerID=1045,OrderDate=DateTime.Parse("10-28-2020")},
                new Order{GameID=2,CustomerID=1050,OrderDate=DateTime.Parse("09-28-2020")},
                new Order{GameID=4,CustomerID=1050,OrderDate=DateTime.Parse("09-28-2020")},
                new Order{GameID=6,CustomerID=1050,OrderDate=DateTime.Parse("10-28-2020")},
            };
            foreach (Order o in orders)
            {
                context.Orders.Add(o);
            }
            context.SaveChanges();

            var corporations = new Corporations[]
            {
                new Corporations{CorporationName="Square Enix",Adress="Shinjuku, Tokyo, Japan"},
                new Corporations{CorporationName="EA",Adress="Redwood City, California, USA"},
                new Corporations{CorporationName="Namco Tales Studio",Adress="Tokyo, Japan"},
            };
            foreach (Corporations co in corporations)
            {
                context.Corporations.Add(co);
            }
            context.SaveChanges();

            var publishedgames = new PublishedGame[]
            {
                new PublishedGame {
                GameID = games.Single(c => c.Title == "Tales Of Zestiria" ).ID,
                CorporationID = corporations.Single(i => i.CorporationName == "Namco Tales Studio").ID
                },
                new PublishedGame {
                GameID = games.Single(c => c.Title == "Nier : Automata" ).ID,
                CorporationID = corporations.Single(i => i.CorporationName == "Square Enix").ID
                },
                new PublishedGame {
                GameID = games.Single(c => c.Title == "The Sims 4" ).ID,
                CorporationID = corporations.Single(i => i.CorporationName == "EA").ID
                },
                new PublishedGame {
                GameID = games.Single(c => c.Title == "Nier : Replicant" ).ID,
                CorporationID = corporations.Single(i => i.CorporationName == "Square Enix").ID
                },
                new PublishedGame {
                GameID = games.Single(c => c.Title == "Tales Of Eternia" ).ID,
                CorporationID = corporations.Single(i => i.CorporationName == "Namco Tales Studio").ID
                },
                new PublishedGame {
                GameID = games.Single(c => c.Title == "The Sims 3" ).ID,
                CorporationID = corporations.Single(i => i.CorporationName == "EA").ID
            },
            };
            foreach (PublishedGame pg in publishedgames)
            {
                context.PublishedGames.Add(pg);
            }
            context.SaveChanges();
        }
    }
}