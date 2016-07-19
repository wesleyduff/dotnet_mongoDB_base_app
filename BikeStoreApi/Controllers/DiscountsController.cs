using BikeStoreApi.Interfaces;
using Domain;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace BikeStoreApi.Controllers
{
    public class DiscountsController : ApiController
    {
        private readonly IDiscountComposer _composer;

        public DiscountsController(IDiscountComposer composer)
        {
            _composer = composer;
        }

        public JObject Get()
        {
            return _composer.Get();
        }

        public JObject Get(string id)
        {
            return _composer.Get(id);
        }

        public async Task<JObject> Create(Discount discount)
        {
            return await _composer.Create(discount);
        }

        public async Task<JObject> Delete(string id)
        {
            return await _composer.Delete(id);
        }
    }
}
