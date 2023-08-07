namespace AuctionApi.Models
{
    public class AuctionDto
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Category { get; set; }
        public string UserFullName { get; set; }
    }
}
