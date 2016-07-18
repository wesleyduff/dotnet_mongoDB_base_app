using Domain;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace BikeStoreApi.Interfaces
{
    public interface IDiscountComposer : ICrud<Discount>
    {
        void compose();
    }
}