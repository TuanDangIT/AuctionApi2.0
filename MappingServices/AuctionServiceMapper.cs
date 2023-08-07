using AuctionApi.Models;

namespace AuctionApi.MappingServices
{
<<<<<<< HEAD
    public class AuctionServiceMapper
    {
        private readonly AuctionDbContext _context;
        public AuctionServiceMapper(AuctionDbContext context)
        {
            _context = context;
        }
=======
    public class RestaurantServiceMapper
    {
>>>>>>> b96c9497c06ca39c3e049f6e4c693ca4715fb2ea
        public static AuctionDto MapToAuctionDto(Auction auction)
        {
            return new AuctionDto()
            {
                Title = auction.Title,
                Price = auction.Price,
                Description = auction.Description,
                CreatedDate = auction.CreatedDate,
                UserFullName = auction.User.FirstName + " " + auction.User.LastName,
                Category = auction.Category.Name
            };
        }
<<<<<<< HEAD

        public static Auction MapToAuction(CreateAuctionDto dto)
        {
            var auction = new Auction()
            {
                Title = dto.Title,
                Price = dto.Price,
                Description = dto.Description,
                CategoryId = dto.CategoryId,
                UserId = dto.UserId,
                CreatedDate = dto.CreatedDate
            };
            return auction;
        }
=======
>>>>>>> b96c9497c06ca39c3e049f6e4c693ca4715fb2ea
    }
}
