
namespace AuctionApi.Models
{
    public class CreateAuctionDto
    {

        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int CategoryId { get; set; }
        public int UserId { get; set; }
    }
}
