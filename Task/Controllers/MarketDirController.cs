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
    public class MarketDirController : Controller
    {
        private Database1Entities1 db = new Database1Entities1();

        //
        // GET: /MarketDir/

        public ActionResult Index()
        {
            var projects = db.Projects.Include(p => p.User);
            return View(projects.ToList());
        }
        public ActionResult my()
        {

            int I = Convert.ToInt32(Session["UserID"]);
            var projects = db.Projects.Where(u =>u.MD_ID==I);
            return View(projects.ToList());
        }
        //
        // GET: /MarketDir/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.user_id = new SelectList(db.Users, "ID", "FirstName", project.user_id);
            return View(project);
        }

        //
        // POST: /MarketDir/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("listProj");
            }
            ViewBag.user_id = new SelectList(db.Users, "ID", "FirstName", project.user_id);
            return View(project);
        }

        //
        // GET: /MarketDir/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        //
        // POST: /MarketDir/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("listProj");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        public ActionResult curr()
        {

            int I = Convert.ToInt32(Session["UserID"]);
            var ctx = new Database1Entities1();
                var l = ctx.Projects.Where(u => u.MD_ID==I && u.Status==0).ToList<Project>();            
            return View(l.ToList());
        }
        public ActionResult dell()
        {
            int I = Convert.ToInt32(Session["UserID"]);
            var ctx = new Database1Entities1();
            var l = ctx.Projects.Where(u => u.MD_ID==I && u.Status==1).ToList<Project>();
            return View(l.ToList());
        }

        public ActionResult CreateT()
        {
            var ctx = new Database1Entities1();
            var l = ctx.Users.SqlQuery("Select * from [user] WHERE user_type=2 OR user_type=4").ToList<User>();
            List<User> Ulist = l.ToList();
            ViewBag.Userss = Ulist;
            var l2 = ctx.Projects.SqlQuery("Select * from Project WHERE Status=0").ToList<Project>();
            List<Project> Ulist2 = l2.ToList();
            ViewBag.Projs = Ulist2;
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateT(Team t)
        {
            if (ModelState.IsValid)
            {
                db.Teams.Add(t);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(t);
        }

        public ActionResult CreateTeam()
        {
            var ctx = new Database1Entities1();
            ViewBag.user_id = new SelectList(db.Users.Where(s => s.user_type==4), "ID", "FirstName");
            ViewBag.user_mk = new SelectList(ctx.Users.Where(q => q.user_type == 2),"ID","FirstName");
            ViewBag.proj_id = new SelectList(ctx.Projects.Where(o => o.Status == 0),"ID", "Title");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTeam(Team t)
        {
  var teamHere =(from u in db.Teams where u.Trainee_id==t.Trainee_id && u.Proj_id==t.Proj_id && u.MD_id == t.MD_id select u).FirstOrDefault();
            if (teamHere != null)
            {
                ModelState.AddModelError("", "Replicated Request");
                return View(t);
            }
            else if (t.Trainee_id == 0) { ModelState.AddModelError("", "Fail"); return View(t); }

            else if (t.MD_id == 0) { ModelState.AddModelError("", "Here"); return View(t); }
            else if (t.MD_id == 0 || t.Proj_id == 0 || t.Trainee_id == 0)
            {
                ModelState.AddModelError("", "Wrong Data");
                return View(t);
            }
            if (ModelState.IsValid)
            {
                db.Teams.Add(t);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
                return View(t);
            }
        public ActionResult listProj()
        {
            var l2 = (from u in db.Projects select u).ToList();
            return View(l2);
        }

    }
}