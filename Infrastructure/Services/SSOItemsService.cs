using Application.IServices;
using Common.DTOs.SOS;
using CommonLayer.Helpers;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class SSOItemsService : ISSOItemsService
    {
        public async Task<SOSItemResponseDTO> getAllSSOItemsAsync()
        {
            RestSharpHelper restSharpHelper = new RestSharpHelper();
            string endPoint = "https://api.sosinventory.com/api/v2/item";
            string authToken = "Hr6kM5yH3tEog-4VuG_5np_-7CFyzZivCvAKsYq-ScMQP0M-j_djWLmfszqw2yjpDRArd0PBUH6emynfoUaHQ83Sr2lZPiHF36ES5sn-m5rS59kp8TFCa5JqT78FDdYabninqYb7gPhQrx9B4Wg1gSQzNAbISWRvYdBUBiffJxOng2f3ltjTWUuPo_WfTJFRiKmROev3eDCEi3N6A_5aUDh7vHCTyRB7dEFsKfzODgerQPcjLKdDpvVLxYYtxmhyUyH9VoqeXTqmw27yLzaXExCLt5-hOHWKm8cOcY4VOq3Xm_-H";
            List<KeyValuePair<string, string>> Headers = new List<KeyValuePair<string, string>>();
            Headers.Add(new KeyValuePair<string, string>("Authorization", "Bearer " + authToken));
            var response = await restSharpHelper.SendRequest(Method.Get, endPoint, Headers, "application/json", "");
            SOSItemResponseDTO items = JsonConvert.DeserializeObject<SOSItemResponseDTO>(response.Content);
            return items;
        }
    }
}
