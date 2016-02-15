using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.ApplicationModel.Store;

namespace BaoVietCore.Interfaces
{
    public interface IIAPService
    {
        bool CheckProduct(string name);
        void Init();
        IReadOnlyDictionary<string, ProductLicense> QueryProduct();

        Task<bool> BuyProduct(string name);
    }
}