using Domain;
using Platform.Client.Interfaces;

namespace BikeStoreApi.Interfaces
{
    public interface IDiscountComposer : ICrud<Discount, string>
    {
        void Compose();
    }
}