using MongoDB.Bson;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Platform.Client.Interfaces
{
    public interface ICrud<T, K>
    {
        Task<JObject> Create(T item);
        Task<JObject> Delete(string itemId);
        JObject Get();
        JObject Get(K itemId);
        Task<JObject> Update(T postUpdate);

    }
}
