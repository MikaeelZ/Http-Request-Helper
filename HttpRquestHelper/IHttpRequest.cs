using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Request_Helper.HttpRquestHelper
{
    public interface IHttpRequest
    {
        Task<TResponse> HttpGetAsync<TResponse>(string url, Dictionary<string, string> parameters = null, Dictionary<string, string> headers = null);
        Task<HttpResponseMessage> HttpGetAsync(string url, Dictionary<string, string> headers = null);
        Task<TResponse> HttpPostAsync<TRequest, TResponse>(TRequest requestParameter, string url, Dictionary<string, string> headers = null);
        Task<HttpResponseMessage> HttpPostAsync<TRequest>(TRequest requestParameter, string url, Dictionary<string, string> headers = null);
        Task<TResponse> HttpPostFormUrlEncodedAsync<TResponse>(Dictionary<string, string> parameters, string url, Dictionary<string, string> headers = null);
        Task<TResponse> HttpPutAsync<TRequest, TResponse>(TRequest requestParameter, string url, Dictionary<string, string> headers = null);
        Task<TResponse> HttpPatchAsync<TRequest, TResponse>(TRequest requestParameter, string url, Dictionary<string, string> headers = null);
    }

    public class HttpRequest : IHttpRequest
    {
        private readonly HttpClient _httpClient;

        public HttpRequest()
        {
            _httpClient = new HttpClient();
        }

        private void AddHeader(Dictionary<string, string> header = null)
        {
            _httpClient.DefaultRequestHeaders.Clear();

            if (header != null)
                foreach (var item in header)
                {
                    _httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
        }
        public async Task<TResponse> HttpGetAsync<TResponse>(string url, Dictionary<string, string> parameters = null, Dictionary<string, string> headers = null)
        {
            AddHeader(headers);

            string uri = url;
            if (parameters != null)
            {
                uri += "?";
                foreach (var item in parameters)
                {
                    uri += item.Key + "=" + item.Value + "&";
                }
            }

            HttpResponseMessage response = await _httpClient.GetAsync(uri);

            string responsee = await response.Content.ReadAsStringAsync();

            TResponse res = JsonConvert.DeserializeObject<TResponse>(responsee);

            return res;
        }
        public async Task<HttpResponseMessage> HttpGetAsync(string url, Dictionary<string, string> headers = null)
        {
            AddHeader(headers);

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            // string responsee = await response.Content.ReadAsStringAsync();

            //TResponse res = JsonConvert.DeserializeObject<TResponse>(responsee);

            return response;
        }
        public async Task<TResponse> HttpPostAsync<TRequest, TResponse>(TRequest requestParameter, string url, Dictionary<string, string> headers = null)
        {
            AddHeader(headers);

            string JsonPaymentCode = JsonConvert.SerializeObject(requestParameter);

            StringContent content = new StringContent(JsonPaymentCode, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(url, content);

            string responsee = await response.Content.ReadAsStringAsync();

            TResponse res = JsonConvert.DeserializeObject<TResponse>(responsee);

            return res;
        }
        public async Task<HttpResponseMessage> HttpPostAsync<TRequest>(TRequest requestParameter, string url, Dictionary<string, string> headers = null)
        {
            AddHeader(headers);

            string JsonPaymentCode = JsonConvert.SerializeObject(requestParameter);

            StringContent content = new StringContent(JsonPaymentCode, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(url, content);

            //string responsee = await response.Content.ReadAsStringAsync();

            //TResponse res = JsonConvert.DeserializeObject<TResponse>(responsee);

            return response;
        }
        public async Task<TResponse> HttpPostFormUrlEncodedAsync<TResponse>(Dictionary<string, string> parameters, string url, Dictionary<string, string> headers = null)
        {
            AddHeader(headers);

            var nameValueCollection = new List<KeyValuePair<string, string>>();
            foreach (var item in parameters)
                nameValueCollection.Add(new KeyValuePair<string, string>(item.Key, item.Value));

            FormUrlEncodedContent requestContent = new FormUrlEncodedContent(nameValueCollection);
            HttpResponseMessage response = await _httpClient.PostAsync(url, requestContent);

            string responsee = await response.Content.ReadAsStringAsync();

            TResponse res = JsonConvert.DeserializeObject<TResponse>(responsee);

            return res;
        }
        public async Task<TResponse> HttpPutAsync<TRequest, TResponse>(TRequest requestParameter, string url, Dictionary<string, string> headers = null)
        {
            AddHeader(headers);

            string JsonPaymentCode = JsonConvert.SerializeObject(requestParameter);

            StringContent content = new StringContent(JsonPaymentCode, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync(url, content);

            string responsee = await response.Content.ReadAsStringAsync();

            TResponse res = JsonConvert.DeserializeObject<TResponse>(responsee);

            return res;
        }
        public async Task<TResponse> HttpPatchAsync<TRequest, TResponse>(TRequest requestParameter, string url, Dictionary<string, string> headers = null)
        {
            AddHeader(headers);

            string JsonPaymentCode = JsonConvert.SerializeObject(requestParameter);

            StringContent content = new StringContent(JsonPaymentCode, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PatchAsync(url, content);

            string responsee = await response.Content.ReadAsStringAsync();

            TResponse res = JsonConvert.DeserializeObject<TResponse>(responsee);

            return res;
        }
    }
}