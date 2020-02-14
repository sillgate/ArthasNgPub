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
    public class OrderController : Controller
    {
        private ArthasPubDB db = new ArthasPubDB();

        // GET: Order
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {

                //{
                //    List<Order> model = new List<Order>();
                //    foreach (Order order in db.Orders.Include(u => u.User).Include(c => c.CartItems).ToList())
                //    {
                //        model.Add(new Order 
                //        {
                //            OrderId = order.OrderId,
                //            CreateDate = order.CreateDate,
                //            CartItems = db.CartItems.Where(o => o.OrderId == order.OrderId).ToList()
                //        });
                //    }

                //    return View(model);
                //}


                return View(db.Orders.Include(c => c.CartItems.Select(i => i.Item)).Include(u => u.User));

                //return View(db.CartItems.Where(i => i.OrderId != null).Include(o => o.Order).Include(u => u.User).Include(i => i.Item));
            }


            else
            {
                return View(db.Orders.ToList().Where(i => i.UserId == User.Identity.GetUserId()));
            }
        }

        //// GET: Order/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Order order = db.Orders.Find(id);
        //    if (order == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(order);
        //}

        //// GET: Order/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Order/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "OrderId,UserId,Total,CreateDate")] Order order)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        order.UserId = User.Identity.GetUserId();

        //        db.Orders.Add(order);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(order);
        //}

        //// GET: Order/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Order order = db.Orders.Find(id);
        //    if (order == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(order);
        //}

        //// POST: Order/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "UserId,Total,CreateDate")] Order order)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        order.UserId = User.Identity.GetUserId();
        //        db.Entry(order).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(order);
        //}

        //// GET: Order/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Order order = db.Orders.Find(id);
        //    if (order == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(order);
        //}

        //// POST: Order/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Order order = db.Orders.Find(id);
        //    db.Orders.Remove(order);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
