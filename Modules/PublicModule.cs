using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using hikari.net.Services;
using System.Linq;
using System;
using LiteDB;
using IniParser;
using IniParser.Model;
using MingweiSamuel.Camille;
using MingweiSamuel.Camille.Enums;

namespace hikari.net.Modules
{
    public class PublicModule : ModuleBase<SocketCommandContext>
    {
        public SFWEndpoints SFWEndpoints { get; set; }
        public NSFWEndpoints NSFWEndpoints { get; set; }

        [Command("ping")]
        [Alias("pong", "hello")]
        public Task PingAsync()
            => ReplyAsync("pong!");

        [Command("admin")]
        public async Task AdminPing()
        {
            await Context.Channel.SendMessageAsync("Pinging <@&424793735151353869> for assistance!");
        }

        [Command("dice")]
        [Alias("roll", "d")]
        public async Task DiceRoll([Remainder] int diceNumber)
        {
            var random = new Random();
            int diceType = random.Next(1, diceNumber);
            await Context.Channel.SendMessageAsync($"{Context.Message.Author.Username} has rolled a {diceType}");
        }

        [Command("quote add")]
        public async Task QuoteAdd([Remainder] string quoteText)
        {
            EmbedBuilder quoteAdd = new EmbedBuilder();
            using var db = new LiteDatabase(@"..\hikari.net.db");
            var collection = db.GetCollection<Quotes>("quotes");
            var quotes = new Quotes
            {
                User = Context.Message.Author.Username,
                Quote = quoteText
            };
            quoteAdd.WithTitle("Quote added");
            quoteAdd.AddField(quoteText, Context.Message.Author.Username, false);
            collection.Insert(quotes);
            await Context.Channel.SendMessageAsync("", false, quoteAdd.Build());
        }

        [Command("quote search")]
        public async Task QuoteSearch([Remainder] string quoteText)
        {

            using (var db = new LiteDatabase(@"..\hikari.net.db"))
            {
                EmbedBuilder quoteEmbed = new EmbedBuilder();
                var random = new Random();
                var collection = db.GetCollection<Quotes>("quotes");
                collection.EnsureIndex(n => n.Quote);
                var results = collection.Find(n => n.Quote.Contains(quoteText));
                var resultsArray = results.ToArray();
                var randomIndex = random.Next(0, resultsArray.Length);
                quoteEmbed.WithTitle("Quote");
                quoteEmbed.AddField(resultsArray[randomIndex].Quote, "Added by " + resultsArray[randomIndex].User);
                await Context.Channel.SendMessageAsync("", false, quoteEmbed.Build());
            }
        }

        [Command("lolrank")]
        public async Task LolRank([Remainder] string summonerInfo)
        {
            var configLoad = new FileIniDataParser();
            IniData configData = configLoad.ReadFile("config.ini");
            string riotApiToken = configData["config"]["riot"];

            var riotApi = RiotApi.NewInstance(riotApiToken);
            var summonerRegion = Region.NA;
            var summonerName = summonerInfo.Replace("NA", "").Replace("EUNE", "").Replace("EUW", "")
                .Replace("na", "").Replace("euw", "").Replace("eune", "");

            if (summonerInfo.Contains("NA", StringComparison.OrdinalIgnoreCase))
            {
                summonerRegion = Region.NA;
            }
            if (summonerInfo.Contains("EUW", StringComparison.OrdinalIgnoreCase))
            {
                summonerRegion = Region.EUW;
            }
            if (summonerInfo.Contains("EUNE", StringComparison.OrdinalIgnoreCase))
            {
                summonerRegion = Region.EUNE;
            }
            if (summonerInfo.Contains("SEA", StringComparison.OrdinalIgnoreCase))
            {
                summonerRegion = Region.Sea;
            }
            if (summonerInfo.Contains("LAN", StringComparison.OrdinalIgnoreCase))
            {
                summonerRegion = Region.LAN;
            }
            if (summonerInfo.Contains("LAS", StringComparison.OrdinalIgnoreCase))
            {
                summonerRegion = Region.LAS;
            }
            if (summonerInfo.Contains("KR", StringComparison.OrdinalIgnoreCase))
            {
                summonerRegion = Region.KR;
            }
            if (summonerInfo.Contains("OCE", StringComparison.OrdinalIgnoreCase))
            {
                summonerRegion = Region.OCE;
            }
            if (summonerInfo.Contains("BR", StringComparison.OrdinalIgnoreCase))
            {
                summonerRegion = Region.BR;
            }

            var summonerData = await riotApi.SummonerV4.GetBySummonerNameAsync(summonerRegion, summonerName);
            if (null == summonerData)
            {
                await Context.Channel.SendMessageAsync($"Summoner '{summonerName}' not found.");
                return;
            }

            var summonerRank = riotApi.LeagueV4.GetLeagueEntriesForSummoner(summonerRegion, summonerData.Id);
            var soloQueueRank = summonerRank.FirstOrDefault(r => r.QueueType == "RANKED_SOLO_5x5");

            if (soloQueueRank == null)
            {
                await Context.Channel.SendMessageAsync($"{summonerData.Name} - Level: {summonerData.SummonerLevel} - is unranked");
            }
            else
            {
                await Context.Channel.SendMessageAsync($"{summonerData.Name} - Level: {summonerData.SummonerLevel} - {soloQueueRank.Tier} {soloQueueRank.Rank}");
            }
        }

        [Command("help")]
        [Alias("commands", "help commands")]
        public async Task HelpCommand()
        {
            EmbedBuilder help = new EmbedBuilder();

            help.WithTitle("Current commands are:");
            help.AddField("!help/!commands", "Display this help", false);
            help.AddField("!admin", "Pings all admins to provide assistance", false);
            help.AddField("!dice/!roll/!d <number>", "Rolls a dice");
            help.AddField("!quote add <Quote>", "Adds a quote");
            help.AddField("!quote search <Term>", "Searches for a quote using the term");
            help.AddField("!lolrank <summonername> <region>", "Displays rank and level of summoner");
            help.AddField("!sfw <command>", "Posts SFW Image - Use '!help sfw' for more info", false);
            help.AddField("!nsfw <command>", "Posts NSFW Image - Use '!help nsfw' for more info", false);

            await Context.Channel.SendMessageAsync("", false, help.Build());
        }

        [Command("help sfw")]
        public async Task SFWHelpCommand()
        {
            EmbedBuilder sfwhelp = new EmbedBuilder();

            sfwhelp.WithTitle("SFW Commands are:");
            sfwhelp.AddField("cuddle", "cuddling", false);
            sfwhelp.AddField("tickle", "tickling", false);
            sfwhelp.AddField("slap", "how can he slap", false);
            sfwhelp.AddField("poke", "MY EYE", false);
            sfwhelp.AddField("pat", "headpats for everyone", false);
            sfwhelp.AddField("neko", "nyaa!~~", false);
            sfwhelp.AddField("meow", "miau", false);
            sfwhelp.AddField("lizard", "hissy bois and gals", false);
            sfwhelp.AddField("kiss", "smooch", false);
            sfwhelp.AddField("hug", "cure to war", false);
            sfwhelp.AddField("foxgirl", "kitsune gals", false);
            sfwhelp.AddField("feed", "noms", false);

            await Context.Channel.SendMessageAsync("", false, sfwhelp.Build());
        }

        [Command("help nsfw")]
        public async Task NSFWHelpCommand()
        {
            EmbedBuilder nsfwhelp = new EmbedBuilder();

            nsfwhelp.WithTitle("NSFW Commands are:");
            nsfwhelp.AddField("randomgif\r\n" +
                "pussy\r\n" +
                "lewd\r\n" +
                "lesbian\r\n" +
                "kuni\r\n" +
                "cum\r\n" +
                "classic\r\n" +
                "boobs\r\n" +
                "bj\r\n" +
                "anal\r\n" +
                "yuri\r\n" +
                "trap\r\n" +
                "tits\r\n" +
                "sologirlgif\r\n" +
                "sologirl\r\n" +
                "wank\r\n" +
                "pussyjpg\r\n" +
                "keta\r\n" +
                "hololewd\r\n" +
                "holoero\r\n" +
                "hentai\r\n" +
                "futanari\r\n" +
                "femdom\r\n" +
                "feetgif\r\n" +
                "erofeet\r\n" +
                "ero\r\n" +
                "erok\r\n" +
                "erokemo\r\n" +
                "eron\r\n" +
                "eroyuri\r\n" +
                "cumjpg\r\n" +
                "blowjob\r\n", "End of list", false);

            await Context.Channel.SendMessageAsync("", false, nsfwhelp.Build());
        }

        #region SFW

        [Command("sfw cuddle")]
        public async Task SFWCuddleAsync()
        {
            var stream = await SFWEndpoints.GetSFWCuddleAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "sfwcuddle.png");
        }

        [Command("sfw tickle")]
        public async Task SFWTickleAsync()
        {
            var stream = await SFWEndpoints.GetSFWTickleAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "sfwtickle.png");
        }

        [Command("sfw slap")]
        public async Task SFWSlapAsync()
        {
            var stream = await SFWEndpoints.GetSFWSlapAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "sfwslap.png");
        }

        [Command("sfw poke")]
        public async Task SFWPokeAsync()
        {
            var stream = await SFWEndpoints.GetSFWPokeAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "sfwpoke.png");
        }

        [Command("sfw pat")]
        public async Task SFWPatAsync()
        {
            var stream = await SFWEndpoints.GetSFWPatAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "sfwpat.png");
        }

        [Command("sfw neko")]
        public async Task SFWNekoAsync()
        {
            var stream = await SFWEndpoints.GetSFWNekoAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "sfwneko.png");
        }

        [Command("sfw meow")]
        public async Task SFWMeowAsync()
        {
            var stream = await SFWEndpoints.GetSFWMeowAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "sfwmeow.png");
        }

        [Command("sfw lizard")]
        public async Task SFWLizardAsync()
        {
            var stream = await SFWEndpoints.GetSFWLizardAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "sfwlizard.png");
        }

        [Command("sfw kiss")]
        public async Task SFWKissAsync()
        {
            var stream = await SFWEndpoints.GetSFWKissAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "sfwkiss.png");
        }

        [Command("sfw hug")]
        public async Task SFWHugAsync()
        {
            var stream = await SFWEndpoints.GetSFWHugAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "sfwhug.png");
        }

        [Command("sfw foxgirl")]
        public async Task SFWFoxGirlAsync()
        {
            var stream = await SFWEndpoints.GetSFWFoxGirlAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "sfwfoxgirl.png");
        }

        [Command("sfw feed")]
        public async Task SFWFeedAsync()
        {
            var stream = await SFWEndpoints.GetSFWFeedAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "sfwfeed.png");
        }

        #endregion

        #region NSFW

        [Command("nsfw randomgif")]
        public async Task NSFWRandomGifAsync()
        {
            var stream = await NSFWEndpoints.GetNSFWRandomHentaiGifAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "nsfw-random.gif");
        }

        [Command("nsfw pussy")]
        public async Task NSFWPussyAsync()
        {
            var stream = await NSFWEndpoints.GetNSFWPussyAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "nsfw-pussy.png");
        }

        [Command("nsfw lewd")]
        public async Task NSFWLewdAsync()
        {
            var stream = await NSFWEndpoints.GetNSFWLewdAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "nsfw-lewd.png");
        }

        [Command("nsfw lesbian")]
        public async Task NSFWLesAsync()
        {
            var stream = await NSFWEndpoints.GetNSFWLesAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "nsfw-les.png");
        }

        [Command("nsfw kuni")]
        public async Task NSFWKuniAsync()
        {
            var stream = await NSFWEndpoints.GetNSFWPussyAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "nsfw-kuni.png");
        }

        [Command("nsfw cum")]
        public async Task NSFWCumAsync()
        {
            var stream = await NSFWEndpoints.GetNSFWCumAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "nsfw-cum.png");
        }

        [Command("nsfw classic")]
        public async Task NSFWclassicAsync()
        {
            var stream = await NSFWEndpoints.GetNSFWClassicAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "nsfw-classic.png");
        }

        [Command("nsfw boobs")]
        public async Task NSFWBoobsAsync()
        {
            var stream = await NSFWEndpoints.GetNSFWBoobsAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "nsfw-boobs.png");
        }

        [Command("nsfw bj")]
        public async Task NSFWBJAsync()
        {
            var stream = await NSFWEndpoints.GetNSFWBJAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "nsfw-bj.png");
        }

        [Command("nsfw anal")]
        public async Task NSFWAnalAsync()
        {
            var stream = await NSFWEndpoints.GetNSFWAnalAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "nsfw-anal.png");
        }

        [Command("nsfw yuri")]
        public async Task NSFWYuriAsync()
        {
            var stream = await NSFWEndpoints.GetNSFWYuriAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "nsfw-yuri.png");
        }

        [Command("nsfw trap")]
        public async Task NSFWTrapAsync()
        {
            var stream = await NSFWEndpoints.GetNSFWTrapAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "nsfw-trap.png");
        }

        [Command("nsfw tits")]
        public async Task NSFWTitsAsync()
        {
            var stream = await NSFWEndpoints.GetNSFWTitsAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "nsfw-tits.png");
        }

        [Command("nsfw sologirlgif")]
        public async Task NSFWGirlSoloGifAsync()
        {
            var stream = await NSFWEndpoints.GetNSFWSoloGAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "nsfw-solo.gif");
        }

        [Command("nsfw sologirl")]
        public async Task NSFWSoloGirlAsync()
        {
            var stream = await NSFWEndpoints.GetNSFWSoloAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "nsfw-solo.png");
        }

        [Command("nsfw wank")]
        public async Task NSFWPWankGAsync()
        {
            var stream = await NSFWEndpoints.GetNSFWPWankGAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "nsfw-wank.gif");
        }

        [Command("nsfw pussyjpg")]
        public async Task NSFWPussyJPGAsync()
        {
            var stream = await NSFWEndpoints.GetNSFWPussyJPGAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "nsfw-pussy.png");
        }

        [Command("nsfw keta")]
        public async Task NSFWKetaAsync()
        {
            var stream = await NSFWEndpoints.GetNSFWKetaAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "nsfw-keta.png");
        }

        [Command("nsfw hololewd")]
        public async Task NSFWHoloLewdAsync()
        {
            var stream = await NSFWEndpoints.GetNSFWHoloLewdAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "nsfw-hololewd.png");
        }

        [Command("nsfw holoero")]
        public async Task NSFWHoloEroAsync()
        {
            var stream = await NSFWEndpoints.GetNSFWHoloEroAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "nsfw-holoero.png");
        }

        [Command("nsfw hentai")]
        public async Task NSFWHentaiAsync()
        {
            var stream = await NSFWEndpoints.GetNSFWHentaiAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "nsfw-hentai.png");
        }

        [Command("nsfw futa")]
        public async Task NSFWFutanariAsync()
        {
            var stream = await NSFWEndpoints.GetNSFWFutanariAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "nsfw-futa.png");
        }

        [Command("nsfw femdom")]
        public async Task NSFWFemdomAsync()
        {
            var stream = await NSFWEndpoints.GetNSFWFemdomAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "nsfw-femdom.png");
        }

        [Command("nsfw feetgif")]
        public async Task NSFWFeetGAsync()
        {
            var stream = await NSFWEndpoints.GetNSFWFeetGAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "nsfw-feet.gif");
        }

        [Command("nsfw erofeet")]
        public async Task NSFWEroFeetAsync()
        {
            var stream = await NSFWEndpoints.GetNSFWEroFeetAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "nsfw-erofeet.gif");
        }

        [Command("nsfw ero")]
        public async Task NSFWEroAsync()
        {
            var stream = await NSFWEndpoints.GetNSFWEroAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "nsfw-ero.png");
        }

        [Command("nsfw erok")]
        public async Task NSFWEroKAsync()
        {
            var stream = await NSFWEndpoints.GetNSFWEroKAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "nsfw-erok.png");
        }

        [Command("nsfw erokemo")]
        public async Task NSFWEroKemoAsync()
        {
            var stream = await NSFWEndpoints.GetNSFWEroKemoAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "nsfw-erokemo.png");
        }

        [Command("nsfw eron")]
        public async Task NSFWEroNAsync()
        {
            var stream = await NSFWEndpoints.GetNSFWEroNAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "nsfw-eron.png");
        }

        [Command("nsfw eroyuri")]
        public async Task NSFWEroYuriAsync()
        {
            var stream = await NSFWEndpoints.GetNSFWEroYuriAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "nsfw-eroyuri.png");
        }

        [Command("nsfw cumjpg")]
        public async Task NSFWCumJPGAsync()
        {
            var stream = await NSFWEndpoints.GetNSFWCumJPGAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "nsfw-cum.png");
        }

        [Command("nsfw blowjob")]
        public async Task NSFWBlowjobAsync()
        {
            var stream = await NSFWEndpoints.GetNSFWBlowjobAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "nsfw-blowjob.png");
        }

        #endregion

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

        public class Quotes
        {
            public int Id { get; set; }
            public string User { get; set; }
            public string Quote{ get; set; }
        }
    }
}
