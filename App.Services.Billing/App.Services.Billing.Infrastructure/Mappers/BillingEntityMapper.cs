using App.Services.Billing.Common.Dtos;
using App.Services.Billing.Data.Entities;
using AutoMapper;

namespace App.Services.Billing.Infrastructure.Mappers
{

    public class BillingEntityMapper : Profile
    {
        public BillingEntityMapper()
        {
            this.CreateMap<BillingEntity, BillingDto>();
        }
    }
}
