using Domain;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Platform.Client.Interfaces
{
    public interface IDiscountServiceClient
    {
        Task<JObject> CreateDiscount(Discount discount);
        JObject GetDiscount(string id);
        JObject GetDiscounts();
        Task<JObject> DeleteDiscount(string id);
    }
}