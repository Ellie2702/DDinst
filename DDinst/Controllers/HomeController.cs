using DDinst.Base;
using DDinst.Base.DAL.Entity;
using DDinst.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using System.Text;
using System.IO;

namespace DDinst.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //var Email = HttpContext.User.Identity.Name;
            //if (Email != null)
            //{
            //    return RedirectToAction("Profile", "Home");
            //}
            //else
            //{
                return RedirectToAction("Feed", "Home");
            //}
            
        }

        [AllowAnonymous]
        public ActionResult Feed(FeedModel model)
        {
            using (DataContextcs db = new DataContextcs())
            {
                var date = DateTime.Now.AddHours(-24);
                var posts = db.Posts.Where(p => p.DateCreated > date);
                model.Posts = posts.ToList();
                model.images = new List<Image>();
                model.Nickname = new List<string>();
                for (int i = 0; i < 10 && i < model.Posts.Count; i++)
                {
                    var user = model.Posts[i].User_Id;
                    var userName = db.Users.Where(u => u.User_Id == user).FirstOrDefault();
                    var id = model.Posts[i].Post_Id;
                    var temp = db.PostImages.Where(p => p.Post_Id == id).FirstOrDefault();
                    var img = db.Images.Where(im => im.Image_Id == temp.Image_Id).FirstOrDefault();
                    model.images.Add(img);
                    model.Nickname.Add(userName.Nickname);
                }
            }
            return View(model);
        }

      
        public ActionResult Registration(RegModel model)
        {
            if (ModelState.IsValid)
            {
                var salt = Base.BLL.Hash.CreateSalt(10);
                var passhash = Base.BLL.Hash.GenerateSaltedHash(model.Password, salt);


                User user = null;
                using (DataContextcs db = new DataContextcs())
                {
                    user = db.Users.FirstOrDefault(u => u.Email == model.Email);
                }
                if (user == null)
                {
                    
                    using (DataContextcs db = new DataContextcs())
                    {
                       
                        db.Users.Add(new User { Email = model.Email, Nickname = model.Nickname, PassHash = Convert.ToBase64String(passhash), Birth = model.Birth,Salt = Convert.ToBase64String(salt), RegDate = DateTime.Now });
                        db.SaveChanges();
                        var pass = Convert.ToBase64String(passhash);
                        user = db.Users.Where(u => u.Email == model.Email && u.PassHash == pass).FirstOrDefault();
                    }
            
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Email, true);
                        string login = HttpContext.User.Identity.Name;
                        return RedirectToAction("Profile", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                }
            }

            return View(model);
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
     
        [AllowAnonymous]
        public async System.Threading.Tasks.Task<ActionResult> Login(UserLogin model)
        {

            if (ModelState.IsValid)
            {
                    
                User user = null;
                using (DataContextcs db = new DataContextcs())
                {
                    user = db.Users.FirstOrDefault(u => u.Email == model.Email);
                    var salt = Convert.FromBase64String(user.Salt);
                    var passhash = Base.BLL.Hash.GenerateSaltedHash(model.PassHash, salt);
                    var hash = Convert.ToBase64String(passhash);
                    user = db.Users.FirstOrDefault(u => u.Email == model.Email && u.PassHash == hash);

                }
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Email, true);
                    HttpContext.Response.Cookies["Email"].Value = model.Email;
                    
                    return RedirectToAction("Profile", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                }
            }

            ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");

            return View(model);
        }

      
        [Authorize]
        public ActionResult Profile(ProfileModel model)
        {
            DataContextcs db = new DataContextcs();
            model.Email = HttpContext.User.Identity.Name;
            User user = db.Users.FirstOrDefault(u => u.Email == model.Email);
            model.Nickname = user.Nickname;
            model.Birth = user.Birth;
            var avatar = db.Avatars.FirstOrDefault(a => a.User_ID == user.User_Id && a.IsUsed == true);
            if(avatar != null) { 
                model.Avatar_ID = avatar.Image_ID;
                model.Avatar_Name = db.Images.Where(i => i.Image_Id == model.Avatar_ID).FirstOrDefault().ImageName;
            }
            model.Posts = db.Posts.Where(p => p.User_Id == user.User_Id).ToList();

            model.images = new List<Image>();
            for(int i = 0; i < model.Posts.Count; i++)
            {
                var id = model.Posts[i].Post_Id;
                var temp = db.PostImages.Where(p => p.Post_Id == id).FirstOrDefault();
                var img = db.Images.Where(im => im.Image_Id == temp.Image_Id).FirstOrDefault();
                model.images.Add(img);
            }

            return View(model);
        }


        
        [HttpPost]
        public ActionResult UploadAvatar()
        {
            foreach (string file in Request.Files)
            {
                var upload = Request.Files[file];
                if (upload != null)
                {
             
                    string fileName = System.IO.Path.GetFileName(upload.FileName);
                 
                    upload.SaveAs(Server.MapPath("~/Files/" + fileName));
                    using (DataContextcs db = new DataContextcs())
                    {
                        var Email = HttpContext.User.Identity.Name;
                        User user = db.Users.FirstOrDefault(u => u.Email == Email);
                        var older1 = db.Images.Where(a => a.ImageName == fileName && a.Owner == user.User_Id).FirstOrDefault();
                        if (older1 == null)
                        {
                            db.Images.Add(new Image { ImageName = fileName, ImageType = upload.ContentType, Owner = user.User_Id });
                        }
                        db.SaveChanges();
                        var avatar = db.Images.Where(a => a.ImageName == fileName && a.Owner == user.User_Id).FirstOrDefault();

                        var last = db.Avatars.Where(a => a.User_ID == user.User_Id && a.IsUsed == true).FirstOrDefault();
                        if (last != null)
                        {
                            last.IsUsed = false;
                        }

                        var older2 = db.Avatars.Where(a => a.Image_ID == avatar.Image_Id && a.User_ID == user.User_Id && a.IsUsed == false).FirstOrDefault();
                        if (older2 != null)
                        {
                            older2.IsUsed = true;
                        }
                        else
                        {
                            db.Avatars.Add(new Avatar { Image_ID = avatar.Image_Id, User_ID = user.User_Id, IsUsed = true });
                        }
                        db.SaveChanges();
                        
                    }

                }
            }
            return RedirectToAction("Profile", "Home");
        }

        [HttpPost]
        public JsonResult UploadImage()
        {
            try
            {
                foreach (string file in Request.Files)
                {
                    var upload = Request.Files[file];
                    if (upload != null)
                    {
                        DataContextcs db = new DataContextcs();
                        var Email = HttpContext.User.Identity.Name;
                        User user = db.Users.FirstOrDefault(u => u.Email == Email);

                        string fileName = System.IO.Path.GetFileName(upload.FileName);

                        upload.SaveAs(Server.MapPath("~/Files/" + fileName));
                        db.Images.Add(new Image { ImageName = fileName, ImageType = upload.ContentType, Owner = user.User_Id });
                        db.SaveChanges();
                    }
                }
            }
            catch { }
            return Json("Изображение загружено! Заполняйте текстовое поле и выкладывайте пост!");
            //RedirectToAction("NewPost", "Home");
        }
        [HttpGet]
        [Authorize]
        public ActionResult NewPost()
        {

            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult NewPost(PostModel model)
        {
            try
            {
                using (DataContextcs db = new DataContextcs())
                {
                    var Email = HttpContext.User.Identity.Name;
                    User user = db.Users.FirstOrDefault(u => u.Email == Email);
                    var Images = db.Images.Where(i => i.Owner == user.User_Id).ToList().LastOrDefault();
                    DateTime now = DateTime.Now;
                    db.Posts.Add(new Post { Content = Request.Form[0], DateCreated = now, User_Id = user.User_Id });
                    db.SaveChanges();
                    var post2 = db.Posts.Where(p => p.User_Id == user.User_Id);
                    var post = post2.ToList().LastOrDefault();
                    db.PostImages.Add(new PostImage { Post_Id = post.Post_Id, Image_Id = Images.Image_Id });
                    db.SaveChanges();

                    return RedirectToAction("Profile", "Home");
                }
            }
            catch(Exception ex)
            {

            }
            return View();
        }

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Feed", "Home");
        }
    }
}