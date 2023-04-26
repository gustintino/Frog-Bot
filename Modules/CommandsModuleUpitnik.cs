using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using ObcMessenger.Services;
using static System.Net.WebRequestMethods;


namespace frog_bot
{
    internal class CommandsModuleUpitnik : ModuleBase<SocketCommandContext>
    {
        OsuApi m_api;

        public CommandsModuleUpitnik(OsuApi api)
        {
            m_api = api;
        }

        [Command("user")]
        public async Task User([Remainder] string args = null)
        {
            if (string.IsNullOrEmpty(args))
            {
                await ReplyAsync("trebam korisnika");
                return;
            }
            
            var userNormalized = string.Join("_", args);

            var user = await m_api.GetUser(userNormalized, ObcMessenger.Enums.Osu.GameMode.Osu);
            await ReplyAsync($"Korisnik {user.Username} je rank #{user.Statistics.GlobalRank}");
        }

        [Command("smrdis")]
        [RequireContext(ContextType.Guild)]
        public async Task Smrdis()
        {
            await Context.Message.ReplyAsync("prdis");
        }

        [Command("echo")]
        public async Task Echo(string text)
        {
            await ReplyAsync(text);
        }
    }


}
