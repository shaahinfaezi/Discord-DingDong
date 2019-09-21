using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using Discord.WebSocket;
using Discord.Commands;


namespace DING_DONG
{
    

    class Utilities 
    {
      
        private static Dictionary<string, string> alerts;
        static Utilities()
        {
            string json = File.ReadAllText("SystemLang/Alerts.json");
            var data = JsonConvert.DeserializeObject<dynamic>(json);
            alerts = data.ToObject<Dictionary<string, string>>();
        }
        public static string GetAlert(string key)
        {
            if(alerts.ContainsKey(key))
            {
                return alerts[key];
            }
            else
            {
                return "";
            }
         

            
        }
       
            
        }
       
    }

