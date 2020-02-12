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
            return View(useritemview());
        }


        //[HttpPost]
        //public ActionResult Index(HttpPostedFileBase postedFile)
        //{
        //    byte[] bytes;
        //    using (BinaryReader br = new BinaryReader(postedFile.InputStream))
        //    {
        //        bytes = br.ReadBytes(postedFile.ContentLength);
        //    }
        //    FilesEntities entities = new FilesEntities();
        //    entities.tblFiles.Add(new tblFile
        //    {
        //        Name = Path.GetFileName(postedFile.FileName),
        //        ContentType = postedFile.ContentType,
        //        Data = bytes
        //    });
        //    entities.SaveChanges();
        //    return RedirectToAction("Index");
        //}


        // GET: Item/Details/5
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
            return View(useritemview());
        }

        // GET: Item/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Item/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Description,Price,Cost,ItemImageUrl,InternalImage,Visible")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(useritemview());
        }

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

        // POST: Item/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemId,Name,Description,Price,Cost,ItemImageUrl,InternalImage,Upload,Disible")] Item item)
        {
            if (ModelState.IsValid)
            {
                if(Request.Files.Count > 0)
                {
                    HttpPostedFileBase file = Request.Files[0];
                    if (file.ContentLength > 0)
                    {
                        String fileName = item.ItemId +"jpg";
                        item.ItemImageUrl = Path.Combine(
                            Server.MapPath("~/App_Data/uploads"), fileName);
                        file.SaveAs (item.ItemImageUrl);
                    }
                }
                byte[] uploadedFile = new byte[item.Upload.InputStream.Length];
                item.Upload.InputStream.Read(uploadedFile, 0, uploadedFile.Length);
                item.InternalImage = uploadedFile;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
        }



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
            return View(useritemview());
        }

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

        public ActionResult Add(int? Id)
        {

            Item i = db.Items.Where(x => x.ItemId == Id).SingleOrDefault();
            return View(i);
        }
        [HttpPost, ActionName("Add")]
        public ActionResult Add(Item item, string qty, int Id)
        {
            Item p = db.Items.Where(x => x.ItemId == Id).SingleOrDefault();

            CartItem cart = new CartItem();
            cart.ItemId = p.ItemId;
            cart.Price = p.Price;
            cart.Quantity = Convert.ToInt32(qty);
            cart.UserId = User.Identity.GetUserId();
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
    }
}
