using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArthasPub.DAL;
using ArthasPub.Models;
using Microsoft.AspNet.Identity;

namespace ArthasPub.Controllers
{
    [Authorize(Roles = "Member, Admin")]
    public class ItemController : Controller
    {
        private ArthasPubDB db = new ArthasPubDB();

        // Retrieve item view for authorized user.
        public ActionResult Index()
        {
            db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            // Return admin view if admin role
            if (User.IsInRole("Admin"))
            {
                return View(adminitemview());
            }
            // return user view by default
            else
            {
                return View(useritemview());
            }
        }

        // Return create item view for Admin role user
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // Create item post by Admin role user
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Description,Price,Cost,ItemImageUrl,Upload,Disable")] Item item)
        {
            db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            if (ModelState.IsValid)
            {
                db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                db.Items.Add(item);
                db.SaveChanges();
                var rs = db.Items.SingleOrDefault(b => b.ItemId == item.ItemId);
                if (rs != null)
                {
                    if (Request.Files.Count > 0)
                    {
                        //Read the uploaded files and put it in the ~/Content/Uploads/, then set the ImageUrl for the Item.
                        HttpPostedFileBase file = Request.Files[0];
                        if (file.ContentLength > 0)
                        {
                            var fileName = item.ItemId + ".jpg";
                            var path = Path.Combine(
                            Server.MapPath("~/Content/Uploads/"), fileName);
                            file.SaveAs(path);
                            item.ItemImageUrl = "~/Content/Uploads/" + fileName;
                        }
                    }
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            return View(useritemview());
        }

        // Return edit item view for Admin role user
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // Edit item post by Admin role user
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemId,Name,Description,Price,Cost,ItemImageUrl,Upload,Disable")] Item item)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    //Read the uploaded files and put it in the ~/Content/Uploads/, then set the ImageUrl for the Item.
                    HttpPostedFileBase file = Request.Files[0];
                    if (file.ContentLength > 0)
                    {
                        var fileName = item.ItemId + ".jpg";
                        var path = Path.Combine(
                        Server.MapPath("~/Content/Uploads/"), fileName);
                        file.SaveAs(path);
                        item.ItemImageUrl = "~/Content/Uploads/" + fileName;
                        byte[] uploadedFile = new byte[item.Upload.InputStream.Length];
                        item.Upload.InputStream.Read(uploadedFile, 0, uploadedFile.Length);
                    }
                }
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        // Retrieve Add item to Order view for Member
        [Authorize(Roles = "Member")]
        public ActionResult Add(int? Id)
        {
            db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            Item i = db.Items.Where(x => x.ItemId == Id).SingleOrDefault();
            return View(i);
        }

        // Post the add item to cartitem (ordered item) for Member
        [Authorize(Roles = "Member")]
        [HttpPost, ActionName("Add")]
        public ActionResult Add(Item item, string qty, int Id)
        {
            db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            Item p = db.Items.Where(i => i.ItemId == Id).SingleOrDefault();
            CartItem cart = new CartItem();
            cart.ItemId = p.ItemId;
            cart.Price = p.Price;
            cart.Quantity = Convert.ToInt32(qty);
            cart.UserId = User.Identity.GetUserId();
            cart.OrderDate = System.DateTime.Now;
            decimal total = cart.Price * cart.Quantity;
            db.CartItems.Add(cart);
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

        private List<Item> useritemview()
        {
            db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            var c = db.Items.Where(i => i.Disable == false).ToList();
            return c;
        }

        private List<Item> adminitemview()
        {
            var c = db.Items.ToList();
            return c;
        }


    }
}
