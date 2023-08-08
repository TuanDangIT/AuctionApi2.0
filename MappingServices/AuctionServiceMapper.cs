using AuctionApi.Models;

namespace AuctionApi.MappingServices
{

    public class AuctionServiceMapper
    {
        private readonly AuctionDbContext _context;
        public AuctionServiceMapper(AuctionDbContext context)
        {
            _context = context;
        }

        public static AuctionDto AuctionMapToAuctionDto(Auction auction)
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


        public static Auction CreateAuctionDtoMapToAuction(CreateAuctionDto createDto)
        {
            var auction = new Auction()
            {
                Title = createDto.Title,
                Price = createDto.Price,
                Description = createDto.Description,
                CategoryId = createDto.CategoryId,
                UserId = createDto.UserId,
                CreatedDate = createDto.CreatedDate
            };
            return auction;

        }
    }
}
