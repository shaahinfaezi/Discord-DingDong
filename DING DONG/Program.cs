using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using Discord;

namespace DING_DONG
{
    class Program
    {
        DiscordSocketClient _client;
        CommandHandler _handler;
        static void Main(string[] args)
        => new Program().StartAsync().GetAwaiter().GetResult();
        public async  Task  StartAsync()
        {
            var config = new DiscordSocketConfig
            {
                AlwaysDownloadUsers = true,
                LogLevel = Discord.LogSeverity.Verbose
            };
            if(Config.bot.token == "" || Config.bot.token == null)
            {
                return;
            }
            _client = new DiscordSocketClient(config);
            
            _client.Log += Log;
            await _client.LoginAsync(Discord.TokenType.Bot, Config.bot.token);
            await _client.StartAsync();
           _handler = new CommandHandler();
           await _handler.InitializeAsync(_client);
            await Task.Delay(-1);
        }

        private async Task Log(Discord.LogMessage msg)
        {
            Console.WriteLine(msg.Message);
        }
    }
}
