using MongoDB.Bson;
using System.Threading.Tasks;
using Domain;
using Newtonsoft.Json.Linq;

namespace Platform.Client.Interfaces
{
    public interface IDistributorsServiceClient : ICrud<Distributor, ObjectId>
    {
        Task<JObject> AddOfferToDistributer(string distributorId, Offers offer);
        Task<JObject> UpdateRecieptTypes(UpdateRecieptTypes postUpdate);
        JObject GetReceiptTypes();
        Task<JObject> UpdateRecieptList(string distributorId, UdateReceiptList postData);
    }
}
