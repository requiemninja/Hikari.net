using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Nekos.Net;
using Nekos.Net.Responses;

namespace hikari.net.Services
{
    public class NSFWEndpoints
    {
        private readonly HttpClient _http;

        public NSFWEndpoints(HttpClient http)
            => _http = http;

        public async Task<Stream> GetNSFWRandomHentaiGifAsync()
        {
            NekosImage image = await NekosClient.GetNsfwAsync(Nekos.Net.Endpoints.NsfwEndpoint.Random_Hentai_Gif);
            var resp = await _http.GetAsync(image.FileUrl);
            return await resp.Content.ReadAsStreamAsync();
        }

        public async Task<Stream> GetNSFWPussyAsync()
        {
            NekosImage image = await NekosClient.GetNsfwAsync(Nekos.Net.Endpoints.NsfwEndpoint.Pussy);
            var resp = await _http.GetAsync(image.FileUrl);
            return await resp.Content.ReadAsStreamAsync();
        }

        public async Task<Stream> GetNSFWLewdAsync()
        {
            NekosImage image = await NekosClient.GetNsfwAsync(Nekos.Net.Endpoints.NsfwEndpoint.Lewd);
            var resp = await _http.GetAsync(image.FileUrl);
            return await resp.Content.ReadAsStreamAsync();
        }

        public async Task<Stream> GetNSFWLesAsync()
        {
            NekosImage image = await NekosClient.GetNsfwAsync(Nekos.Net.Endpoints.NsfwEndpoint.Les);
            var resp = await _http.GetAsync(image.FileUrl);
            return await resp.Content.ReadAsStreamAsync();
        }

        public async Task<Stream> GetNSFWKuniAsync()
        {
            NekosImage image = await NekosClient.GetNsfwAsync(Nekos.Net.Endpoints.NsfwEndpoint.Kuni);
            var resp = await _http.GetAsync(image.FileUrl);
            return await resp.Content.ReadAsStreamAsync();
        }

        public async Task<Stream> GetNSFWCumAsync()
        {
            NekosImage image = await NekosClient.GetNsfwAsync(Nekos.Net.Endpoints.NsfwEndpoint.Cum);
            var resp = await _http.GetAsync(image.FileUrl);
            return await resp.Content.ReadAsStreamAsync();
        }

        public async Task<Stream> GetNSFWClassicAsync()
        {
            NekosImage image = await NekosClient.GetNsfwAsync(Nekos.Net.Endpoints.NsfwEndpoint.Classic);
            var resp = await _http.GetAsync(image.FileUrl);
            return await resp.Content.ReadAsStreamAsync();
        }

        public async Task<Stream> GetNSFWBoobsAsync()
        {
            NekosImage image = await NekosClient.GetNsfwAsync(Nekos.Net.Endpoints.NsfwEndpoint.Boobs);
            var resp = await _http.GetAsync(image.FileUrl);
            return await resp.Content.ReadAsStreamAsync();
        }

        public async Task<Stream> GetNSFWBJAsync()
        {
            NekosImage image = await NekosClient.GetNsfwAsync(Nekos.Net.Endpoints.NsfwEndpoint.Bj);
            var resp = await _http.GetAsync(image.FileUrl);
            return await resp.Content.ReadAsStreamAsync();
        }

        public async Task<Stream> GetNSFWAnalAsync()
        {
            NekosImage image = await NekosClient.GetNsfwAsync(Nekos.Net.Endpoints.NsfwEndpoint.Anal);
            var resp = await _http.GetAsync(image.FileUrl);
            return await resp.Content.ReadAsStreamAsync();
        }

        public async Task<Stream> GetNSFWYuriAsync()
        {
            NekosImage image = await NekosClient.GetNsfwAsync(Nekos.Net.Endpoints.NsfwEndpoint.Yuri);
            var resp = await _http.GetAsync(image.FileUrl);
            return await resp.Content.ReadAsStreamAsync();
        }

        public async Task<Stream> GetNSFWTrapAsync()
        {
            NekosImage image = await NekosClient.GetNsfwAsync(Nekos.Net.Endpoints.NsfwEndpoint.Trap);
            var resp = await _http.GetAsync(image.FileUrl);
            return await resp.Content.ReadAsStreamAsync();
        }

        public async Task<Stream> GetNSFWTitsAsync()
        {
            NekosImage image = await NekosClient.GetNsfwAsync(Nekos.Net.Endpoints.NsfwEndpoint.Tits);
            var resp = await _http.GetAsync(image.FileUrl);
            return await resp.Content.ReadAsStreamAsync();
        }

        public async Task<Stream> GetNSFWSoloGAsync()
        {
            NekosImage image = await NekosClient.GetNsfwAsync(Nekos.Net.Endpoints.NsfwEndpoint.SoloG);
            var resp = await _http.GetAsync(image.FileUrl);
            return await resp.Content.ReadAsStreamAsync();
        }

        public async Task<Stream> GetNSFWSoloAsync()
        {
            NekosImage image = await NekosClient.GetNsfwAsync(Nekos.Net.Endpoints.NsfwEndpoint.Solo);
            var resp = await _http.GetAsync(image.FileUrl);
            return await resp.Content.ReadAsStreamAsync();
        }

        public async Task<Stream> GetNSFWPWankGAsync()
        {
            NekosImage image = await NekosClient.GetNsfwAsync(Nekos.Net.Endpoints.NsfwEndpoint.PWankG);
            var resp = await _http.GetAsync(image.FileUrl);
            return await resp.Content.ReadAsStreamAsync();
        }

        public async Task<Stream> GetNSFWPussyJPGAsync()
        {
            NekosImage image = await NekosClient.GetNsfwAsync(Nekos.Net.Endpoints.NsfwEndpoint.Pussy_JPG);
            var resp = await _http.GetAsync(image.FileUrl);
            return await resp.Content.ReadAsStreamAsync();
        }

        public async Task<Stream> GetNSFWKetaAsync()
        {
            NekosImage image = await NekosClient.GetNsfwAsync(Nekos.Net.Endpoints.NsfwEndpoint.Keta);
            var resp = await _http.GetAsync(image.FileUrl);
            return await resp.Content.ReadAsStreamAsync();
        }

        public async Task<Stream> GetNSFWHoloLewdAsync()
        {
            NekosImage image = await NekosClient.GetNsfwAsync(Nekos.Net.Endpoints.NsfwEndpoint.HoloLewd);
            var resp = await _http.GetAsync(image.FileUrl);
            return await resp.Content.ReadAsStreamAsync();
        }

        public async Task<Stream> GetNSFWHoloEroAsync()
        {
            NekosImage image = await NekosClient.GetNsfwAsync(Nekos.Net.Endpoints.NsfwEndpoint.HoloEro);
            var resp = await _http.GetAsync(image.FileUrl);
            return await resp.Content.ReadAsStreamAsync();
        }

        public async Task<Stream> GetNSFWHentaiAsync()
        {
            NekosImage image = await NekosClient.GetNsfwAsync(Nekos.Net.Endpoints.NsfwEndpoint.Hentai);
            var resp = await _http.GetAsync(image.FileUrl);
            return await resp.Content.ReadAsStreamAsync();
        }

        public async Task<Stream> GetNSFWFutanariAsync()
        {
            NekosImage image = await NekosClient.GetNsfwAsync(Nekos.Net.Endpoints.NsfwEndpoint.Futanari);
            var resp = await _http.GetAsync(image.FileUrl);
            return await resp.Content.ReadAsStreamAsync();
        }

        public async Task<Stream> GetNSFWFemdomAsync()
        {
            NekosImage image = await NekosClient.GetNsfwAsync(Nekos.Net.Endpoints.NsfwEndpoint.Femdom);
            var resp = await _http.GetAsync(image.FileUrl);
            return await resp.Content.ReadAsStreamAsync();
        }

        public async Task<Stream> GetNSFWFeetGAsync()
        {
            NekosImage image = await NekosClient.GetNsfwAsync(Nekos.Net.Endpoints.NsfwEndpoint.FeetG);
            var resp = await _http.GetAsync(image.FileUrl);
            return await resp.Content.ReadAsStreamAsync();
        }

        public async Task<Stream> GetNSFWEroFeetAsync()
        {
            NekosImage image = await NekosClient.GetNsfwAsync(Nekos.Net.Endpoints.NsfwEndpoint.EroFeet);
            var resp = await _http.GetAsync(image.FileUrl);
            return await resp.Content.ReadAsStreamAsync();
        }

        public async Task<Stream> GetNSFWEroAsync()
        {
            NekosImage image = await NekosClient.GetNsfwAsync(Nekos.Net.Endpoints.NsfwEndpoint.Ero);
            var resp = await _http.GetAsync(image.FileUrl);
            return await resp.Content.ReadAsStreamAsync();
        }

        public async Task<Stream> GetNSFWEroKAsync()
        {
            NekosImage image = await NekosClient.GetNsfwAsync(Nekos.Net.Endpoints.NsfwEndpoint.EroK);
            var resp = await _http.GetAsync(image.FileUrl);
            return await resp.Content.ReadAsStreamAsync();
        }

        public async Task<Stream> GetNSFWEroKemoAsync()
        {
            NekosImage image = await NekosClient.GetNsfwAsync(Nekos.Net.Endpoints.NsfwEndpoint.EroKemo);
            var resp = await _http.GetAsync(image.FileUrl);
            return await resp.Content.ReadAsStreamAsync();
        }

        public async Task<Stream> GetNSFWEroNAsync()
        {
            NekosImage image = await NekosClient.GetNsfwAsync(Nekos.Net.Endpoints.NsfwEndpoint.EroN);
            var resp = await _http.GetAsync(image.FileUrl);
            return await resp.Content.ReadAsStreamAsync();
        }

        public async Task<Stream> GetNSFWEroYuriAsync()
        {
            NekosImage image = await NekosClient.GetNsfwAsync(Nekos.Net.Endpoints.NsfwEndpoint.EroYuri);
            var resp = await _http.GetAsync(image.FileUrl);
            return await resp.Content.ReadAsStreamAsync();
        }

        public async Task<Stream> GetNSFWCumJPGAsync()
        {
            NekosImage image = await NekosClient.GetNsfwAsync(Nekos.Net.Endpoints.NsfwEndpoint.Cum_JPG);
            var resp = await _http.GetAsync(image.FileUrl);
            return await resp.Content.ReadAsStreamAsync();
        }

        public async Task<Stream> GetNSFWBlowjobAsync()
        {
            NekosImage image = await NekosClient.GetNsfwAsync(Nekos.Net.Endpoints.NsfwEndpoint.Blowjob);
            var resp = await _http.GetAsync(image.FileUrl);
            return await resp.Content.ReadAsStreamAsync();
        }
    }
}
