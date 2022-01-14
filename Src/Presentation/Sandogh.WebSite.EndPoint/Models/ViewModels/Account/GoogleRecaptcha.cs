using Newtonsoft.Json.Linq;
using Sandogh.Common;
using System.Net.Http;

namespace Sandogh.WebSite.EndPoint.Models.ViewModels.Account
{
    public class GoogleRecaptcha
    {
        private readonly  Configs _configs;
        public GoogleRecaptcha(Configs configs)
        {
            _configs = configs;
        }
        public bool Verify(string googleResponse)
        {
            string sec = _configs.GoogleRecaptchaSecretKey;
            HttpClient httpClient = new HttpClient();
            var result = httpClient.PostAsync($"https://www.google.com/recaptcha/api/siteverify?secret={sec}&response={googleResponse}", null).Result;
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return false;
            }

            string content = result.Content.ReadAsStringAsync().Result;
            dynamic jsonData = JObject.Parse(content);

            if (jsonData.success == "true")
            {
                return true;
            }

            return false;
        }
    }
}
