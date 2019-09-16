using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web_Kladilnica.Models;

namespace Web_Kladilnica.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult GamesPartial()
        {
            var g = db.Games.Include(m=>m.Team1).Include(m=>m.Team2);
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
    }
}