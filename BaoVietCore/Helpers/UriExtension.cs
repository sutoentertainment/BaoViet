using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoVietCore.Helpers
{
    public static class UriExtension
    {
        public static string ToReadability(this string uri)
        {
            if (uri.ReadabilityCompatible())
                return uri;
            return "https://readability.com/api/content/v1/parser?token=46d1bfeec489836a5a09c1e0f7c110ba6f63c8e5&url=" + uri;
        }

        public static string ToMercury(this string uri)
        {
            if (uri.MercuryCompatible())
                return uri;
            return "https://mercury.postlight.com/parser?url=" + uri;
        }

        public static bool ReadabilityCompatible(this string uri)
        {
            return uri.StartsWith("https://readability.com/api/content/v1/parser?token=46d1bfeec489836a5a09c1e0f7c110ba6f63c8e5&url=");
        }

        public static bool MercuryCompatible(this string uri)
        {
            return uri.StartsWith("https://mercury.postlight.com/parser?url=");
        }
    }
}
