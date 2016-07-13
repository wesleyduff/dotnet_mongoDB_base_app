using Domain;
using System.Threading.Tasks;

namespace BikeStoreApi.Interfaces
{
    public interface IDiscountComposer
    {

        Task<string> CreateDiscount(Discount discount);
        string GetDiscount(string id);
        string GetDiscounts();
        Task<string> DeleteDiscount(string id);
    }
}