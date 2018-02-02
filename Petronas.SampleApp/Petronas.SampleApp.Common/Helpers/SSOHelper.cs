using Newtonsoft.Json;
using Petronas.SampleApp.Common;
using Petronas.SampleApp.Common.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Petronas.SampleApp.Common
{
    public class SSOHelper
    {
        public async Task<Guid> Login(string username,string password,string domain)
        {
            #region Temporary Code to allow dummy password for any user
            
            //if (request.Password.Equals(ConfigurationManager.AppSettings[Constants.Configuration.MockPassword], StringComparison.OrdinalIgnoreCase))
            //{
            //    SSOResponse dummyPasswordResponse = null;

            //    var userResponse = _activeDirectoryHelper.GetUserProfile(request.UserId, request.Domain);
            //    if (userResponse == null)
            //    {
            //        dummyPasswordResponse = new SSOResponse { ErrorCode = Constants.ErrorMessageCodes.InvalidUserName, ErrorMessage = Constants.ErrorMessageCodes.InvalidUserNameMessage };
            //        return dummyPasswordResponse;
            //    }

            //    dummyPasswordResponse = new SSOResponse { AccessToken = Guid.NewGuid() };
            //    return dummyPasswordResponse;
            //}
            #endregion

            SSOResponse token = null;
            System.Net.IPAddress iP = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList[1];
            string hostName = HttpContext.Current.Server.MachineName;
            string userAgentText = HttpContext.Current.Request.Browser.Browser + " " + HttpContext.Current.Request.Browser.Version;
            var information = new LoginRequest()
            {
                Id = ConfigurationManager.AppSettings[Constants.Configuration.ApplicationId],
                ApplicationId = ConfigurationManager.AppSettings[Constants.Configuration.ApplicationId],
                Domain = domain,
                Client = new LoginClient()
                {
                    Ip = iP.ToString(),
                    HostName = hostName,
                    UserAgent = userAgentText,
                },
                UserId = username.Split('@')[0],
                Password = password
            };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings[Constants.Configuration.SSOApiBaseUrl]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Constants.Global.MediaTypeJson));
              
                var result = client.PostAsJsonAsync(ConfigurationManager.AppSettings[Constants.Configuration.SSOWebAuthLogin], information);
                var response = result.Result.Content.ReadAsStringAsync().Result;

                //var result = await client.PostAsJsonAsync(ConfigurationManager.AppSettings[Constants.Configuration.SSOWebAuthLogin], information);
                //var response = result.Content.ReadAsStringAsync().Result;

                //var response = client.PostAsJsonAsync("api/Web/Auth/Login", information);
                //token = await result.Content.ReadAsAsync<SSOResponse>();

                token = JsonConvert.DeserializeObject<SSOResponse>(response);
            }
            return token.AccessToken;
        }
    }
}
