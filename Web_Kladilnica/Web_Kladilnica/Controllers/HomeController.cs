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
        public ActionResult Index()
        {
            return View();
        }
       
        public PartialViewResult GamesPartial(string izbor,string datum)
        {
                List<Game> games = db.Games.Where(m => m.Sport.Equals(izbor)).ToList();
            
            List<GameViewModelT> gameView = new List<GameViewModelT>();
            Random random = new Random();
            foreach(Game g in games)
            {
                if (g.Time > 0)
                {
                    int rand = random.Next(1, 50);
                    if (rand == 3)
                    {
                        if (g.Sport.Equals("Basketball"))
                        {
                            if (random.Next(0, 3) == 2)
                            {
                                g.team1Score = g.team1Score + 3;
                            }
                            else
                            {
                                g.team1Score = g.team1Score + 2;
                            }
                        }
                        else
                        {
                            g.team1Score = g.team1Score + 1;
                        }
                        db.Entry(g).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else if (rand == 5)
                    {
                        if (g.Sport.Equals("Basketball"))
                        {
                            if (random.Next(1, 3) == 2)
                            {
                                g.team2Score = g.team2Score + 3;
                            }
                            else
                            {
                                g.team2Score = g.team2Score + 2;
                            }
                        }
                        else
                        {
                            g.team2Score = g.team2Score + 1;
                        }
                        db.Entry(g).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                if (g.StartTime.ToShortDateString() == datum)
                {
                    GameViewModelT gv = new GameViewModelT()
                    {
                        gameId = g.ID,
                        HalfTime = g.HalfTime,
                        team1 = db.Teams.Find(g.Team1ID),
                        team2 = db.Teams.Find(g.Team2ID),
                        start = g.StartTime,
                        end = g.EndTime,
                        team1Score = g.team1Score,
                        team2Score = g.team2Score,
                        completed = g.Completed,
                        Coefficient1 = g.Coefficient1,
                        Coefficient2 = g.Coefficient2,
                        Coefficient3 = g.Coefficient3,
                        outcome = g.Result,
                        Time = g.Time
                    };
                    gameView.Add(gv);
                }
            }
            return PartialView("_GamesView", gameView);
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
            game1.Team1ID = gamecreate.team1ID;
            game1.Team2ID = gamecreate.team2ID;
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
           List<GameViewModelT> selGames = new List<GameViewModelT>();
            double totalCoef =1;
            int[] gues = new int[idgame.Length];
            for (int i = 0; i <idgame.Length; i++)
            {
                int tempID = Convert.ToInt32(idgame[i]);
                Game tempGame =db.Games.Find(tempID);
                if (tempGame != null)
                {
                    GameViewModelT gameView = new GameViewModelT()
                    {
                        gameId = tempGame.ID,
                        HalfTime = tempGame.HalfTime,
                        team1 = db.Teams.Find(tempGame.Team1ID),
                        team2 = db.Teams.Find(tempGame.Team2ID),
                        start = tempGame.StartTime,
                        end = tempGame.EndTime,
                        team1Score = tempGame.team1Score,
                        team2Score = tempGame.team2Score,
                        completed = tempGame.Completed,
                        Coefficient1 = tempGame.Coefficient1,
                        Coefficient2 = tempGame.Coefficient2,
                        Coefficient3 = tempGame.Coefficient3,
                        outcome = tempGame.Result,
                        Time = tempGame.Time
                    };
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
                    selGames.Add(gameView);
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
            List<GameViewModelT> gameviews = new List<GameViewModelT>();
            bool win = false;
            int[] realIds = new int[ticketCreate.gameIds.Length]; 
            for(int i = 0; i < ticketCreate.gameIds.Length; i++)
            {

                int tempID = Convert.ToInt32(ticketCreate.gameIds[i]);
                realIds[i] = tempID;
                Game tempGame = db.Games.Find(tempID);
                if(tempGame.Completed && tempGame.Result == ticketCreate.guesses[i])
                {
                    win = true;
                }
                else
                {
                    win = false;
                }
                if (tempGame != null)
                {
                    GameViewModelT gameView = new GameViewModelT()
                    {
                        gameId = tempGame.ID,
                        HalfTime=tempGame.HalfTime,
                        team1 = db.Teams.Find(tempGame.Team1ID),
                        team2 = db.Teams.Find(tempGame.Team2ID),
                        start = tempGame.StartTime,
                        end = tempGame.EndTime,
                        team1Score = tempGame.team1Score,
                        team2Score = tempGame.team2Score,
                        completed = tempGame.Completed,
                        Coefficient1 = tempGame.Coefficient1,
                        Coefficient2 = tempGame.Coefficient2,
                        Coefficient3 = tempGame.Coefficient3,
                        outcome = tempGame.Result,
                        Time = tempGame.Time
                    };
                    gameviews.Add(gameView);
                }

            }
            ticket.win = win;
            ticket.gameIDs = realIds;
            db.tickets.Add(ticket);
            db.SaveChanges();
            List<TicketDisplayViewModel> tickets1 = new List<TicketDisplayViewModel>();
            
            string userID = User.Identity.GetUserId();
                ApplicationUser user = db.Users.Include(m => m.tickets).SingleOrDefault(m => m.Id == userID);
                user.tickets.Add(ticket);

               db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
                foreach(var item in user.tickets)
                    {
                List<GameViewModelT> viewGames1 = new List<GameViewModelT>();
                Ticket temp = db.tickets.Find(item.ID);
                bool iswon = false;
                        for(int j = 0; j < temp.gameIDs.Length; j++)
                {
                    Game tempGame = db.Games.Find(temp.gameIDs[j]);
                    if (tempGame != null)
                    {
                        if (tempGame.Completed && tempGame.Result == item.guesses[j])
                        {
                            iswon = true;
                        }
                        else
                        {
                            iswon = false;
                        }
                        GameViewModelT gameView = new GameViewModelT()
                        {
                            gameId = tempGame.ID,
                            HalfTime = tempGame.HalfTime,
                            team1 = db.Teams.Find(tempGame.Team1ID),
                            team2 = db.Teams.Find(tempGame.Team2ID),
                            start = tempGame.StartTime,
                            end = tempGame.EndTime,
                            team1Score = tempGame.team1Score,
                            team2Score = tempGame.team2Score,
                            completed = tempGame.Completed,
                            Coefficient1 = tempGame.Coefficient1,
                            Coefficient2 = tempGame.Coefficient2,
                            Coefficient3 = tempGame.Coefficient3,
                            outcome = tempGame.Result,
                            Time = tempGame.Time
                        };
                        viewGames1.Add(gameView);
                    }
                }
                if (temp.win != iswon)
                {
                    temp.win = iswon;
                    db.Entry(temp).State = EntityState.Modified;
                    db.SaveChanges();
                   
                }
                TicketDisplayViewModel tv = new TicketDisplayViewModel()
                {
                    TicketID=temp.ID,
                    totalCoefficient = temp.totalCoefficient,
                    guesses =temp.guesses,
                    games = viewGames1,
                    moneyInvested = temp.moneyInvested,
                    Result = temp.win,
                    futureWinnings = temp.WinMoney,
                    Time=temp.time
                };
                tickets1.Add(tv);
            }
           

            return View("ViewTickets",tickets1);
        }
       [Authorize]
        public ActionResult UserViewTickets(string userID)
        {
            string userid = userID;
            if (userID == null)
            {
                userid = User.Identity.GetUserId();
            }
            List<TicketDisplayViewModel> tickets1 = new List<TicketDisplayViewModel>();
            ApplicationUser user = db.Users.Include(m => m.tickets).SingleOrDefault(m => m.Id == userid);
            foreach (var item in user.tickets)
            {
                List<GameViewModelT> viewGames1 = new List<GameViewModelT>();
                Ticket temp = db.tickets.Find(item.ID);
                bool iswon = false;
                for (int j = 0; j < temp.gameIDs.Length; j++)
                {

                    Game tempGame = db.Games.Find(temp.gameIDs[j]);
                    if (tempGame != null)
                    {
                        if (tempGame.Completed && tempGame.Result == item.guesses[j])
                        {
                            iswon = true;
                        }
                        else
                        {
                            iswon = false;
                        }
                        GameViewModelT gameView = new GameViewModelT()
                        {
                            gameId = tempGame.ID,
                            HalfTime = tempGame.HalfTime,
                            team1 = db.Teams.Find(tempGame.Team1ID),
                            team2 = db.Teams.Find(tempGame.Team2ID),
                            start = tempGame.StartTime,
                            end = tempGame.EndTime,
                            team1Score = tempGame.team1Score,
                            team2Score = tempGame.team2Score,
                            completed = tempGame.Completed,
                            Coefficient1 = tempGame.Coefficient1,
                            Coefficient2 = tempGame.Coefficient2,
                            Coefficient3 = tempGame.Coefficient3,
                            outcome = tempGame.Result,
                            Time = tempGame.Time
                        };
                        viewGames1.Add(gameView);
                    }

                }
                if (temp.win != iswon)
                {
                    temp.win = iswon;
                    db.Entry(temp).State = EntityState.Modified;
                    db.SaveChanges();
                }
                TicketDisplayViewModel tv = new TicketDisplayViewModel()
                {
                    TicketID = temp.ID,
                    totalCoefficient = temp.totalCoefficient,
                    guesses = temp.guesses,
                    games = viewGames1,
                    moneyInvested = temp.moneyInvested,
                    Result = temp.win,
                    futureWinnings = temp.WinMoney,
                     Time = temp.time
                };
                tickets1.Add(tv);
            }
            return View("ViewTickets", tickets1);
        }
        [Authorize(Roles ="Administrator")]
        public ActionResult EditGame(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            GameCreateModel gcm = new GameCreateModel();
            gcm.team1ID = game.Team1ID;
            gcm.team2ID = game.Team2ID;
            gcm.game = game;
            gcm.teams = db.Teams.ToList();
            return View("EditGame", gcm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditGame([Bind(Include = "game,team1ID,team2ID")] GameCreateModel gcm)
        {
                
            if (ModelState.IsValid)
            {
                db.Entry(gcm.game).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("EditGame", gcm);
        }
        public bool DeleteGame(int id)
        {
            Game g = db.Games.Find(id);
            if (g == null)
            {
                return false;
            }
            db.Games.Remove(g);
            db.SaveChanges();
            return true;
        }
        public bool DeleteTicket(int id)
        {
            Ticket t = db.tickets.Find(id);
            if (t == null)
            {
                return false;
            }
            db.tickets.Remove(t);
            db.SaveChanges();
            return true;
        }
       
        

    }
}