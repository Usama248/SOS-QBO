using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs.SOS
{
    public class SOSAccessTokenCredentialsDTO
    {
        public string request { get; set; }
        public string content_Type { get; set; }
        public string host { get; set; }
        public string grant_Type { get; set; }
        public string redirect_Url { get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }
    }
}
