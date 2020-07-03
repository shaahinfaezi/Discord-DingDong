using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using Discord;
using DiscordRPC;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;


namespace DING_DONG
{
    class Program
    {
        DiscordRpcClient RPclient;
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
            RPC_Create();
            _client.Log += Log;
            await _client.LoginAsync(Discord.TokenType.Bot, Config.bot.token);
            await _client.StartAsync();
           _handler = new CommandHandler();
           await _handler.InitializeAsync(_client);
            await Task.Delay(-1);
        }
        void RPC_Create()
        {
          
            RPclient = new DiscordRpcClient(Config.bot.clientid);
            RPclient.SetPresence(new RichPresence
            {
                
                State = "Aw man",
                Details = "Ding Donging",
                Assets = new Assets()
                {
                    LargeImageKey = "webp_net-resizeimage",
                    LargeImageText = "Ding Dong",
                    SmallImageKey = "webp_net-resizeimage_2_",
                    SmallImageText = "YEET"
                }

            }) ; 
            RPclient.Initialize();
        }
        

        private async Task Log(Discord.LogMessage msg)
        {
            Console.WriteLine(msg.Message);
        }

    }
}
