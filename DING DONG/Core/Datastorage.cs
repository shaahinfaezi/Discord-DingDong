using DING_DONG.Core.UserAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace DING_DONG.Core
{
    public static class Datastorage
    {
        //set  
        public static void SaveUserAccounts(IEnumerable<UserAccount> accounts, string folderpath, string filepath)
        {
          
            string json = JsonConvert.SerializeObject(accounts, Formatting.Indented);
            File.WriteAllText(folderpath + "/" + filepath, json);


        }




        //get
        public static IEnumerable<UserAccount> GetUserAccounts(string folderpath, string filepath)
        {
            if (Directory.Exists(folderpath) && File.Exists(folderpath + "/" + filepath))
            {
                string json = File.ReadAllText(folderpath + "/"+filepath);
                return JsonConvert.DeserializeObject<List<UserAccount>>(json);
            }
            else
            {
                return null;
            }

        }
        public static bool FileExists(string filepath)
        {
            return File.Exists(filepath);
        }
    }
}   

