using App.Services.Billing.Common.Dtos;
using App.Services.Billing.Data.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
