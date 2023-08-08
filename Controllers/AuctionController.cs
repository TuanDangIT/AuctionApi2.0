using AuctionApi.Models;
using AuctionApi.Services.Interfaces;

namespace AuctionApi.Controllers
{
    [Route("api/auction")]
    [ApiController]
    public class AuctionController : ControllerBase
    {
        private readonly IAuctionService _auctionService;
        public AuctionController(IAuctionService auctionService)
        {
            _auctionService = auctionService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AuctionDto>> GetAll()
        {
            var auctions = _auctionService.GetAll();
            return Ok(auctions);
        }

        [HttpGet("{id}")]
        public ActionResult<AuctionDto> GetById([FromRoute] int id)
        {
            var auction = _auctionService.GetById(id);
            return Ok(auction);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteById([FromRoute] int id)
        {
            _auctionService.DeleteById(id);
            return NoContent();
        }
        [HttpPut("{id}")]
        public ActionResult UpdateById([FromRoute] int id, [FromBody]UpdateAuctionDto dto)
        {
            _auctionService.UpdateById(id, dto);
            return NoContent();
        }
        [HttpPost]
        public ActionResult Create([FromBody]CreateAuctionDto dto)
        {
            _auctionService.Create(dto);
            return NoContent();
        }
        

    }
}
