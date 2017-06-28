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
    public class adminController : Controller
    {
        private Database1Entities1 db = new Database1Entities1();

        //
        // GET: /admin/

        public ActionResult Index()
        {

            return View();
        }
        public ActionResult all()
        {

            return View((db.Users.SqlQuery("Select * from [User] WHERE user_type != 1").ToList<User>()).ToList());
            //return View(db.Users.ToList());
        }

        //
        // GET: /admin/Details/5

        public ActionResult Details(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // GET: /admin/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /admin/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        //
 
        //
        // GET: /admin/Delete/5

        public ActionResult DeleteProj(int id = 0)
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

        [HttpPost, ActionName("DeleteProj")]
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
            var l2 = (from u in db.Projects where u.Status == 0 select u).ToList();
            return View(l2);
        }
        public ActionResult dell()
        {
            var l2 = (from u in db.Projects where u.Status == 1 select u).ToList();
            return View(l2);
        }
        public ActionResult listProj()
        {
            var l2 = (from u in db.Projects select u).ToList();
            return View(l2);
        }
        public ActionResult EditProj(int id = 0)
        {
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.user_id = new SelectList(db.Users, "ID", "FirstName", project.user_id);
            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProj(Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("listProj");
            }
            return View(project);
        }

    }
}