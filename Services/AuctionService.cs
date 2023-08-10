﻿using AuctionApi.Authorization;
using AuctionApi.Enums;
using AuctionApi.MappingServices;
using AuctionApi.Models;
using AuctionApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace AuctionApi.Services
{
    public class AuctionService : IAuctionService
    {
        private readonly AuctionDbContext _context;
        private readonly IUserContextService _userContextService;
        private readonly IAuthorizationService _authorizationService;
        public AuctionService(AuctionDbContext context, IUserContextService userContextService, IAuthorizationService authorizationService)
        {
            _context = context;
            _userContextService = userContextService;
            _authorizationService = authorizationService;
        }

        public List<AuctionDto> GetAll()
        {
            var auctions = _context.Auctions
                .Include(a => a.User)
                .Include(a => a.Category)
                .ToList();
            List<AuctionDto> auctionsDto = new List<AuctionDto>();
            auctions.ForEach(auction => auctionsDto.Add(AuctionServiceMapper.AuctionMapToAuctionDto(auction)));
            return auctionsDto;
        }

        public AuctionDto GetById(int id)
        {
            var auction = _context.Auctions
                .Include(a => a.User)
                .Include(a => a.Category)
                .FirstOrDefault(a => a.Id == id);
            AuctionNotFoundExceptionChecker(auction);
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, auction, new ResourceOperationRequirement(ResourceOperation.Delete)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException("Access forbidden");
            }
            var auctionDto = AuctionServiceMapper.AuctionMapToAuctionDto(auction);

            return auctionDto;
        }

        public void DeleteById(int id)
        {
            var auction = _context.Auctions
                .FirstOrDefault(a => a.Id == id);
            AuctionNotFoundExceptionChecker(auction);
            _context.Auctions.Remove(auction);
            _context.SaveChanges();
        }

        public void UpdateById(int id, UpdateAuctionDto dto)
        {
            var auction = _context.Auctions
                .Include(a => a.Category)
                .FirstOrDefault(a => a.Id == id);

            AuctionNotFoundExceptionChecker(auction);
            if(dto.Title != null) auction.Title = dto.Title;
            if (dto.Description != null) auction.Description = dto.Description;
            if (dto.Price != default) auction.Price = dto.Price;
            if (dto.Category != null)
            {
                var newCategory = _context.Categories
                .FirstOrDefault(a => a.Name == dto.Category);
                auction.Category.Id = newCategory.Id;
            }
            auction.UpgradeDate = DateTime.Now;
            _context.SaveChanges();
        }
        public void Create(CreateAuctionDto dto)
        {
            var auction = AuctionServiceMapper.CreateAuctionDtoMapToAuction(dto);
            auction.UserId = _userContextService.GetUserId;
            _context.Auctions.Add(auction);
            _context.SaveChanges();
        }
        
       
        private void AuctionNotFoundExceptionChecker(Auction? auction)
        {
            if(auction == null)
            {
                throw new NotFoundException("Auction not found");
            }
        }


    }
}
