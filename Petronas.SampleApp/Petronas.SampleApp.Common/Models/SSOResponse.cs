using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petronas.SampleApp.Common.Models
{
    public class SSOResponse
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public Guid AccessToken { get; set; }
    }
}
