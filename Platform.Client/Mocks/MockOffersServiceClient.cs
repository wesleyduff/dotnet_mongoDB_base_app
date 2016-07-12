using System.Threading.Tasks;
using Platform.Client.DataContracts.Offers;

namespace Platform.Client.Mocks
{
    public interface IOfferMockServiceClient
    {
        Task<OffersDataContract> GetOffers();
    }


    public class MockOffersServiceClient : IOfferMockServiceClient
    {
        public async Task<OffersDataContract> GetOffers()
        {
            var result = await Task.Run(() => Utils.ReadJsonFileAndDeserialize<OffersDataContract>("offers.json"));
            return result;
        }
    }
}
