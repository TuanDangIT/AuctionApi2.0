using AuctionApi.Models;

namespace AuctionApi.Services.Interfaces
{
    public interface IAuctionService
    {
        public List<AuctionDto> GetAll();
        public AuctionDto GetAuction(int id);
        public void DeleteAuction(int id);
        public void UpdateAuction(int id, UpdateAuctionDto dto);
        public void Create(CreateAuctionDto dto);
    }
}
