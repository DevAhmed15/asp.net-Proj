using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task.Models;

namespace Task.Controllers
{
    public class CustomerController : Controller
    {
        private Database1Entities1 db = new Database1Entities1();
        //
        // GET: /Customer/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult addProj()
        {
            Project m=new Project();
            m.user_id= Convert.ToInt32(Session["UserID"]);
            ViewBag.user_md = new SelectList(db.Users.Where(q => q.user_type == 2), "ID", "FirstName");
            return View(m);
        }

        [HttpPost]
        public ActionResult addProj(Project p)
        {
            if (ModelState.IsValid)
            {
                db.Projects.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(p);
        }
        public ActionResult listProj()
        {
            int uID = Convert.ToInt32(Session["UserID"]);
            var l2=(from u in db.Projects where u.user_id==uID select u).ToList();
            return View(l2);
        }
        public ActionResult curr()
        {
            int uID = Convert.ToInt32(Session["UserID"]);
            var l2 = (from u in db.Projects where u.user_id == uID && u.Status==0 select u).ToList();
            return View(l2);
        }
        public ActionResult dell()
        {
            int uID = Convert.ToInt32(Session["UserID"]);
            var l2 = (from u in db.Projects where u.user_id == uID && u.Status == 1 select u).ToList();
            return View(l2);
        }

        public ActionResult DetailsProj(int id = 0)
        {
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
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
                return RedirectToAction("Index");
            }
            return View(project);
        }
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
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


    }
}
