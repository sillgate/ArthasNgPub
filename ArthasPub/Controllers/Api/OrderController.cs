using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ArthasPub.DAL;
using ArthasPub.Models;

namespace ArthasPub.Controllers.Api
{
    public class OrderController : ApiController
    {
        private ArthasPubDB db = new ArthasPubDB();

        // GET: api/Order
        public IQueryable<Order> GetOrders()
        {
            return db.Orders;
        }

        // GET: api/Order/5
        [ResponseType(typeof(Order))]
        public IHttpActionResult GetOrder(int id)
        {
            Order Order = db.Orders.Find(id);
            if (Order == null)
            {
                return NotFound();
            }

            return Ok(Order);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderExists(int id)
        {
            return db.Orders.Count(e => e.OrderId == id) > 0;
        }
    }
}