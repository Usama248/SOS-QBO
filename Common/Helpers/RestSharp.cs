using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CommonLayer.Helpers
{
    public class RestSharpHelper
    {
        public async Task<RestResponse> SendRequest(Method method, string endPointUrl, List<KeyValuePair<string, string>> headers,string requestType, string body) {
            var client = new RestClient(endPointUrl);
            var request = new RestRequest("", method);
            request.AddHeaders(headers);
            request.AddParameter(requestType, body, ParameterType.RequestBody);
            return await client.ExecuteAsync(request);
        }
    }

}
