using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStoreApi.Interfaces
{
    public interface ICrud<T>
    {
        Task<JObject> Create(T item);
        Task<JObject> Delete(string itemId);
        JObject Get();
        JObject Get(string itemId);
        Task<JObject> Update(T item);

    }
}
