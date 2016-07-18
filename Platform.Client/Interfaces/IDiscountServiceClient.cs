using Domain;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Platform.Client.Interfaces
{
    public interface IDiscountServiceClient : ICrud<Discount, ObjectId>
    {

    }
}