using System;
using System.Collections.Generic;
using System.Data.Common;
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
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private ArthasPubDB db = new ArthasPubDB();

        // Retrieve the sales report to user
        public ActionResult Index()
        {
            db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            if (User.IsInRole("Admin"))
            {
                return View(db.Orders.Include(c => c.CartItems.Select(i => i.Item)).Include(u => u.User));
            }
            else
            {
                return View(db.Orders.ToList().Where(i => i.UserId == User.Identity.GetUserId()));
            }
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
