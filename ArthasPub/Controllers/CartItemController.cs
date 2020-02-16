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


        //Retrieve the Ordered item list
        public ActionResult Index()
        {
            db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            //View for member to check the item they ordered
            if (User.IsInRole("Member"))
            {
                return View(customerview());
            }
            //View for Admin to check the ordered item for all member
            if (User.IsInRole("Admin"))
            {
                return View(db.CartItems.Include(u => u.User).ToList());
            }
            return RedirectToRoute("Index");
        }

        //Go to checkbill view for Member
        [Authorize(Roles = "Member")]
        public ActionResult Checkout()
        {
            db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            return View(db.CartItems.ToList().Where(i => i.UserId == User.Identity.GetUserId()).Where(o => o.OrderId == null).Where(c => c.Cancel == false).ToList());
        }

        //Commit the checkbill for Member
        [Authorize(Roles = "Member")]
        [HttpPost]
        public ActionResult Checkout(List<CartItem> cart)
        {
            db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
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
                //Clear cancel item after checkout completed
                var cancelitem = db.CartItems.ToList().Where(i => i.UserId == User.Identity.GetUserId()).Where(c => c.Cancel == true).ToList();
                foreach (var i in cancelitem)
                {
                    db.CartItems.Remove(i);
                    db.SaveChanges();
                }

            }
            return RedirectToAction("Index", "Item");
        }

        //Return Cancel View
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

        // Perform Cancel
        [HttpPost, ActionName("Cancel")]
        public ActionResult Cancel(int id)
        {
            CartItem item = db.CartItems.Find(id);
            item.Cancel = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        //Common function for calling customer view
        private List<CartItem> customerview()
        {
            db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            var result = db.CartItems.ToList().Where(i => i.UserId == User.Identity.GetUserId()).Where(o => o.OrderId == null).ToList();
            return result;
        }
    }
}
