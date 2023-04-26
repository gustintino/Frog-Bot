using Discord.WebSocket;
using Discord;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Discord.Commands;
using System.Reflection;
using System.IO;

namespace frog_bot.Services;

internal class DiscordHost
{
    DiscordSocketClient m_client;
    IServiceProvider m_services;
    CommandService m_commands;

    Timer spamMsgTimer = new Timer(6);
    Timer cmdTimer = new Timer(5);
    Timer memeTimer = new Timer(15);

    class TextMatch
    {
        public string[] Strings { get; set; }
        public string[] Ignores { get; set; }
        public bool IsPartial { get; set; }

        public Action<SocketMessage> Handler { get; set; }

        public TextMatch(string[] strings, string[] ignores = null, bool isPartial = false, Action<SocketMessage> handler = null)
        {
            Strings = strings;
            Ignores = ignores;
            IsPartial = isPartial;
            Handler = handler;
        }

        public bool IsValid(string text)
        {
            if (IsPartial)
            {
                if (Strings.Any(s => text.Contains(s)) && !Ignores.Any(s => text.Contains(s)))
                {
                    return true;
                }
            }
            else
            {
                if (text == Strings[0])
                {
                    return true;
                }
            }

            return false;
        }
    }

    List<TextMatch> m_internalCommands = new();
    Random m_random = new Random();

    public DiscordHost(CommandService commandService, IServiceProvider services)
    {
        m_services = services;
        m_commands = commandService;

        m_internalCommands.Add(new TextMatch(new[] { "ratio" }, handler: async (message) =>
        {
            var fire = new Emoji("🔥");
            await message.AddReactionAsync(fire);
        }));

       /* m_internalCommands.Add(new TextMatch(new[] { "8", "osam", }, new[] { "http", "18" }, isPartial: true,
            handler: async (message) =>
        {
            int r = m_random.Next(0, 260);

            await message.Channel.SendMessageAsync(r % 2 == 0 ? "nos ti posran" : "na kurcu te nosam");
        }));

        m_internalCommands.Add(new TextMatch(new[] { "6", "sest", }, new[] { "http", "16" }, isPartial: true,
            handler: async (message) =>
            {
                await message.Channel.SendMessageAsync("na kurcu si cest");
            }));*/
    }

    public async Task RunAsync()
    {
        var config = new DiscordSocketConfig()
        {
            GatewayIntents = GatewayIntents.All,
            MessageCacheSize = 100
        };

        m_client = new DiscordSocketClient(config);

        m_client.Log += Log;
        m_commands.Log += Log;
        m_client.ReactionAdded += ReactionHandler;

        await RegisterCommandsAsync();

        await m_client.LoginAsync(TokenType.Bot, File.ReadAllLines("token.txt")[0]);
        await m_client.StartAsync();
    }

    private Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }

    public async Task RegisterCommandsAsync()
    {
        m_client.MessageReceived += HandleCommandAsync;
        await m_commands.AddModulesAsync(Assembly.GetEntryAssembly(), m_services);
    }

    public async Task HandleCommandAsync(SocketMessage msg)
    {
        var message = msg as SocketUserMessage;
        var context = new SocketCommandContext(m_client, message);
        int argPos = 1;

        var dt = DateTime.Now.ToString("HH:mm:ss");
        var cnt = string.IsNullOrEmpty(message.Content.Trim()) ? message.Attachments.First().Url : message.Content;
        Console.WriteLine($"[{dt}] <{message.Author.Username}> {cnt}");

        if ((message.Author.Id != 254732114409291776 && false) ||
            message.Author.IsBot)
        {
            return;
        }

        var text = message.Content.ToLower();

        /*if (memeTimer.CheckTimer())
        {
            var cmd = m_internalCommands.FirstOrDefault(h => h.IsValid(text));
            if (cmd != null)
            {
                cmd.Handler?.Invoke(msg);
            }

            memeTimer.SetTimer();
        }

        if (cmdTimer.CheckTimer())
        {
            //actual commands
            

            if (!result.IsSuccess)
            {
                Console.WriteLine("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA command not working Lol");
                await message.Channel.SendMessageAsync("nekaj ne valja");
            }

            cmdTimer.SetTimer();
        }
        else
        {
            if (spamMsgTimer.CheckTimer())
            {
                await context.Channel.SendMessageAsync("chill, ne spamaj");
                spamMsgTimer.SetTimer();
            }
        }*/

        if (!message.Content.StartsWith("!")) return;

        var result = await m_commands.ExecuteAsync(context, argPos, m_services);
    }

    public async Task ReactionHandler(Cacheable<IUserMessage, ulong> message, Cacheable<IMessageChannel, ulong> channel, SocketReaction reaction)
    {
        var m = await message.DownloadAsync();

        if (m.Reactions.Count == 1 && m.Author.Id == reaction.UserId)
        {
            await reaction.Channel.SendMessageAsync($"idemute <@{reaction.UserId}> 1h self react");
        }
    } 

    public class Timer
    {
        private DateTime m_lastTime = DateTime.Now;
        private int m_cooldownSeconds;

        public Timer(int cooldownSeconds)
        {
            m_cooldownSeconds = cooldownSeconds;
        }

        public bool CheckTimer()
        {
            if (DateTime.Now > m_lastTime)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SetTimer()
        {
            m_lastTime = DateTime.Now.AddSeconds(m_cooldownSeconds);
        }
    }
}
