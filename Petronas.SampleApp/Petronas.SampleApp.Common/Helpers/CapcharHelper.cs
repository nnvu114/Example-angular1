using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petronas.SampleApp.Common.Helpers
{
    public class CapcharHelper
    {
        public static bool Validate(string EncodedResponse)
        {
            var client = new System.Net.WebClient();

            string privateKey = ConfigurationManager.AppSettings[Common.Constants.Configuration.CaptchaPrivateKey];

            if (string.IsNullOrEmpty(privateKey))
            {
                return false;
            }

            var googleReply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", privateKey, EncodedResponse));

            var captchaResponse = JsonConvert.DeserializeObject<CapcharHelper>(googleReply);

            return captchaResponse.Success;
        }

        private bool m_Success;
        [JsonProperty("success")]
        public bool Success
        {
            get { return m_Success; }
            set { m_Success = value; }
        }

        private List<string> m_ErrorCodes;
        [JsonProperty("error-codes")]
        public List<string> ErrorCodes
        {
            get { return m_ErrorCodes; }
            set { m_ErrorCodes = value; }
        }      
    }
}
