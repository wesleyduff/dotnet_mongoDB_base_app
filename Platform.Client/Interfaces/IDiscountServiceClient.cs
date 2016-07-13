using Domain;
using System.Threading.Tasks;

namespace Platform.Client.Interfaces
{
    public interface IDiscountServiceClient
    {
        Task<string> CreateDiscount(Discount discount);
        string GetDiscount(string id);
        string GetDiscounts();
        Task<string> DeleteDiscount(string id);
    }
}