using Application.IRepos;
using Application.IServices;
using Common.DTOs;
using Common.DTOs.QB;
using Common.DTOs.QB.Auth;
using CommonLayer.Helpers;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class QBItemsService : IQBItemsService
    {
        private readonly QBAuthDTO _QBAuthDTO;
        private readonly IQBAuthRepo _qBAuthRepository;
        private readonly IQBTokenRepo _qBTokenRepo;
        public QBItemsService(IOptions<QBAuthDTO> QBAuthDTO, IQBAuthRepo qBAuthRepository, IQBTokenRepo qBTokenRepo)
        {
            _qBAuthRepository = qBAuthRepository;
            _QBAuthDTO = new QBAuthDTO
            {
                ClientId = QBAuthDTO.Value.ClientId,
                ClientSecret = QBAuthDTO.Value.ClientSecret,
                Environment = QBAuthDTO.Value.Environment,
                RedirectURL = QBAuthDTO.Value.RedirectURL,
            };
            _qBTokenRepo = qBTokenRepo;
        }
        public async Task<Item> CreateItem(QBItemsRequestDTO request)
        {
            RestSharpHelper restSharpHelper = new RestSharpHelper();
            string companyId = "4620816365250896610";
            string endPoint = _QBAuthDTO.QBBaseURL + "v3/company/"+ companyId + "/item?minorversion=65";
            string authToken = "eyJlbmMiOiJBMTI4Q0JDLUhTMjU2IiwiYWxnIjoiZGlyIn0..BOBLw-vP-6XH7_q2PeCPJA.wkCDpS7BkB4o3DpSQPb8tVPVnH4_fFsrOmBu7fwHnq-Fu8D-xp4OW1wqhmdtOjZXXGh8fNvk8aUpaQCBqEWBy7wuQ7Dz38KjGrIgGh0Z7ZEABN8yMLFZsYQGvyg4KKYOe7j1aILCpcQ8N--cEJmBA5TXVBwqwO60ZANeHuyRCMDwBMNuZjVkipm06d1AMeIYu2-DKkpf6LspOANpBei3acgQofix4fbt3bWISBglbjO2tQec2NJ5-igrzpo1-xUbgXx0EQ1XlyX6hQjIx3oeDmwAUF5ZGryk8_lwsMu4bL3bYIvPJlVN3492ko3-6k2AvIZzlK-P-MJjNpaJDGcic4sSBKHCtDIjaTlhrQqH9K-sVACmbWzUPO7ehewd0nuLqyULl4ImS2VqAH_poxV80fSi4hakI4aQCoXMQutM8wfC629N-XDub60nv6R5_S6ckHRRlCHM_oe9JC4XRSsgpE26u8lhmQGrSHQFj0FeVkLVxs1B9qaUP-rTGBSDrBQ-tDFgg3Qn4rQFnbS2O0HxXUJnX6YtUJkycITDA1glsKnK4eR6d0DSOlZS5_SszcY1oOAWGIgQQtGF3ZBli7fMWmvz7djsMgYDQmKI__hsYh4Hos0xpWIYdSzD4gGZkyEdKRDCq-1FnIHsVhZLll485kYxaZeuJnpfA7W5B67O1bBmOERsm_ZS9DRuHri1_u-logZu0Hb2FzCsYBOhn317CTEGGvE6hVCDEvTbdv-iMc_98RQRp0uAD2Pa2jcYaM3T.Bvo8n7u3S2EEtnG-dQzgQg";
            var json = JsonConvert.SerializeObject(request);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            List<KeyValuePair<string, string>> Headers = new List<KeyValuePair<string, string>>();
            Headers.Add(new KeyValuePair<string, string>("Authorization", "Bearer " + authToken));
            var response = await restSharpHelper.SendRequest(Method.Post, endPoint, Headers, "application/json", data.ToString());
            Item items = JsonConvert.DeserializeObject<Item>(response.Content);
            return items;
        }
    }
}
