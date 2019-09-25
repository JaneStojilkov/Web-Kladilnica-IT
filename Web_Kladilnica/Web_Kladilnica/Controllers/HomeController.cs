using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web_Kladilnica.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Web_Kladilnica.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private List<Game> games = new List<Game>();
        public ActionResult Index()
        {
            return View();
        }
       
        public PartialViewResult GamesPartial(string izbor)
        {
            var g = db.Games.Where(m=>m.Sport.Equals(izbor)).Include(m=>m.Team1).Include(m=>m.Team2);
            return PartialView("_GamesView", g.ToList());
        }
        

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public void updateGames(List<Game> igri)
        {
            games = igri;
        }
        [Authorize(Roles = "Administrator")]
        public ActionResult CreateGame()
        {
            GameCreateModel model=new GameCreateModel();
            model.teams = db.Teams.ToList();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateGame([Bind(Include = "game,team1ID,team2ID")] GameCreateModel gamecreate)
        {

            DateTime dat = DateTime.Now;
            Game game1 = gamecreate.game;
            game1.StartTime = dat;
            game1.EndTime = dat.AddMinutes(105);
            game1.team1Score = 0;
            game1.team2Score = 0;
            game1.Team1 = db.Teams.Find(gamecreate.team1ID);
            game1.Team2 = db.Teams.Find(gamecreate.team2ID);
            db.Games.Add(game1);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        [HttpPost]
        public ActionResult CreatingTicket(TicketGamesView tick)
        {
            TicketCreateModel ticketCreate = new TicketCreateModel();
            string[] idgame = tick.ids.Split(',');
            string[] coefgame = tick.gameCoef.Split(',');
           List<Game> selGames = new List<Game>();
            double totalCoef =1;
            int[] gues = new int[idgame.Length];
            for (int i = 0; i <idgame.Length; i++)
            {
                int tempID = Convert.ToInt32(idgame[i]);
                Game tempGame =db.Games.Include(m=>m.Team1).Include(m => m.Team2).SingleOrDefault(m=>m.ID==tempID);
                if (tempGame != null)
                {
                    if (coefgame[i].ToString().Equals("c1"))
                    {
                        totalCoef *= tempGame.Coefficient1;
                        gues[i] = 1;
                    }
                    else if (coefgame[i].ToString().Equals("c2"))
                    {
                        totalCoef *= tempGame.Coefficient2;
                        gues[i] = 0;
                    }
                    else if(coefgame[i].ToString().Equals("c3"))
                    {
                        totalCoef *= tempGame.Coefficient3;
                        gues[i] = 2;
                    }
                    selGames.Add(tempGame);
                }
            }
            /* ticket.games = selGames;
             ticket.totalCoefficient = totalCoef;
             ticket.time = DateTime.Now;
             ticket.guesses = gues;*/
            
            ticketCreate.totalCoefficient = totalCoef;
            ticketCreate.gameIds = idgame;
            ticketCreate.games = selGames;
            ticketCreate.guesses = gues;
            return View(ticketCreate);

    
        }
        [HttpPost]
        public ActionResult CreateTicket([Bind(Include = "moneyInvested,totalCoefficient,guesses,gameIds")] TicketCreateModel ticketCreate)
        {
            
            Ticket ticket = new Ticket();
            ticket.time = DateTime.Now;
            ticket.totalCoefficient = ticketCreate.totalCoefficient;
            ticket.moneyInvested = ticketCreate.moneyInvested;
            ticket.guesses = ticketCreate.guesses;
            List<Game> selGames = new List<Game>();
            for(int i = 0; i < ticketCreate.gameIds.Length; i++)
            {
                int tempID = Convert.ToInt32(ticketCreate.gameIds[i]);
                Game tempGame = db.Games.Include(m => m.Team1).Include(m => m.Team2).SingleOrDefault(m => m.ID == tempID);
                if (tempGame != null)
                {
                    selGames.Add(tempGame);
                }

            }
            ticket.games = selGames;
            db.tickets.Add(ticket);
            db.SaveChanges();
            List<TicketDisplayViewModel> tickets1 = new List<TicketDisplayViewModel>();
            List<GameViewModelT> gameview = new List<GameViewModelT>();
            string userID = User.Identity.GetUserId();
                ApplicationUser user = db.Users.Include(m => m.tickets).SingleOrDefault(m => m.Id == userID);
                user.tickets.Add(ticket);

               db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
                foreach(var item in user.tickets)
                    {
               
                Ticket temp = db.tickets.Include(m => m.games).SingleOrDefault(m => m.ID == item.ID);
                TicketDisplayViewModel tv = new TicketDisplayViewModel()
                {
                    totalCoefficient = temp.totalCoefficient,
                    guesses = temp.guesses,
                    games = temp.games,
                    moneyInvested = temp.moneyInvested,
                    Result = temp.win,
                    futureWinnings = temp.WinMoney
                };
                tickets1.Add(tv);
            }
           

            return View("ViewTickets",tickets1);
        }
        [HttpPost]
        public ActionResult ViewTickets()
        {
            string userID = User.Identity.GetUserId();
            ApplicationUser user = db.Users.Include(m => m.tickets).SingleOrDefault(m => m.Id == userID);
            List<TicketDisplayViewModel> tickets = new List<TicketDisplayViewModel>();
            foreach (var item in user.tickets)
            {

                Ticket temp = db.tickets.Include(m => m.games).SingleOrDefault(m => m.ID == item.ID);
                TicketDisplayViewModel tv = new TicketDisplayViewModel()
                {
                    totalCoefficient = temp.totalCoefficient,
                    guesses = temp.guesses,
                    games = temp.games,
                    moneyInvested = temp.moneyInvested,
                    Result = temp.win,
                    futureWinnings = temp.WinMoney
                };
                tickets.Add(tv);
            }


            return View("ViewTickets",tickets);

        }

    }
}