using InnerCore.Api.DeConz.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace InnerCore.Api.DeConz
{
    /// <summary>
    /// Responsible for communicating with the gateway
    /// </summary>
    public partial class DeConzClient
    {
        private readonly int _parallelRequests = 5;

        private readonly string _ip;

        private readonly int _port;

        /// <summary>
        /// Whitelist ID
        /// </summary>
        private string _appKey;

        /// <summary>
        /// Indicates the DeConzClient is initialized with an AppKey
        /// </summary>
        public bool IsInitialized { get; private set; }

        /// <summary>
        /// Base URL for the API
        /// </summary>
        public string ApiBase
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_appKey))
                    return string.Format("http://{0}:{1}/api/{2}/", _ip, _port, _appKey);
                else
                    return string.Format("http://{0}:{1}/api/", _ip, _port);

            }
        }

        private static HttpClient _httpClient;

        /// <summary>
        /// Initialize with Bridge IP/Port
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public DeConzClient(string ip, int port = 80)
        {
            if (ip == null)
                throw new ArgumentNullException(nameof(ip));

            CheckValidIp(ip, port);

            _ip = ip;
            _port = port;
        }

        /// <summary>
        /// Initialize with Bridge IP/Port and AppKey
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public DeConzClient(string ip, int port, string appKey)
        {
            if (ip == null)
                throw new ArgumentNullException(nameof(ip));

            CheckValidIp(ip, port);

            _ip = ip;
            _port = port;

            //Direct initialization
            Initialize(appKey);
        }

        /// <summary>
        /// Initialize with Bridge IP/Port and AppKey
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="ip"></param>
        public DeConzClient(string ip, string appKey) : this(ip, 80, appKey)
        {

        }

        /// <summary>
        /// Check if the provided IP/Port is valid by using it in an URI to the DeConz Bridge
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        private void CheckValidIp(string ip, int port)
        {
            Uri uri;

            if (!Uri.TryCreate(string.Format("http://{0}:{1}/description.xml", ip, port), UriKind.Absolute, out uri))
            {
                //Invalid ip or hostname caused Uri creation to fail
                throw new Exception(string.Format("The supplied ip to the DeConzClient is not a valid ip: {0}:{1}", ip, port));
            }
        }

        /// <summary>
        /// Initialize client with your app key
        /// </summary>
        /// <param name="appKey"></param>
        public void Initialize(string appKey)
        {
            if (string.IsNullOrEmpty(appKey))
                throw new ArgumentNullException(nameof(appKey));

            _appKey = appKey;

            IsInitialized = true;
        }

        public static Task<HttpClient> GetHttpClient()
        {
            // return per-thread HttpClient
            if (_httpClient == null)
            {
                _httpClient = new HttpClient() { Timeout = TimeSpan.FromSeconds(Constants.TIMEOUT_IN_SECONDS) };
            }

            return Task.FromResult(_httpClient);
        }

        /// <summary>
        /// Check if the DeConzClient is initialized
        /// </summary>
        protected void CheckInitialized()
        {
            if (!IsInitialized)
                throw new InvalidOperationException("DeConzClient is not initialized. First call RegisterAsync or Initialize.");
        }

        /// <summary>
        /// Deserialization helper that can also check for errors
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        protected static T DeserializeResult<T>(string json) where T : class
        {
            try
            {
                T objResult = JsonConvert.DeserializeObject<T>(json);

                return objResult;
            }

            catch
            {
                var defaultResult = DeserializeDefaultDeConzResult(json);

                //We expect an actual object, it was unsuccesful, show error why
                if (defaultResult.HasErrors())
                    throw new Exception(defaultResult.Errors.First().Error.Description);

            }
            return null;
        }

        /// <summary>
        /// Checks if the json contains errors
        /// </summary>
        /// <param name="json"></param>
        private static DeConzResults DeserializeDefaultDeConzResult(string json)
        {
            DeConzResults result = null;

            try
            {
                result = JsonConvert.DeserializeObject<DeConzResults>(json);
            }

            catch (JsonSerializationException)
            {
                //Ignore JsonSerializationException
            }

            return result;
        }

        /// <summary>
        /// Checks if the json contains errors
        /// </summary>
        /// <param name="json"></param>
        private static IReadOnlyCollection<T> DeserializeDefaultDeConzResult<T>(string json)
        {
            List<T> result = null;

            try
            {
                result = JsonConvert.DeserializeObject<List<T>>(json);
            }
            catch (JsonSerializationException)
            {
                //Ignore JsonSerializationException
            }

            return result;
        }
    }
}
