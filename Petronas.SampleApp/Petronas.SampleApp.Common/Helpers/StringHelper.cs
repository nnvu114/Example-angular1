using System;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Petronas.SampleApp.Common.Helpers
{
    public static class StringHelper
    {
        public static string ToUrlFriendly(this string s)
        {
            if (s == null) return s;

            s = s.Normalize(NormalizationForm.FormD);
            s = s.Trim().ToLower().Replace('đ', 'd');

            var result = new StringBuilder();
            bool flag = false;
            foreach (var t in s.Where(t => t <= 300))
            {
                if (t >= 'a' && t <= 'z' || t >= '0' && t <= '9')
                {
                    result.Append(t);
                    flag = true;
                }
                else if (flag)
                {
                    result.Append('-');
                    flag = false;
                }

            }

            // Remove last - character if neccessary
            if (result.Length > 0 && result[result.Length - 1] == '-')
                result.Remove(result.Length - 1, 1);


            return result.ToString();
        }

        public static bool IsPropertyExist(dynamic obj, string name)
        {
            JObject jObject = JObject.Parse(obj.ToString());

            return jObject.Property(name) != null;
        }

        public static string GetErrorMessage(this string value, Exception exception)
        {
            return string.Format("{0}{1}: {2}\r\n{3}\n", value, DateTime.Now, exception.Message, exception.StackTrace);
        }

        public static string GetErrorMessage(this string value, string prefix, Exception exception)
        {
            return string.Format("{0}{1}: [{2}] - {3}\r\n{4}\n", value, DateTime.Now, prefix, exception.Message, exception.StackTrace);
        }

        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source != null && toCheck != null && source.IndexOf(toCheck, comp) >= 0;
        }
    }
}