﻿using AuctionService.Data;
using AuctionService.protos;
using Grpc.Core;

namespace AuctionService.Services
{
    public class GrpcAuctionService(AuctionDbContext _dbContext) : GrpcAuction.GrpcAuctionBase
    {
        public override async Task<GrpcAuctionResponse> GetAuction(GetAuctionRequest request, ServerCallContext context)
        {
            Console.WriteLine("==> Received Grpc request for Auction");

            var auction = await _dbContext.Auctions.FindAsync(Guid.Parse(request.Id)) 
                ?? throw new RpcException(new Status(StatusCode.NotFound, "Not found"));

            var response = new GrpcAuctionResponse
            {
                Auction = new GrpcAuctionModel
                {
                    AuctionEnd = auction.AuctionEnd.ToString(),
                    Id = auction.Id.ToString(),
                    ReservePrice = auction.ReservePrice,
                    Seller = auction.Seller
                }
            };

            return response;
        }
    }
}