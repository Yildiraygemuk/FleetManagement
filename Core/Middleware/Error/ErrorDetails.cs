using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Core.Extensions
{
    public class ErrorDetails
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }

        public object Data { get; set; }

        public bool Success { get; set; }

        public override string ToString()
        {
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings()
            {
                ContractResolver = contractResolver
            });
        }
    }
}