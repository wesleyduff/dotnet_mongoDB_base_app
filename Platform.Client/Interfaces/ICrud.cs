using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Platform.Client.Interfaces
{
    public interface ICrud<T>
    {
        Task<JObject> Creat(T item);
        Task<JObject> Delete(string itemId);
        JObject Get();
        JObject Get(string itemId);
        Task<JObject> Update(T item);

    }
}
