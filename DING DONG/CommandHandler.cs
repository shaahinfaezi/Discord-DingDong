using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;
using Discord.Commands;
using System.Reflection;

namespace DING_DONG
{

    class CommandHandler
    {
        DiscordSocketClient _client;
        CommandService _service;

        public  async  Task InitializeAsync(DiscordSocketClient client)
        {
            _client = client;
            _service = new CommandService();
            await _service.AddModulesAsync(assembly:Assembly.GetEntryAssembly(),services:null);
            _client.MessageReceived += HandleCommandAsync;

        }

        private async Task HandleCommandAsync(SocketMessage s)
        {
            var msg = s as SocketUserMessage;
            if (msg == null)
            {
                return;
            }
            var context = new SocketCommandContext(_client, msg);
            int argpos = 0;
            if(msg.HasStringPrefix(Config.bot.cmdPrefix,ref argpos)|| msg.HasMentionPrefix(_client.CurrentUser,ref argpos))
            {
                var result = await _service.ExecuteAsync(context, argpos,services:null);
                if(!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                {
                    Console.WriteLine(result.ErrorReason);
                }
            }
        }
    }
}
