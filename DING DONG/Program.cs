using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using Discord;
using DiscordRPC;
using System.Threading;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;
using DING_DONG.Core.UserAccounts;
using Discord.WebSocket;
using System.IO;
using Discord.Net;
using DiscordInteractivity;





namespace DING_DONG
{

    class Program
    {

        DiscordRpcClient RPclient;
        DiscordSocketClient _client;
        CommandHandler _handler;
        static void Main(string[] args)
        => new Program().StartAsync().GetAwaiter().GetResult();

        public async Task StartAsync()
        {
            var config = new DiscordSocketConfig
            {
                AlwaysDownloadUsers = true,
                LogLevel = Discord.LogSeverity.Verbose,
                MessageCacheSize = 100
            };
            if (Config.bot.token == "" || Config.bot.token == null)
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
            _client.Ready += async () =>
            {
                await Onclient();


            };



            await Task.Delay(-1);




        }





        private async Task Log(Discord.LogMessage msg)
        {
            Console.WriteLine(msg.Message);
        }





        void RPC_Create()
        {

            RPclient = new DiscordRpcClient(Config.bot.clientid);
            RPclient.SetPresence(new RichPresence
            {

                State = "",
                Details = "Ding Donging",
                Assets = new Assets()
                {
                    LargeImageKey = "rsz_22_v1",
                    LargeImageText = "Ding Dong",

                }

            });
            RPclient.Initialize();
        }

        public async Task Onclient()
        {
            
            var guild = _client.GetGuild(Convert.ToUInt64(Config.bot.serverid));
            IMessageChannel channel = guild.GetChannel(Convert.ToUInt64(Config.bot.channelid)) as IMessageChannel;
            await channel.SendMessageAsync("Hey @everyone I'm UP and running!");

        }





    }
}
