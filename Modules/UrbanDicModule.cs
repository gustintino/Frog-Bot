using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UrbanDictionnet;

namespace frog_bot.Modules
{
    internal class UrbanDicModule : ModuleBase<SocketCommandContext> //TODO: pages
    {
        public UrbanClient urban { get; set; }
        public DiscordSocketClient client;

        [Command("urbandictionary")]
        [Alias("ud", "urban")]
         async Task GetWord([Remainder] string word)
        {
            var embed = new EmbedBuilder();
            var result = await urban.GetWordAsync(word);

            //await ReplyAsync(result.Definitions[1].ToString());
            embed.WithTitle("Search: " + result.Definitions[0].Word.ToString())
                 .AddField("__**Definition:**__ ", result.Definitions[0].Definition.ToString())
                 .AddField("__**Example:**__", result.Definitions[0].Example.ToString())
                 .WithColor(Color.Green);

            var button = new ComponentBuilder()
                .WithButton("nesto", "nesto drugo");

            await ReplyAsync(embed: embed.Build(), components: button.Build());
        }

        public async Task ButtonHandler(SocketMessageComponent msg)
        {
            await Task.CompletedTask;
        }
    }
}
