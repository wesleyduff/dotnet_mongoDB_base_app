using Domain;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace BikeStoreApi.Interfaces
{
    public interface IDiscountComposer
    {

        Task<string> CreateDiscount(Discount discount);
        string GetDiscount(string id);
        JObject GetDiscounts();
        Task<string> DeleteDiscount(string id);
    }
}