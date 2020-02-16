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
    public class ItemController : ApiController
    {
       private ArthasPubDB db = new ArthasPubDB();

        // GET: api/Item
        public IQueryable<Item> GetItems()
        {
            return db.Items;
        }

        // GET: api/Item/5
        [ResponseType(typeof(Item))]
        public IHttpActionResult GetItem(int id)
        {
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // PUT: api/Item/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutItem(int id, Item item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != item.ItemId)
            {
                return BadRequest();
            }

            db.Entry(item).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Item

            
        [ResponseType(typeof(Item))]
        /*
        public IHttpActionResult PostItem(Item item)
         {

            db.Items = 
             if (!ModelState.IsValid)
             {
                 return BadRequest(ModelState);
             }

             db.Items.Add(item);
             db.SaveChanges();

             return CreatedAtRoute("DefaultApi", new { id = item.ItemId }, item);
         }
        */
        
         public IHttpActionResult PostItem(Models.Item item)
         {
             if (!ModelState.IsValid)
                 return BadRequest("Invalid data.");



            using (var ctx = new ArthasPubDB())
             {
                if(item != null) { 
                 ctx.Items.Add(new Item()
                 {
                     Name = item.Name,
                     Description = item.Description,
                     Price = item.Price,
                     Cost = item.Cost,
                     ItemImageUrl = item.ItemImageUrl,
                     Disable = item.Disable
                 });
                
                ctx.SaveChanges();
                }
            }


             return Ok();
         }
         


        // DELETE: api/Item/5
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid student id");

            using (var ctx = new ArthasPubDB())
            {
                var student = ctx.Items
                    .Where(s => s.ItemId == id)
                    .FirstOrDefault();

                ctx.Entry(student).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ItemExists(int id)
        {
            return db.Items.Count(e => e.ItemId == id) > 0;
        }
    }
}