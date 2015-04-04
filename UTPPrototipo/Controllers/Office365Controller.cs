using Microsoft.Office365.OutlookServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OpenIdConnect;
using System.Web;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows;
using System.Net.Http;
using System.Net.Http.Headers;



namespace UTPPrototipo.Controllers
{
    public class Office365Controller : Controller
    {
        public class CalendarEvent
        {

            public string Subject { get; set; }
            public string Location { get; set; }
            public DateTime Start { get; set; }
            public DateTime End { get; set; }

        }
        private const string ServiceResourceId = "https://outlook.office365.com";
        private static readonly Uri ServiceEndpointUri = new Uri("https://outlook.office365.com/ews/odata");
        private static DiscoveryContext _discoveryContext;

        public async Task<ExchangeClient> GetExchangeClient()
        {
            // Create the discovery context if it doesn't already exist.
            if (_discoveryContext == null)
            {
                _discoveryContext = await DiscoveryContext.CreateAsync();
            }

            // Authenticate and retrieve the tenant ID and user ID.
            var discoverResult = await _discoveryContext.DiscoverResourceAsync(ServiceResourceId);

            string refreshToken = new SessionCache().Read("RefreshToken");

            Microsoft.IdentityModel.Clients.ActiveDirectory.ClientCredential creds =
                new Microsoft.IdentityModel.Clients.ActiveDirectory.ClientCredential(
                    _discoveryContext.AppIdentity.ClientId, _discoveryContext.AppIdentity.ClientSecret);

            return new ExchangeClient(ServiceEndpointUri, async () =>
            {
                // Get the access token based on the refresh token.
                return (await _discoveryContext.AuthenticationContext.AcquireTokenByRefreshTokenAsync(
                    refreshToken, creds, ServiceResourceId)).AccessToken;
            });
        }

        // GET: Office365
        public ActionResult Index()
        {
            return View();
        }
    }
}