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
            game1.EndTime = dat.AddMinutes(90);
            game1.Completed = false;
            
            game1.HalfTime = false;
            game1.selectedCoefficient = 0;
            game1.team1Score = 0;
            game1.team2Score = 0;
            game1.Time = 0;
            game1.Team1 = db.Teams.Find(gamecreate.team1ID);
            game1.Team2 = db.Teams.Find(gamecreate.team2ID);
            db.Games.Add(game1);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        [HttpPost]
        public ActionResult CreatingTicket(TicketGamesView tick)
        {
            Ticket ticket = new Ticket();
            string[] idgame = tick.ids.Split(',');
            string[] coefgame = tick.gameCoef.Split(',');
           List<Game> selGames = new List<Game>();
            float totalCoef =1;
            for (int i = 0; i <idgame.Length; i++)
            {
                int tempID = Convert.ToInt32(idgame[i]);
                Game tempGame =db.Games.Include(m=>m.Team1).Include(m => m.Team2).SingleOrDefault(m=>m.ID==tempID);
                if (tempGame != null)
                {
                    if (coefgame[i].ToString().Equals("c1"))
                    {
                        totalCoef *= tempGame.Coefficient1;
                        tempGame.selectedCoefficient = 1;
                    }
                    else if (coefgame[i].ToString().Equals("c2"))
                    {
                        totalCoef *= tempGame.Coefficient2;
                        tempGame.selectedCoefficient = 0;
                    }
                    else
                    {
                        totalCoef *= tempGame.Coefficient3;
                        tempGame.selectedCoefficient = 2;
                    }
                    selGames.Add(tempGame);
                }
            }
            ticket.games = selGames;
            ticket.totalCoefficient = totalCoef;
            ticket.time = DateTime.Now;
            return View(ticket);

    
        }
        [HttpPost]
        public ActionResult CreateTicket([Bind(Include = "ID,games,moneyInvested,totalCoefficient,WinMoney,win,time")] Ticket ticket)
        {
            List<Ticket> tickets1 = new List<Ticket>();
            
            string userID = User.Identity.GetUserId();
                ApplicationUser user = db.Users.Include(m => m.tickets).SingleOrDefault(m => m.Id == userID);
                user.tickets.Add(ticket);
                db.Entry(user).State = EntityState.Modified;
                tickets1 = user.tickets ;
            db.tickets.Add(ticket);
            db.SaveChanges();
            return View("ViewTickets",tickets1);
        }

    }
}