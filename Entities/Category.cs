using System;
using System.Collections.Generic;
namespace AuctionApi.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Auction>? Auctions { get; set; } = new List<Auction>();
    }
}
