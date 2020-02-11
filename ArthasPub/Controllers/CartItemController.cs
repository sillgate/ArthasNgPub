using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
    public class CartItemController : Controller
    {
        private ArthasPubDB db = new ArthasPubDB();

        // GET: Cart List
        public ActionResult Index()
        {
            return View(userview());
        }

        //GET: Checkout
        public ActionResult Checkout()
        {
            return View(userview());
        }

        //POST: Checkout
        [HttpPost]
        public ActionResult Checkout(List<CartItem> cart)
        {
            if (ModelState.IsValid)
            {
                decimal t = 0;
                foreach (var i in cart)
                {
                    t += i.Total;
                }
                Order order = new Order();
                order.UserId = User.Identity.GetUserId();
                order.CreateDate = System.DateTime.Now;
                order.Total = t;
                System.Diagnostics.Debug.WriteLine(t);
                System.Diagnostics.Debug.WriteLine("hihi");
                System.Diagnostics.Debug.WriteLine(order.OrderId);
                System.Diagnostics.Debug.WriteLine(order.UserId);
                db.Orders.Add(order);
                db.SaveChanges();
                foreach (var i in cart)
                {
                    var rs = db.CartItems.SingleOrDefault(b => b.CartItemId == i.CartItemId);
                    if (rs != null)
                    {
                        rs.OrderId = order.OrderId;
                        db.SaveChanges();
                    }
                }
            }
            return Index(userview());
        }

        //Post: Change Quantity
        [HttpPost]
        public ActionResult Index(List<CartItem> cart)
        {
            if (ModelState.IsValid)
            {
                foreach (var i in cart)
                {
                    var c = db.CartItems.Where(a => a.CartItemId.Equals(i.CartItemId)).FirstOrDefault();
                    c.Quantity = i.Quantity;
                    db.SaveChanges();
                    return View(userview());
                }
            }
            return View(userview());
        }

        // GET: Item/Delete/5
        public ActionResult Cancel(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItem item = db.CartItems.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Item/Delete/5
        [HttpPost, ActionName("Cancel")]
        public ActionResult Cancel(int id)
        {
            CartItem item = db.CartItems.Find(id);
            item.Cancel = true;
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

        private List<CartItem> userview() 
        {
            var c = db.CartItems.ToList().Where(i => i.UserId == User.Identity.GetUserId()).ToList();
                return c;  
        }
    }
}
