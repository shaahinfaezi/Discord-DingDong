using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading;
using NReco.ImageGenerator;
using ImageFormat = NReco.ImageGenerator.ImageFormat;
using System.IO;
using DING_DONG.Core.UserAccounts;

namespace DING_DONG.Commands
{
    
    public class Commands : ModuleBase<SocketCommandContext>
    {
       public static string emoji_1 = "1️⃣";
        public static string emoji_2 = "2️⃣";
        public static string emoji_3 = "3️⃣";
        public static string emoji_4 = "4️⃣";
        public static string emoji_5 = "5️⃣";
        public static string emoji_6 = "6️⃣";
        public static string emoji_7 = "7️⃣";
        public static  string emoji_8 = "8️⃣";
        public static string emoji_9 = "9️⃣";
        public static string emoji_10 = "❌";
        public static string emoji_11 = "⭕";
        public static string template ;
        Emoji emoji1, emoji2, emoji3, emoji4, emoji5, emoji6, emoji7, emoji8, emoji9, emojix, emojio;
        EmbedBuilder embed;
        MemoryStream winimage;
        public static SocketGuildUser player1, player2;
        public static bool turn=true;
        public static bool TICTACTOE = false;
        public static string playerturn;
        public static bool winCheck=false;
        public static bool winner;
        public static string footer;
        public static int usercount;


        

     




        //TICTACTOE
        [Command("TTTSTATS")]
        public async Task TTTSTATS()
        {
            if (Role_Check((SocketGuildUser)Context.User, "TicTacToeRole"))
            {
                if (Context.Channel.Id == 619570247476707328)
                {
                    var account = UserAccounts.GetAccount(Context.User);
                    var embed = new EmbedBuilder();
                    embed.WithTitle("TICTACTOE STATS");
                    embed.WithDescription("You have " + account.wins.ToString() + " wins!");
                    embed.WithColor(Color.Green);
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                    
                    
                }
                else
                {
                    await Context.Channel.SendMessageAsync("You Can't do that in this channel!");
                }
            }
            else
            {
                await Context.Channel.SendMessageAsync("You Can't do that!");
            }
        }


        [Command("player1")]
        public async Task player1_()
        {
            if (player1 == null)
            {
                if (Role_Check((SocketGuildUser)Context.User, "TicTacToeRole"))
                {
                    if (Context.Channel.Id == 619570247476707328)
                    {
                        
                        Role_Add((SocketGuildUser)Context.User, "Player1Role");
                        player1 = Context.Guild.GetUser(Context.User.Id);
                        await Context.Channel.SendMessageAsync(player1.Username.ToString() + " is player 1");
                          
                       
                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync("You Can't do that in this channel!");
                    }


                }
                else
                {
                    await Context.Channel.SendMessageAsync("You Can't do that!");
                }
            }
            else
            {
                await Context.Channel.SendMessageAsync("Player 1 is already selected!");
            }
        }
        [Command("player2")]
        public async Task player2_()
        {
            if (player1 != null)
            {
                if (player2 == null)
                {
                    if (player1 != Context.User||Role_Check((SocketGuildUser)Context.User,"OwnerRole"))
                    { 
                        if (Role_Check((SocketGuildUser)Context.User, "TicTacToeRole"))
                        {
                            if (Context.Channel.Id == 619570247476707328)
                            {
                                Role_Add((SocketGuildUser)Context.User, "Player2Role");
                                player2 = Context.Guild.GetUser(Context.User.Id);
                                await Context.Channel.SendMessageAsync(player2.Username.ToString() + " is player 2");
                                Thread.Sleep(2000);

                                if ((player1 != null) && (player2 != null))
                                {
                                    TICTACTOE = true;
                                    NewEmoji();
                                    TemplateRefresh();
                                    EmbedMaker();
                                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                                }
                                else
                                {
                                    await Context.Channel.SendMessageAsync("Please choose the players first!");
                                }


                            }
                            else
                            {
                                await Context.Channel.SendMessageAsync("You Can't do that in this channel!");
                            }
                        }
                        else
                        {
                            await Context.Channel.SendMessageAsync("You Can't do that!");
                        }
                }
                    else
                    {
                        await Context.Channel.SendMessageAsync("You wanna play with yourself?!");
                    }
                }
                else
                {
                    await Context.Channel.SendMessageAsync("Player 2 is already selected!");
                }
            }
            else
            {
                await Context.Channel.SendMessageAsync("Please choose Player 1 first!");
            }
            }
        
   
        [Command("ttt")]
        public async Task TTT([Remainder]string pick)
        {
            char pickc = Convert.ToChar(pick);
            int picki = Convert.ToInt32(pick);
            if (Context.Channel.Id == 619570247476707328)
            {
                if (TICTACTOE == true)
                {
                    if ((char.IsNumber(pickc)) && (picki <= 9) && (1 <= picki))
                    {
                        if (Role_Check((SocketGuildUser)Context.User, "Player1Role") || Role_Check((SocketGuildUser)Context.User, "Player2Role"))
                        {
                            if (playerturn == Context.User.Username.ToString())
                            {

                                TicTacToeSystem(picki);
                                NewEmoji();
                                TemplateRefresh();
                                EmbedMaker();
                                await Context.Channel.SendMessageAsync("", false, embed.Build());
                                if (winCheck == true)
                                {
                                    if (winner == true)
                                    {
                                        var account = UserAccounts.GetAccount(player1);
                                        account.wins += 1;
                                        UserAccounts.SaveAccount();
                                        TTTRANK(player1, account.wins);
                                        image_generator(player1);
                                        await Context.Channel.SendFileAsync(winimage, "Image.png");
                                    }
                                    else
                                    {
                                        var account = UserAccounts.GetAccount(player2);
                                        account.wins += 1;
                                        UserAccounts.SaveAccount();
                                        TTTRANK(player2, account.wins);
                                    }
                                    
                                    Role_Remove(player1, "Player1Role");
                                    Role_Remove(player2, "Player2Role");
                                    player1 = null;
                                    player2 = null; 
                                    Default();

                                }
                                else
                                {
                                    if (emoji_1 != "1️⃣" && emoji_2 != "2️⃣" && emoji_3 != "3️⃣" && emoji_4 != "4️⃣" && emoji_5 != "5️⃣" && emoji_6 != "6️⃣" && emoji_7 != "7️⃣" && emoji_8 != "8️⃣" && emoji_9 != "9️⃣")
                                    {
                                        Role_Remove(player1, "Player1Role");
                                        Role_Remove(player2, "Player2Role");
                                        player1 = null;
                                        player2 = null;
                                        Default();
                                    }
                                }
                            }
                            else
                            {
                                await Context.Channel.SendMessageAsync("It's not your turn!");
                            }


                        }

                        else
                        {
                            await Context.Channel.SendMessageAsync("You Can't do that!");
                        }
                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync("Please type a number between 1 and 9.");
                    }
                }
                else
                {
                    await Context.Channel.SendMessageAsync("Please start the game first.");
                }
            }
            else
            {
                await Context.Channel.SendMessageAsync("You Can't do that in this channel!");
            }
        }
        //TICTACTOE
        public static bool Role_Check(SocketGuildUser user,string key)
        {
            
            var result = from r in user.Guild.Roles
                         where r.Name == Utilities.GetAlert(key)
                         select r.Id;
            ulong id = result.FirstOrDefault();
            if (id == 0) return false;
            var TargetRole = user.Guild.GetRole(id);
            return user.Roles.Contains(TargetRole);            
    }
        public void Role_Remove(SocketGuildUser user,string key)
        {
            var result = from r in user.Guild.Roles
                         where r.Name == Utilities.GetAlert(key)
                         select r.Id;
            ulong id = result.FirstOrDefault();
            IRole TargetRole = user.Guild.GetRole(id);
            user.RemoveRoleAsync(TargetRole);
        }
        public void Role_Add(SocketGuildUser user,string key)
        {
            var result = from r in user.Guild.Roles
                         where r.Name == Utilities.GetAlert(key)
                         select r.Id;
            ulong id = result.FirstOrDefault();
            IRole TargetRole = user.Guild.GetRole(id);
            user.AddRoleAsync(TargetRole);
            
        }
      
        private void NewEmoji()
        {
             emoji1 = new Emoji(emoji_1);
             emoji2 = new Emoji(emoji_2);
             emoji3 = new Emoji(emoji_3);
             emoji4 = new Emoji(emoji_4);
             emoji5 = new Emoji(emoji_5);
             emoji6 = new Emoji(emoji_6);
             emoji7 = new Emoji(emoji_7);
             emoji8 = new Emoji(emoji_8);
             emoji9 = new Emoji(emoji_9);
             emojix = new Emoji(emoji_10);
             emojio = new Emoji(emoji_11);
        }
        private void TemplateRefresh()
        {
            template = emoji1 + " | " + emoji2 + " | " + emoji3 + "\n" +

"────────" + "\n" +

emoji4 + " | " + emoji5 + " | " + emoji6 + "\n" +

"────────" + "\n" +

emoji7 + " | " + emoji8 + " | " + emoji9;
        }
        private void EmbedMaker()
        {
            Winchecker();
            turnchecker();
            if (winCheck == true)
            {
                if (winner == true)
                {
                    footer = player1.Username.ToString() + " won!";
                }
                else
                {
                    footer = player2.Username.ToString() + " won!";
                }
            }
            else
            {
                if(emoji_1!= "1️⃣"&& emoji_2 != "2️⃣"&& emoji_3 != "3️⃣"&& emoji_4 != "4️⃣"&& emoji_5 != "5️⃣"&& emoji_6 != "6️⃣"&& emoji_7 != "7️⃣"&& emoji_8 != "8️⃣"&& emoji_9 != "9️⃣")
                {
                    footer = "It was a draw!";
                }
                else
                {
                    footer = "It's " + playerturn + "'s" + " turn.";
                }
            }
            embed = new EmbedBuilder();
            embed.WithTitle(player1.Username.ToString() + " VS " + player2.Username.ToString());
            embed.WithDescription(template);
            embed.WithColor(Color.Green);
            embed.WithFooter(footer);
        }
        private void TicTacToeSystem(int PICK)
        {
            if (turn == true)
            {

                switch (PICK)
                {
                    case 1:
                        if ((emoji_1 != emoji_10) && (emoji_1 != emoji_11))
                        {
                            emoji_1 = emoji_10;
                            turn = false;
                        }
                        else
                        {
                            
                        }
                        break;
                    case 2:
                        if ((emoji_2 != emoji_10) && (emoji_2 != emoji_11))
                        {
                            emoji_2 = emoji_10;
                            turn = false;
                        }
                        else
                        {

                        }
                        break;
                    case 3:
                        if ((emoji_3 != emoji_10) && (emoji_3 != emoji_11))
                        {
                            emoji_3 = emoji_10;
                            turn = false;
                        }
                        else
                        {

                        }
                        break;
                    case 4:
                        if ((emoji_4 != emoji_10) && (emoji_4 != emoji_11))
                        {
                            emoji_4 = emoji_10;
                            turn = false;
                        }
                        else
                        {

                        }
                        break;
                    case 5:
                        if ((emoji_5 != emoji_10) && (emoji_5 != emoji_11))
                        {
                            emoji_5 = emoji_10;
                            turn = false;
                        }
                        else
                        {

                        }
                        break;
                    case 6:
                        if ((emoji_6 != emoji_10) && (emoji_6 != emoji_11))
                        {
                            emoji_6 = emoji_10;
                            turn = false;
                        }
                        else
                        {

                        }
                        break;
                    case 7:
                        if ((emoji_7 != emoji_10) && (emoji_7 != emoji_11))
                        {
                            emoji_7 = emoji_10;
                            turn = false;
                        }
                        else
                        {

                        }
                            break;
                        
                        
                    case 8:
                        if ((emoji_8 != emoji_10) && (emoji_8 != emoji_11))
                        {
                            emoji_8 = emoji_10;
                            turn = false;
                        }
                        else
                        {

                        }
                        break;
                    case 9:
                        if ((emoji_9 != emoji_10) && (emoji_9 != emoji_11))
                        {
                            emoji_9 = emoji_10;
                            turn = false;
                        }
                        else
                        {

                        }
                        break;
                    default :
                       
                        break;
                        


                }
            }
            else
            {
                switch (PICK)
                {
                    case 1:
                        if ((emoji_1 != emoji_10) && (emoji_1 != emoji_11))
                        {
                            emoji_1 = emoji_11;
                            turn = true;
                        }
                        else
                        {

                        }
                        break;
                    case 2:
                        if ((emoji_2 != emoji_10) && (emoji_2 != emoji_11))
                        {
                            emoji_2 = emoji_11;
                            turn = true;
                        }
                        else
                        {

                        }
                        break;
                    case 3:
                        if ((emoji_3 != emoji_10) && (emoji_3 != emoji_11))
                        {
                            emoji_3 = emoji_11;
                            turn = true;
                        }

                        else
                        {

                        }
                        break;
                    case 4:
                        if ((emoji_4 != emoji_10) && (emoji_4 != emoji_11))
                        {
                            emoji_4 = emoji_11;
                            turn = true;
                        }
                        else
                        {

                        }
                        break;
                    case 5:
                        if ((emoji_5 != emoji_10) && (emoji_5 != emoji_11))
                        {
                            emoji_5 = emoji_11;
                            turn = true;
                        }
                        else
                        {

                        }
                        break;
                    case 6:
                        if ((emoji_6 != emoji_10) && (emoji_6 != emoji_11))
                        {
                            emoji_6 = emoji_11;
                            turn = true;
                        }
                        else
                        {

                        }
                        break;
                    case 7:
                        if ((emoji_7 != emoji_10) && (emoji_7 != emoji_11))
                        {
                            emoji_7 = emoji_11;
                            turn = true;
                        }
                        else
                        {

                        }
                        break;
                    case 8:
                        if ((emoji_8 != emoji_10) && (emoji_8 != emoji_11))
                        {

                            emoji_8 = emoji_11;
                            turn = true;
                        }
                        else
                        {

                        }
                        break;
                    case 9:
                        if ((emoji_9 != emoji_10) && (emoji_9 != emoji_11))
                        {
                            emoji_9 = emoji_11;
                            turn = true;
                        }else
                        {

                        }
                        break;
                    default:

                        break;



                }
            }
        }
        private void turnchecker()
        {
            if (turn == true)
            {
                playerturn = player1.Username.ToString();
            }
            else
            {
                playerturn = player2.Username.ToString();
            }
           
        }
        private void Winchecker()
        {
        if((emoji_1==emoji_10)&&(emoji_2==emoji_10)&&(emoji_3 == emoji_10))
            {
                winCheck = true;
                winner = true;
            }
            else if((emoji_4 == emoji_10) && (emoji_5 == emoji_10) && (emoji_6 == emoji_10))
            {
                winCheck = true;
                winner = true;
            }
            else if ((emoji_7== emoji_10) && (emoji_8 == emoji_10) && (emoji_9 == emoji_10))
            {
                winCheck = true;
                winner = true;
            }
            else if ((emoji_1 == emoji_10) && (emoji_4 == emoji_10) && (emoji_7 == emoji_10))
            {
                winCheck = true;
                winner = true;
            }
            else if ((emoji_2 == emoji_10) && (emoji_5 == emoji_10) && (emoji_8 == emoji_10))
            {
                winCheck = true;
                winner = true;
            }
            else if ((emoji_3 == emoji_10) && (emoji_6 == emoji_10) && (emoji_9 == emoji_10))
            {
                winCheck = true;
                winner = true;
            }
            else if ((emoji_1 == emoji_10) && (emoji_5 == emoji_10) && (emoji_9 == emoji_10))
            {
                winCheck = true;
                winner = true;
            }
            else if ((emoji_3 == emoji_10) && (emoji_5 == emoji_10) && (emoji_7 == emoji_10))
            {
                winCheck = true;
                winner = true;
            }
            else if ((emoji_1 == emoji_11) && (emoji_2 == emoji_11) && (emoji_3 == emoji_11))
            {
                winCheck = true;
                winner = false;
            }
            else if ((emoji_4 == emoji_11) && (emoji_5 == emoji_11) && (emoji_6 == emoji_11))
            {
                winCheck = true;
                winner = false;
            }
            else if ((emoji_7 == emoji_11) && (emoji_8 == emoji_11) && (emoji_9 == emoji_11))
            {
                winCheck = true;
                winner = false;
            }
            else if ((emoji_1 == emoji_11) && (emoji_4 == emoji_11) && (emoji_7 == emoji_11))
            {
                winCheck = true;
                winner = false;
            }
            else if ((emoji_2 == emoji_11) && (emoji_5 == emoji_11) && (emoji_8 == emoji_11))
            {
                winCheck = true;
                winner = false;
            }
            else if ((emoji_3 == emoji_11) && (emoji_6 == emoji_11) && (emoji_9 == emoji_11))
            {
                winCheck = true;
                winner = false;
            }
            else if ((emoji_1 == emoji_11) && (emoji_5 == emoji_11) && (emoji_9 == emoji_11))
            {
                winCheck = true;
                winner = false;
            }
            else if ((emoji_3 == emoji_11) && (emoji_5 == emoji_11) && (emoji_7 == emoji_11))
            {
                winCheck = true;
                winner = false;
            }
        }
        public void Default()
        {
         emoji_1 = "1️⃣";
         emoji_2 = "2️⃣";
         emoji_3 = "3️⃣";
         emoji_4 = "4️⃣";
         emoji_5 = "5️⃣";
         emoji_6 = "6️⃣";
         emoji_7 = "7️⃣";
         emoji_8 = "8️⃣";
         emoji_9 = "9️⃣";
         emoji_10 = "❌";
         emoji_11 = "⭕";
         template="";
         turn = true;
         TICTACTOE = false;
         winCheck=false;
        footer="";

    }
        private void TTTRANK(SocketGuildUser user,uint WINS)
        {
            switch (WINS)
            {
                case 10:
                    Role_Add(user, "TTTBronzeRole");
                    break;
                case 20:
                    Role_Remove(user, "TTTBronzeRole");
                    Role_Add(user, "TTTSilverRole");
                    break;
                case 30:
                    Role_Remove(user, "TTTBronzeRole");
                    Role_Remove(user, "TTTSilverRole");
                    Role_Add(user, "TTTGoldRole");
                    break;
                case 40:
                    Role_Remove(user, "TTTBronzeRole");
                    Role_Remove(user, "TTTSilverRole");
                    Role_Remove(user, "TTTGoldRole");
                    Role_Add(user, "TTTDiamondRole");
                    break;
                case 50:
                    Role_Remove(user, "TTTBronzeRole");
                    Role_Remove(user, "TTTSilverRole");
                    Role_Remove(user, "TTTGoldRole");
                    Role_Remove(user, "TTTDiamondRole");
                    Role_Add(user, "TTTMasterRole");
                    break;

                default:
                    break;
            }
        }
        public void  image_generator(SocketGuildUser user)
        {
            string css = "<style>\n h1{\n    color:#BB0A21 ;\n border-radius:20px;\n font-family: Arial, Helvetica, sans-serif;\n font-size:150%;\n}\n</style>\n";
            string html = String.Format("<body bgcolor=383838><h1>{0} Won!</h1>\n</body>", user.Username);
            var Convertor = new HtmlToImageConverter
            {
                Width = 280,
                Height = 80

            };
            var pngbytes = Convertor.GenerateImage(css + html, ImageFormat.Png);
            winimage = new MemoryStream(pngbytes);
            
        }
        


    }
    
}
