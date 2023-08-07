using AuctionApi.Migrations;
using Microsoft.AspNetCore.Identity;

namespace AuctionApi.Bogus
{
    public class DataGenerator
    {
        public static void Seed(AuctionDbContext context)
        {
            string[] categories = { "Electronics", "Cars", "Jeweleries", "Food", "Furnitures" };
            var newUser1 = new User { FirstName = "Adam", LastName = "Sanders", Email = "admin@admin.pl", Balance = 9999, RoleId = 1};
            var newUser2 = new User { FirstName = "Tom", LastName = "Blind", Email = "test@test.pl", Balance = 1000, RoleId = 3 };
            context.Users.AddRange(newUser1, newUser2);
            int i = 0;
            var auctionFaker = new Faker<Auction>()
                .RuleFor(a => a.Title, f => f.Lorem.Word())
                .RuleFor(a => a.Description, f => f.Name.JobDescriptor())
                .RuleFor(a => a.CreatedDate, f => f.Date.Recent(5));
            
            var auctions = auctionFaker.Generate(5);
            newUser2.Auctions = auctions;
            context.Auctions.AddRange(auctions);
            foreach (var category in categories)
            {
                context.Categories.Add(new Category() { Name = category, Auctions = auctions.GetRange(i++, 1) });
            }
            context.SaveChanges();
        }
    }
}