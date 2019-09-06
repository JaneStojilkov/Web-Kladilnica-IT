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
    public class TicketsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Tickets
        public ActionResult Index()
        {
            return View(db.Betting_Ticket.ToList());
        }

        // GET: Tickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Betting_Ticket betting_Ticket = db.Betting_Ticket.Find(id);
            if (betting_Ticket == null)
            {
                return HttpNotFound();
            }
            return View(betting_Ticket);
        }

        // GET: Tickets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Invested,SumCooef,SumWin,Won,Time")] Betting_Ticket betting_Ticket)
        {
            if (ModelState.IsValid)
            {
                db.Betting_Ticket.Add(betting_Ticket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(betting_Ticket);
        }

        // GET: Tickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Betting_Ticket betting_Ticket = db.Betting_Ticket.Find(id);
            if (betting_Ticket == null)
            {
                return HttpNotFound();
            }
            return View(betting_Ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Invested,SumCooef,SumWin,Won,Time")] Betting_Ticket betting_Ticket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(betting_Ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(betting_Ticket);
        }

        // GET: Tickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Betting_Ticket betting_Ticket = db.Betting_Ticket.Find(id);
            if (betting_Ticket == null)
            {
                return HttpNotFound();
            }
            return View(betting_Ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Betting_Ticket betting_Ticket = db.Betting_Ticket.Find(id);
            db.Betting_Ticket.Remove(betting_Ticket);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
