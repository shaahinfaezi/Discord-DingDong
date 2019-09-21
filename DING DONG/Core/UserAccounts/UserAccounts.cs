using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DING_DONG.Core.UserAccounts
{
    public static class UserAccounts
    {
        private static List<UserAccount> accounts;
        private static string AccountFolder = "Resources";
        private static string AccountFile = "accounts.json";

        static UserAccounts()
        {
            if (Datastorage.FileExists(AccountFolder + "/" + AccountFile))
            {
                accounts = Datastorage.GetUserAccounts(AccountFolder, AccountFile).ToList();
            }
            else
            {
                accounts = new List<UserAccount>();
                SaveAccount();

            }

        }
        public static void SaveAccount()
        {
            Datastorage.SaveUserAccounts(accounts, AccountFolder, AccountFile);
        }
        public static UserAccount GetAccount(SocketUser user)
        {
            return GetOrCreateAccount(user.Id);
        }
        private static UserAccount GetOrCreateAccount(ulong id)
        {
            var result = from a in accounts
                         where a.ID == id
                         select a;
            var account = result.FirstOrDefault();
            if (account == null) account = CreateUserAccount(id);
            return account;
         
        }
        private static UserAccount CreateUserAccount(ulong id)
        {
            var newAccount = new UserAccount()
            {
                ID = id,
                wins = 0
            };
            accounts.Add(newAccount);
            SaveAccount();  
            return newAccount;
        }
    }
}
