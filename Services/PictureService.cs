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

        public async Task<Stream> GetSFWCuddleAsync()
        {
            NekosImage image = await NekosClient.GetSfwAsync(Nekos.Net.Endpoints.SfwEndpoint.Cuddle);
            var resp = await _http.GetAsync(image.FileUrl);
            return await resp.Content.ReadAsStreamAsync();
        }

        public async Task<Stream> GetSFWTickleAsync()
        {
            NekosImage image = await NekosClient.GetSfwAsync(Nekos.Net.Endpoints.SfwEndpoint.Tickle);
            var resp = await _http.GetAsync(image.FileUrl);
            return await resp.Content.ReadAsStreamAsync();
        }

        public async Task<Stream> GetSFWSlapAsync()
        {
            NekosImage image = await NekosClient.GetSfwAsync(Nekos.Net.Endpoints.SfwEndpoint.Slap);
            var resp = await _http.GetAsync(image.FileUrl);
            return await resp.Content.ReadAsStreamAsync();
        }

        public async Task<Stream> GetSFWPokeAsync()
        {
            NekosImage image = await NekosClient.GetSfwAsync(Nekos.Net.Endpoints.SfwEndpoint.Poke);
            var resp = await _http.GetAsync(image.FileUrl);
            return await resp.Content.ReadAsStreamAsync();
        }

        public async Task<Stream> GetSFWPatAsync()
        {
            NekosImage image = await NekosClient.GetSfwAsync(Nekos.Net.Endpoints.SfwEndpoint.Pat);
            var resp = await _http.GetAsync(image.FileUrl);
            return await resp.Content.ReadAsStreamAsync();
        }

        public async Task<Stream> GetSFWNekoAsync()
        {
            NekosImage image = await NekosClient.GetSfwAsync(Nekos.Net.Endpoints.SfwEndpoint.Neko);
            var resp = await _http.GetAsync(image.FileUrl);
            return await resp.Content.ReadAsStreamAsync();
        }

        public async Task<Stream> GetSFWMeowAsync()
        {
            NekosImage image = await NekosClient.GetSfwAsync(Nekos.Net.Endpoints.SfwEndpoint.Meow);
            var resp = await _http.GetAsync(image.FileUrl);
            return await resp.Content.ReadAsStreamAsync();
        }

        public async Task<Stream> GetSFWLizardAsync()
        {
            NekosImage image = await NekosClient.GetSfwAsync(Nekos.Net.Endpoints.SfwEndpoint.Lizard);
            var resp = await _http.GetAsync(image.FileUrl);
            return await resp.Content.ReadAsStreamAsync();
        }

        public async Task<Stream> GetSFWKissAsync()
        {
            NekosImage image = await NekosClient.GetSfwAsync(Nekos.Net.Endpoints.SfwEndpoint.Kiss);
            var resp = await _http.GetAsync(image.FileUrl);
            return await resp.Content.ReadAsStreamAsync();
        }

        public async Task<Stream> GetSFWHugAsync()
        {
            NekosImage image = await NekosClient.GetSfwAsync(Nekos.Net.Endpoints.SfwEndpoint.Hug);
            var resp = await _http.GetAsync(image.FileUrl);
            return await resp.Content.ReadAsStreamAsync();
        }

        public async Task<Stream> GetSFWFoxGirlAsync()
        {
            NekosImage image = await NekosClient.GetSfwAsync(Nekos.Net.Endpoints.SfwEndpoint.Fox_Girl);
            var resp = await _http.GetAsync(image.FileUrl);
            return await resp.Content.ReadAsStreamAsync();
        }

        public async Task<Stream> GetSFWFeedAsync()
        {
            NekosImage image = await NekosClient.GetSfwAsync(Nekos.Net.Endpoints.SfwEndpoint.Feed);
            var resp = await _http.GetAsync(image.FileUrl);
            return await resp.Content.ReadAsStreamAsync();
        }


        //NekosImage image = await NekosClient.GetSfwAsync(Nekos.Net.Endpoints.SfwEndpoint.Cuddle);
    }
}
