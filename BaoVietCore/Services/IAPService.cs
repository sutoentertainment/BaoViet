using BaoVietCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Store;

namespace BaoVietCore.Services
{
    public class IAPService : ServiceBase, IIAPService
    {
        private LicenseInformation licenseInformation;

        public IAPService(Manager man) : base(man)
        {

        }

        public void Init()
        {
            // some app initialization functions 

            // Get the license info
            // The next line is commented out for testing.

            licenseInformation = CurrentApp.LicenseInformation;

            // The next line is commented out for production/release.       
            //licenseInformation = CurrentAppSimulator.LicenseInformation;

        }

        public bool CheckProduct(string name)
        {
            if (licenseInformation.ProductLicenses[name].IsActive)
            {
                // the customer can access this feature
                return true;
            }
            else
            {
                // the customer can't access this feature
                return false;
            }

        }

        public IReadOnlyDictionary<System.String, ProductLicense> QueryProduct()
        {
            return licenseInformation.ProductLicenses;
        }

        public async Task<bool> BuyProduct(string name)
        {
            var result = await CurrentApp.RequestProductPurchaseAsync(name);
            if (result.Status == ProductPurchaseStatus.Succeeded)
                return true;
            return false;
        }
    }


}
