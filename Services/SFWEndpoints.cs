using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Nekos.Net;
using Nekos.Net.Responses;

namespace hikari.net.Services
{
    class SFWEndpoints
    {
        private readonly HttpClient _http;

        public SFWEndpoints(HttpClient http)
            => _http = http;
        
    }
}
