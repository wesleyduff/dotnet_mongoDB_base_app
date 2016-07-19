using System.Threading.Tasks;
using Domain;
using Platform.Client.Interfaces;
using Newtonsoft.Json.Linq;

namespace BikeStoreApi.Interfaces
{
    public interface IDistributorComposer : ICrud<Distributor, string>
    {
        Task<JObject> UpdateRecieptTypes(UpdateRecieptTypes postUpdate);
        JObject GetReceiptTypes();
        Task<JObject> UpdateRecieptList(string distributorId, UdateReceiptList postData);
    }
}