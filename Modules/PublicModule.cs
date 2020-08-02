using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using hikari.net.Services;
using System.Linq;
using System;

namespace hikari.net.Modules
{
    public class PublicModule : ModuleBase<SocketCommandContext>
    {
        public PictureService PictureService { get; set; }

        [Command("ping")]
        [Alias("pong", "hello")]
        public Task PingAsync()
            => ReplyAsync("pong!");

        [Command("admin")]
        public async Task AdminPing()
        {
            await Context.Channel.SendMessageAsync("Pinging <@&424793735151353869> for assistance!");
        }

        [Command("commands")]
        public async Task CommandsList()
        {
            await Context.Channel.SendMessageAsync("Commands list placeholder");
        }

        [Command("sfw cuddle")]
        public async Task SFWCuddleAsync()
        {
            var stream = await PictureService.GetSFWCuddleAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "sfwcuddle.png");
        }

        [Command("sfw tickle")]
        public async Task SFWTickleAsync()
        {
            var stream = await PictureService.GetSFWTickleAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "sfwtickle.png");
        }

        [Command("sfw slap")]
        public async Task SFWSlapAsync()
        {
            var stream = await PictureService.GetSFWSlapAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "sfwslap.png");
        }

        [Command("sfw poke")]
        public async Task SFWPokeAsync()
        {
            var stream = await PictureService.GetSFWPokeAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "sfwpoke.png");
        }

        [Command("sfw pat")]
        public async Task SFWPatAsync()
        {
            var stream = await PictureService.GetSFWPatAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "sfwpat.png");
        }

        [Command("sfw neko")]
        public async Task SFWNekoAsync()
        {
            var stream = await PictureService.GetSFWNekoAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "sfwneko.png");
        }

        [Command("sfw meow")]
        public async Task SFWMeowAsync()
        {
            var stream = await PictureService.GetSFWMeowAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "sfwmeow.png");
        }

        [Command("sfw lizard")]
        public async Task SFWLizardAsync()
        {
            var stream = await PictureService.GetSFWLizardAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "sfwlizard.png");
        }

        [Command("sfw kiss")]
        public async Task SFWKissAsync()
        {
            var stream = await PictureService.GetSFWKissAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "sfwkiss.png");
        }

        [Command("sfw hug")]
        public async Task SFWHugAsync()
        {
            var stream = await PictureService.GetSFWHugAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "sfwhug.png");
        }

        [Command("sfw foxgirl")]
        public async Task SFWFoxGirlAsync()
        {
            var stream = await PictureService.GetSFWFoxGirlAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "sfwfoxgirl.png");
        }

        [Command("sfw feed")]
        public async Task SFWFeedAsync()
        {
            var stream = await PictureService.GetSFWFeedAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "sfwfeed.png");
        }

        [Command("ban")]
        [RequireContext(ContextType.Guild)]
        [RequireUserPermission(GuildPermission.BanMembers)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        public async Task BanUserAsync(IGuildUser user, [Remainder] string reason = null)
        {
            await user.Guild.AddBanAsync(user, reason: reason);
            await ReplyAsync($"{user} has been banned for {reason}.");
            //await ReplyAsync(user + " has been banned for " + reason + );
        }
    }
}
