using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using frog_bot.Modules;
using frog_bot.Services;
using Microsoft.Extensions.DependencyInjection;
using ObcMessenger.Services;
using UrbanDictionnet;
using Google.Apis.Discovery;
using Google.Apis.Services;
using Google.Apis.Sheets;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Auth.OAuth2;

/*  TODO features:
 *      - mal integration
 *      - mute when self react (in progress)
 *      - joska and pepo database
 */

namespace frog_bot;

class Program // https://discord.com/api/oauth2/authorize?client_id=1014613847769882665&permissions=8&scope=bot
{
    static ServiceProvider ConfigureServices()
    {
        return new ServiceCollection()
            .AddSingleton<CommandService>()
            .AddSingleton<DiscordHost>()
            .AddSingleton<UrbanClient>()
            .AddSingleton<OsuApi>()
            .BuildServiceProvider();
    }

    public static async Task Main(string[] args)
    {
        using (var services = ConfigureServices())
        {
            string sheetsKey = File.ReadAllLines("sheetsKey.txt")[0];

            var oa = services.GetRequiredService<OsuApi>();
            await oa.Connect(Int16.Parse(File.ReadAllLines("osuId.txt")[0]), File.ReadAllLines("osuKey.txt")[0]);

            var db = services.GetRequiredService<DiscordHost>();
            await db.RunAsync();

            await Task.Delay(-1);
        }
    }

    public void LoadSecrets(string path)
    {

    }

}
    

       