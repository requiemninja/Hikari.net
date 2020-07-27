using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using hikari.net.Services;
using System.Linq;

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
        public async Task adminPing()
        {
            await Context.Channel.SendMessageAsync("Pinging <@&424793735151353869> for assistance!");
        }

        [Command("nsfwneko")]
        public async Task NSFWNekoAsync()
        {
            var stream = await PictureService.GetNSFWNekoPictureAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "nsfwneko.png");
        }

        [Command("sfwneko")]
        public async Task SFWNekoAsync()
        {
            var stream = await PictureService.GetSFWNekoPictureAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "sfwneko.png");
        }

        [Command("ban")]
        [RequireContext(ContextType.Guild)]
        [RequireUserPermission(GuildPermission.BanMembers)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        public async Task BanUserAsync(IGuildUser user, [Remainder] string reason = null)
        {
            await user.Guild.AddBanAsync(user, reason: reason);
            await ReplyAsync("ok!");
        }
    }
}
