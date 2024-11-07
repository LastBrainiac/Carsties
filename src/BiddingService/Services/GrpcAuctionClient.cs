using AuctionService.protos;
using BiddingService.Models;
using Grpc.Net.Client;

namespace BiddingService.Services
{
    public class GrpcAuctionClient(ILogger<GrpcAuctionClient> _logger, IConfiguration _config)
    {
        public Auction GetAuction(string id)
        {
            _logger.LogInformation("Calling gRPC service");
            var channel = GrpcChannel.ForAddress(_config["GrpcAuction"]);
            var client = new GrpcAuction.GrpcAuctionClient(channel);
            var request = new GetAuctionRequest { Id = id };

            try
            {
                var reply = client.GetAuction(request);
                var auction = new Auction
                {
                    ID = reply.Auction.Id,
                    AuctionEnd = DateTime.Parse(reply.Auction.AuctionEnd),
                    Seller = reply.Auction.Seller,
                    ReservePrice = reply.Auction.ReservePrice
                };
                return auction;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not call gRPC server");
                return null;
            }
        }
    }
}
