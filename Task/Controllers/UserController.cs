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
    public class UserController : Controller
    {
        private Database1Entities1 db = new Database1Entities1();

        //
        // GET: /User/

        public ActionResult Index()
        {
            var projects = db.Projects.Include(p => p.User);
            return View(projects.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /User/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User objUser,string returnUrl)
    {
        TempData["notice"] = "Successfully registered";
            if (objUser.Email == null || objUser.JobDescription == null || objUser.LastName == null || objUser.Mobile == null || objUser.Password == null || objUser.user_type == null)
            {
                ModelState.AddModelError("", "Empty Data");
                return View(objUser);
            }
            else if (ModelState.IsValid)
            {
                db.Users.Add(objUser);
                db.SaveChanges();
            }
            if (string.IsNullOrEmpty(returnUrl) && Request.UrlReferrer != null)
                returnUrl = Server.UrlEncode(Request.UrlReferrer.PathAndQuery);

            if (Url.IsLocalUrl(returnUrl) && !string.IsNullOrEmpty(returnUrl))
            {
                ViewBag.ReturnURL = returnUrl;
            }
            return View(objUser);
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User objUser)
        {
            if (ModelState.IsValid)
            {
                
                    var obj = db.Users.Where(a => a.Email.Equals(objUser.Email) && a.Password.Equals(objUser.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserID"] = obj.ID.ToString();
                        Session["UserName"] = obj.FirstName.ToString();
                        Session["UserNameTwo"] = obj.LastName.ToString();
                        Session["UserType"] = obj.user_type.ToString();
                        if(obj.Photo != null)
                        Session["UserPhoto"] = obj.Photo.ToString();
                        return RedirectToAction("Route");
                    }
                }
            
            return View(objUser);
        }

        public ActionResult Route()
        {
            if (Session["UserID"] != null)
            {
                if (String.Equals(Session["UserType"], "1"))
                    return RedirectToAction("Index","Admin");

                else if (String.Equals(Session["UserType"], "2"))
                    return RedirectToAction("Index", "MarketDir");

                else if (String.Equals(Session["UserType"], "3"))
                    return RedirectToAction("Index", "MarketTeamLead");

                else if (String.Equals(Session["UserType"], "4"))
                    return RedirectToAction("Index", "MarketTrainee");

                else if (String.Equals(Session["UserType"], "5"))
                    return RedirectToAction("Index", "Customer");
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult listProfile()
        {
            User m = new User();
            m.ID = Convert.ToInt32(Session["UserID"]);
            int n=m.ID;
            var ctx = new Database1Entities1();
            //var l = ctx.Users.SqlQuery("Select * from User where ID=1").ToList();
           var l= ctx.Users.Where(u => u.ID == n).Select(u => u);
            return View(l);

        }
        public ActionResult Update(int id = 0)
        {
            User u = db.Users.Find(id);
            if (u == null)
            {
                return HttpNotFound();
            }
            ViewBag.user_id = new SelectList(db.Users, "ID", "FirstName", u.ID);
            return View(u);
        }

        [HttpPost]
        public ActionResult Update(User u,HttpPostedFileBase file)
        {
            TempData["notice"] = "Successfully Updated";
            u.user_type = Convert.ToInt32(Session["UserType"]);
            if (u.Photo == null && file == null )
                ModelState.AddModelError("", "Photo Is empty , upload it please");
            else
            {
                if (file != null)
                {
                    var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", "jpeg" };
                    if (!(allowedExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.')))))
                    {
                        ModelState.AddModelError("", "Wrong File ");
                    }
                    else
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var paths = Path.Combine(Server.MapPath("~/Img"), fileName);
                        file.SaveAs(paths);
                        u.Photo = "/Img/" + fileName;
                    }
                }
                    if (ModelState.IsValid)
                    {
                        db.Entry(u).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                }
            return View(u);
        }

        }
}