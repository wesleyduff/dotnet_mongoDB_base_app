using BikeStoreApi.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace BikeStoreApi.Controllers
{
    public class DiscountsController : ApiController
    {
        private readonly IDiscountComposer discountComposer;

        public DiscountsController(IDiscountComposer discountComposer)
        {
            this.discountComposer = discountComposer;
        }

        public string Get()
        {
            return discountComposer.GetDiscounts();
        }

        public string Get(string id)
        {
            return discountComposer.GetDiscount(id);
        }

        public Task<string> Create(Discount discount)
        {
            return discountComposer.CreateDiscount(discount);
        }

        public Task<string> Delete(string id)
        {
            return discountComposer.DeleteDiscount(id);

        }
    }
}
