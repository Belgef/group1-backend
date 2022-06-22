using AutoMapper;
using MarketplaceBackend.Common.Mappings;
using MarketplaceBackend.Models;

namespace MarketplaceBackend.Contracts.V1.Responses.Orders
{
    public class OrderBriefResponse : IMapFrom<Order>
    {
        public int Id { get; set; }

        public DateTime OnDate { get; set; }

        public decimal TotalCost { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Order, OrderBriefResponse>()
                .ForMember(d => d.TotalCost, opt => opt.MapFrom(s => s.OrderProducts.Aggregate((decimal)0, (acc, op) => acc + op.Quantity * op.UnitPrice)));
        }
    }
}
