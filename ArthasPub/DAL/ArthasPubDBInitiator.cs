using ArthasPub.Models;
using System.Collections.Generic;


namespace ArthasPub.DAL
{
    public class ArthasPubDBInitiator : System.Data.Entity.CreateDatabaseIfNotExists<ArthasPubDB>
    {
        //Seed 4 items to edit and use
        protected override void Seed(ArthasPubDB context)
        {
            var items = new List<Item>
            {
                new Item{ItemId=1, Name="Beer", Description="Tap Beer", Price=2m, Cost=1m, Disable=false},
                new Item{ItemId=2, Name="Red Wine", Description="House Red", Price=5m, Cost=2m, Disable=false},
                new Item{ItemId=3, Name="White Wine", Description="Sparkling", Price=3m, Cost=1.5m, Disable=false},
                new Item{ItemId=4, Name="Burger Set", Description="Burger with Fries", Price=15m, Cost=6m, Disable=false}
            };
            items.ForEach(s => context.Items.Add(s));
            context.SaveChanges();
        }
    }

}