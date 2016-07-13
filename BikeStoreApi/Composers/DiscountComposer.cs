using BikeStoreApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;
using System.Threading.Tasks;
using Platform.Client.Interfaces;

namespace BikeStoreApi.Composers
{
    public class DiscountComposer : IDiscountComposer
    {
        private readonly IDiscountServiceClient _discountService;

        public DiscountComposer(IDiscountServiceClient discountServiceClient)
        {
            _discountService = discountServiceClient;
        }

        public Task<string> CreateDiscount(Discount discount)
        {
             return _discountService.CreateDiscount(discount);
        }

        public Task<string> DeleteDiscount(string id)
        {
            return _discountService.DeleteDiscount(id);
        }

        public string GetDiscount(string id)
        {
            return _discountService.GetDiscount(id);
        }

        public string GetDiscounts()
        {
            return _discountService.GetDiscounts();
        }
    }
}