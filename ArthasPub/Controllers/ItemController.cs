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

        // GET: Item
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                return View(adminitemview());
            }
            else
            {
                return View(useritemview());
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
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

        [Authorize(Roles = "Admin")]
        // GET: Item/Create
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        // POST: Item/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Description,Price,Cost,ItemImageUrl,Upload,Visible")] Item item)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
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
                        item.InternalImage = uploadedFile;
                    }
                }
                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(useritemview());
        }

        [Authorize(Roles = "Admin")]
        // GET: Item/Edit/5
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

        [Authorize(Roles = "Admin")]
        // POST: Item/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemId,Name,Description,Price,Cost,ItemImageUrl,Upload,Disable")] Item item)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
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
                        item.InternalImage = uploadedFile;
                    }
                }
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
        }


        [Authorize(Roles = "Admin")]
        // GET: Item/Delete/5
        public ActionResult Delete(int? id)
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

        [Authorize(Roles = "Admin")]
        // POST: Item/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Member")]
        public ActionResult Add(int? Id)
        {

            Item i = db.Items.Where(x => x.ItemId == Id).SingleOrDefault();
            return View(i);
        }

        [Authorize(Roles = "Member")]
        [HttpPost, ActionName("Add")]
        public ActionResult Add(Item item, string qty, int Id)
        {
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
            var c = db.Items.ToList().Where(i => i.Disable == false).ToList();
            return c;
        }

        private List<Item> adminitemview()
        {
            var c = db.Items.ToList();
            return c;
        }


    }
}
