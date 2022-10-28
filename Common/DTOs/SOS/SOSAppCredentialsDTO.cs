using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs.SOS
{
    public class SOSAppCredentialsDTO
    {
		public string SOSBaseURI { get; set; }
		public string RoutesOAuth2 { get; set; }
		public string ClientId { get; set; }
		public string Authorize { get; set; }
		public string ResponseType { get; set; }
		public string RedirectUrl { get; set; }
		public string ClientSecret { get; set; }
		public string URLToRedirect { get; set; }
	}
}
