using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using ObcMessenger.Services;

namespace frog_bot
{
    [Group("cta")]
    [Alias("cat")]
    internal class CtaModule : ModuleBase<SocketCommandContext> //TODO: tags
    {
        static readonly HttpClient http = new HttpClient();

        [Command("")]
        public async Task RandomCat() // TODO: jpg/gif differencing
        {
            var request = await http.GetAsync("https://cataas.com/cat");
            var stream = await request.Content.ReadAsStreamAsync();

            await Context.Channel.SendFileAsync(new Discord.FileAttachment(stream, "cat.jpg"));
        }

        [Command("text")]
        [Alias("text:", "says", "say")]
        public async Task TextCat([Remainder] string text)
        {
            var request = await http.GetAsync($"https://cataas.com/cat/says/{text}");
            var stream = await request.Content.ReadAsStreamAsync();

            await Context.Channel.SendFileAsync(new Discord.FileAttachment(stream, "cat.jpg"));
        }

    }
}
