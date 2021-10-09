using FaceItStats.Api.Client.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace FaceItStats.Api.Helpers
{
    public static class StringExtension
    {
        public static string FillTemplate(this string template, FaceItResponse response)
        {
            var pattern = new Regex(@"{\w{1,}}", RegexOptions.Multiline);
            var responeString = JsonConvert.SerializeObject(response);
            var responseObject = JObject.Parse(responeString);

            var matches = pattern.Matches(template);

            foreach (Match match in matches)
            {
                var propName = match.Value.Replace("{", string.Empty).Replace("}", string.Empty);
                var newValue = (string)responseObject[propName];
                template = template.Replace(match.Value, newValue);
            }

            return template;
        }
    }
}
