using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhantomJSChamps.Models;
using System.IO;

namespace PhantomJSChamps
{
    public class BotIO : IBotIO
    {
        public static BotIO Instance { get { return new BotIO(); } }

        public BotIO()
        {
           
        }

        public string[] LoadProxyList()
        {
            var checkDir = Directory.Exists(Directory.GetCurrentDirectory() + "\\AppFiles");

            if (!checkDir)
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\AppFiles");
            }

            var checkProxieFileExist = File.Exists(Directory.GetCurrentDirectory() + "\\AppFiles" + "\\Proxie.json");
            var JsonFilePath = Directory.GetCurrentDirectory() + "\\AppFiles" + "\\Proxie.json";

            if (!checkProxieFileExist)
            {
                File.Create(Directory.GetCurrentDirectory() + "\\AppFiles" + "\\Proxie.json");

            }
            else
            {
                var file = File.ReadAllText(JsonFilePath);
                var json = Newtonsoft.Json.JsonConvert.DeserializeObject<ProxyList>(file);
                return json.Proxies;
            }

            return new string[] { };

        }

        public (string UserAgent ,string Proxy,string Header)[] LoadUserProfiles()
        {
            throw new NotImplementedException();
        }

        public UserInfo LoadUserInfo()
        {
            throw new NotImplementedException();
        }
    }
}
