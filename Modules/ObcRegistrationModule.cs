using Discord.Commands;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Sheets.v4;
using ObcMessenger.Enums.Osu;
using ObcMessenger.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Discord.WebSocket;
using Discord;

namespace frog_bot.Modules;

public class ObcRegistrationModule : ModuleBase<SocketCommandContext>
{
    OsuApi m_api;
    GoogleHelperService m_service = new GoogleHelperService();

    static List<ulong> regUsers = new List<ulong>();
    static string regPath = "registracije.txt";

    public ObcRegistrationModule(OsuApi api)
    {
        m_api = api;
    }

    [Command("blacklist")]
    public async Task Blacklist([Remainder] SocketGuildUser user)
    {
        if(Context.Message.Author.Id == 254732114409291776 || Context.Message.Author.Id == 217395823523135489)
        {
            AddToList(user.Id);
            await ReplyAsync($"The user \"{user.Username}\" has been blacklisted");
        }
    }

    [Command("unblacklist")]
    public async Task Unblacklist([Remainder] SocketGuildUser user)
    {
        string[] registracije = File.ReadAllText("registracije.txt").Split(Environment.NewLine);

        foreach(string line in registracije)
        {
            if (line != user.Id.ToString())
            {
                File.WriteAllText("registracije.txt", line);
            }
            else
            {
                await ReplyAsync($"The user \"{user.Username}\" has been unblacklisted!");
            }
        }
    }


    // TODO: blacklisting, unregister
    [Command("register")]
    [Alias("registriraj")]
    [RequireContext(ContextType.Guild)]
    public async Task Register([Remainder] string args = null)
    {
        foreach(string line in File.ReadAllLines(regPath))
        {
            if(line == Context.Message.Author.Id.ToString())
            {
                await ReplyAsync("You are already registered.");
                return;
            }
            
        }

        if (Context.Message.Channel.Id == 1067538012092776478) //regs
        {
            if (string.IsNullOrEmpty(args))
            {
                await Context.Channel.SendMessageAsync("Type !register followed by your osu profile name or profile ID");
            }
            else
            {
                var user = await m_api.GetUser(args, GameMode.Osu);
                AddToList(Context.Message.Author.Id);
                await ReplyAsync($"{user.Username} from {user.Country.Name} has been registered for OBC2023!");
                Create(user, Context.Message.Author.ToString());
                await (Context.User as IGuildUser).AddRoleAsync(1067372599564312586);
            }
        }
    }

    public void Create(ObcMessenger.Models.Osu.User user, string discordID)
    {
        var range = "Registrations!C:G";
        var valueRange = new ValueRange();

        var objectList = new List<object>();
        objectList.Add($"=IMAGE(\"https://osuflags.omkserver.nl/{user.CountryCode}.png\", 1)");
        objectList.Add($"= HYPERLINK(\"https://osu.ppy.sh/users/{user.Id}/osu\", \"{user.Username}\")");
        objectList.Add(discordID);
        objectList.Add($"#{user.Statistics.GlobalRank}");
        objectList.Add(user.Statistics.PP);
        valueRange.Values = new List<IList<object>> { objectList };

        var appendRequest = m_service.Service.Spreadsheets.Values.Append(valueRange, m_service.SheetID, range);
        appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;


        var response = appendRequest.Execute();
    }

    public void AddToList(ulong id)
    {
        File.AppendAllText(regPath, id + Environment.NewLine);
    }
}


