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
            if (User.IsInRole("Member"))
            {
                return View(userorderview());
            }
            if (User.IsInRole("Admin"))
            {
                return View(db.CartItems.Include(u => u.User).ToList());
            }
            return RedirectToRoute("Index");
        }

        //GET: Checkout
        public ActionResult Checkout()
        {
            return View(db.CartItems.ToList().Where(i => i.UserId == User.Identity.GetUserId()).Where(o => o.OrderId == null).Where(c => c.Cancel == false).ToList());
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
            return RedirectToAction("Index","Item");
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
                    return View(userorderview());
                }
            }
            return View(userorderview());
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

        private List<CartItem> userorderview()
        {
            var c = db.CartItems.ToList().Where(i => i.UserId == User.Identity.GetUserId()).Where(o => o.OrderId == null).ToList();
            return c;
        }
    }
}
