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
    public class sController : Controller
    {
        private Database1Entities1 db = new Database1Entities1();

        //
        // GET: /s/

        public ActionResult Index()
        {
            var teams = db.Teams.Include(t => t.User).Include(t => t.User1);
            var l = db.Projects.SqlQuery("Select * from Project").ToList<Project>();
            List<Project> Ulist = l.ToList();
            ViewBag.projs = Ulist; 
            return View(teams.ToList());
        }

        //
        // GET: /s/Details/5

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
        // GET: /s/Create

        public ActionResult Create()
        {
            ViewBag.MD_id = new SelectList(db.Users, "ID", "FirstName");
            ViewBag.Trainee_id = new SelectList(db.Users, "ID", "FirstName");
            return View();
        }

        //
        // POST: /s/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Team team)
        {
            if (ModelState.IsValid)
            {
                db.Teams.Add(team);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MD_id = new SelectList(db.Users, "ID", "FirstName", team.MD_id);
            ViewBag.Trainee_id = new SelectList(db.Users, "ID", "FirstName", team.Trainee_id);
            return View(team);
        }

        //
        // GET: /s/Edit/5

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
        // POST: /s/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Team team)
        {
            if (ModelState.IsValid)
            {
                db.Entry(team).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MD_id = new SelectList(db.Users, "ID", "FirstName", team.MD_id);
            ViewBag.Trainee_id = new SelectList(db.Users, "ID", "FirstName", team.Trainee_id);
            return View(team);
        }

        //
        // GET: /s/Delete/5

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
        // POST: /s/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Team team = db.Teams.Find(id);
            db.Teams.Remove(team);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}