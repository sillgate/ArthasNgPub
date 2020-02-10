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
            return View(db.CartItems.ToList().Where(i => i.UserId == User.Identity.GetUserId()).Where(i => i.Cancel == false).ToList());
        }

        // GET: Checkout
        //public ActionResult Checkout()
        //{
        //    return View(db.CartItems.ToList().Where(i => i.UserId == User.Identity.GetUserId()).Where(i => i.Cancel == false).ToList());
        //}

        //POST: Checkout
        //[HttpPost]
        //public ActionResult Checkout(List<CartItem> cart)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        decimal t = 0;
        //        foreach (var i in cart)
        //        {
        //            t += i.Total;
        //        }
        //        Order order = new Order();
        //        order.UserId = User.Identity.GetUserId();
        //        order.CreateDate = System.DateTime.Now;
        //        order.Total = t;
        //        System.Diagnostics.Debug.WriteLine(t);
        //        System.Diagnostics.Debug.WriteLine("hihi");
        //        System.Diagnostics.Debug.WriteLine(order.OrderId);
        //        System.Diagnostics.Debug.WriteLine(order.UserId);
        //        db.Orders.Add(order);
        //        db.SaveChanges();
        //        foreach (var i in cart)
        //        {
        //            var rs = db.CartItems.SingleOrDefault(b => b.CartItemId == i.CartItemId);
        //            if (rs != null)
        //            {
        //                rs.OrderId = order.OrderId;
        //                db.SaveChanges();
        //            }
        //        }
        //    }
        //    return View(cart);
        //}

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
                    c.Cancel = i.Cancel;
                    db.SaveChanges();
                    return View(cart);
                }
            }
            return View(cart);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
