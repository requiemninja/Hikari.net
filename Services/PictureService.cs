using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Nekos.Net;
using Nekos.Net.Responses;

namespace hikari.net.Services
{
    public class PictureService
    {
        private readonly HttpClient _http;

        public PictureService(HttpClient http)
            => _http = http;

        public async Task<Stream> GetSFWNekoPictureAsync()
        {
            NekosImage image = await NekosClient.GetRandomSfwAsync();
            var resp = await _http.GetAsync(image.FileUrl);
            return await resp.Content.ReadAsStreamAsync();
        }

        public async Task<Stream> GetNSFWNekoPictureAsync()
        {
            NekosImage image = await NekosClient.GetRandomNsfwAsync();
            var resp = await _http.GetAsync(image.FileUrl);
            return await resp.Content.ReadAsStreamAsync();
        }
        //NekosImage image = await NekosClient.GetSfwAsync(Nekos.Net.Endpoints.SfwEndpoint.Cuddle);
    }
}
