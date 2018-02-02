using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petronas.SampleApp.Common.Models
{
    public class ResponseBaseModel
    {
        public ResponseBaseModel()
        {
            Data = new DataObject();
        }
        public int statusCode { get; set; }
        public string statusText { get; set; }
        public DataObject Data { get; set; }
        
    }
    public class DataObject
    {
        public int total { get; set; }
        public object data { get; set; }
    }
}
