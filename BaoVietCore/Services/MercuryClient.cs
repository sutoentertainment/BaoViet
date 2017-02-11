using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoVietCore.Services
{
    public class MercuryClient : WebService
    {
        public MercuryClient(Manager man, String apiKey) : base(man)
        {
            this.Client.DefaultRequestHeaders.Add("x-api-key", apiKey);
        }
    }
}
