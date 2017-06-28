using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task.Models;

namespace Task.Controllers
{
    public class MarketTeamLeadController : Controller
    {
        //
        // GET: /MarketTeamLead/
        private Database1Entities1 db = new Database1Entities1();
        private int userID()
        {
            User m = new User();
            m.ID = Convert.ToInt32(Session["UserID"]);
            int id = m.ID;
            return id;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult requests(int id = 0)
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

        public ActionResult listA()
        {
            int I = Convert.ToInt32(Session["UserID"]);
            var ctx = new Database1Entities1();
            var l = ctx.Teams.Where(u => u.MTL_id == I && u.IsApproved == 1).ToList<Team>();
            return View(l.ToList());
        }

        public ActionResult listD()
        {
            int I = Convert.ToInt32(Session["UserID"]);
            var ctx = new Database1Entities1();
            var l = ctx.Teams.Where(u => u.MTL_id == I && u.IsApproved == 0).ToList<Team>();
            return View(l.ToList());
        }
        //
        // POST: /team/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult requests(Team team)
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
        // 3. Remove Projects (leave the project).
        /*       public ActionResult removeProjects()
        {
            int id = userID();
            return View(db.Projects.Where(a => a.user_id == id).ToList());
        }

        [HttpGet]
        public ActionResult removeProject(long id = 0)
        {
            Project project = db.Projects.Find(id);
            return View(project);

        }

        [HttpPost, ActionName("removeProject")]    // the same as  [HttpPost] removeProject
        public ActionResult removProjectConfirmed(long id = 0)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        ///2. Remove MT (remove Undesired member).

        public ActionResult deleteTrainee()
        {

            // var list = db.Teams.Where(u => u.user_type == 3);
            //return View(db.Users.Where(u=>u.user_type == 3).ToList());
            return View(db.Teams.Where(u => u.MTL_id == userID()).ToList());
        }


        [HttpGet]
        public ActionResult removeTrainees(long id = 0)
        {
            /*
            User trainee = db.Users.Find(id);
            return View(trainee);
            

            var trainees = db.Teams.Where(a => a.Proj_id == id).ToList();
            List<User> traineeList = new List<User>();
            foreach (var item in trainees)
            {
                User user = (User)db.Users.Where(a => a.ID == item.Trainee_id && a.user_type == 4);
                traineeList.Add(user);
            }
            return View(traineeList.ToList());
        }

        [HttpGet]
        public ActionResult removeTrainee(long id = 0)
        {
            Project project = db.Projects.Find(id);
            return View(project);

        }

        [HttpPost, ActionName("removeTrainee")]    // the same as  [HttpPost] removeTrainee
        public ActionResult removTraineeConfirmed(long id = 0)
        {
            User trainee = db.Users.Find(id);
            db.Users.Remove(trainee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        ///Checking His Profile to see the requests
        ///
        public ActionResult CheckRequest()
        {
            int id = userID();
            var teams = db.Teams.Where(a => a.MTL_id == id).ToList();
            List<Project> projects = new List<Project>();
            foreach (var item in teams)
            {
                Project tempProject = (Project)db.Projects.Where(a => a.ID == item.Proj_id);
                projects.Add(tempProject);
            }
            return View(projects.ToList());
        }

        public ActionResult Join(int id = 0)
        {

            return RedirectToAction("index");
        }
        public ActionResult Refuse()
        {

            return RedirectToAction("Index");
        }


        */
    }





}
