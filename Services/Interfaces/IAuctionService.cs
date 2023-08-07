using AuctionApi.Models;

namespace AuctionApi.Services.Interfaces
{
    public interface IAuctionService
    {
        public List<AuctionDto> GetAll();
        public AuctionDto GetById(int id);
        public void DeleteById(int id);
        public void UpdateById(int id, UpdateAuctionDto dto);
        public void Create(CreateAuctionDto dto);
    }
}
