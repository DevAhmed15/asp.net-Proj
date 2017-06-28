using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task.Models;

namespace Task.Controllers
{
    public class teamController : Controller
    {
        private Database1Entities1 db = new Database1Entities1();

        //
        // GET: /team/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /team/Details/5

        public ActionResult Details(int id = 0)
        {
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        //
        // GET: /team/Create

        public ActionResult Create()
        {
            int WantedID = Convert.ToInt32(Session["UserID"]);
            ViewBag.IDW = WantedID;
            ViewBag.user_trainee = new SelectList(db.Users.Where(s => s.user_type == 4), "ID", "FirstName");
            ViewBag.user_mtl = new SelectList(db.Users.Where(q => q.user_type == 3), "ID", "FirstName");
            ViewBag.proj_id = new SelectList(db.Projects.Where(o => o.Status == 0), "ID", "Title");
            return View();
        }

        //
        // POST: /team/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Team team)
        {
            if (ModelState.IsValid)
            {
                db.Teams.Add(team);
                db.SaveChanges();
                return RedirectToAction("Index","MarketTeamLead");
            }

            ViewBag.MD_id = new SelectList(db.Users, "ID", "FirstName", team.MD_id);
            ViewBag.Trainee_id = new SelectList(db.Users, "ID", "FirstName", team.Trainee_id);
            return View(team);
        }

        //
        // GET: /team/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            ViewBag.MD_id = new SelectList(db.Users, "ID", "FirstName", team.MD_id);
            ViewBag.Trainee_id = new SelectList(db.Users, "ID", "FirstName", team.Trainee_id);
            return View(team);
        }

        //
        // POST: /team/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Team team)
        {
            if (ModelState.IsValid)
            {
                db.Entry(team).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "MarketTeamLead");
            }
            ViewBag.MD_id = new SelectList(db.Users, "ID", "FirstName", team.MD_id);
            ViewBag.Trainee_id = new SelectList(db.Users, "ID", "FirstName", team.Trainee_id);
            return View(team);
        }

        //
        // GET: /team/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        //
        // POST: /team/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Team team = db.Teams.Find(id);
            db.Teams.Remove(team);
            db.SaveChanges();
            return RedirectToAction("Index","MarketTeamLead");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}