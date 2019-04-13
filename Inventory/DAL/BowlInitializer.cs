using Inventory.Models;
using System.Collections.Generic;


namespace Inventory.DAL
{
    public class BowlInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<BowlContext>
    {


        protected override void Seed(BowlContext context)
        {
            var bowlsizes = new List<BowlSize>
            {
                new BowlSize() {Id = 1, Size = "00"},
                new BowlSize() {Id = 2, Size = "0"},
                new BowlSize() {Id = 3, Size = "1"},
                new BowlSize() {Id = 4, Size = "2"},
                new BowlSize() {Id = 5, Size = "3"},
                new BowlSize() {Id = 6, Size = "4"},
                new BowlSize() {Id = 7, Size = "5"},
                new BowlSize() {Id = 8, Size = "6"},
                new BowlSize() {Id = 9, Size = "7"}
            };
            bowlsizes.ForEach(s => context.Bowlsizes.Add(s));

            var biases = new List<Bias>()
            {
                new Bias() {Id = 1, BiasSize = "Narrow"},
                new Bias() {Id = 2, BiasSize = "Regular"},
                new Bias() {Id = 3, BiasSize = "Wide"}
            };
            biases.ForEach(s => context.Biases.Add(s));

            var weights = new List<Weight>()
            {
                new Weight() {Id=1, BowlWeight = "Light"},
                new Weight() {Id=1, BowlWeight = "Regular"},
                new Weight() {Id=1, BowlWeight = "Heavy"}
            };
            weights.ForEach(s => context.Weights.Add(s));
            context.SaveChanges();

        }
    }
}