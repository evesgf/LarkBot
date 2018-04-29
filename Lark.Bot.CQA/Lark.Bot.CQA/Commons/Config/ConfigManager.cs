using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lark.Bot.CQA.Commons.Config
{
    public class ConfigManager
    {
        #region 单例
        private static object _lock = new object();
        private static ConfigManager instance;
        public static ConfigManager Instance()
        {
            return instance;
        }

        public static ConfigManager Create()
        {
            if (instance == null)
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new ConfigManager();
                        InitConfig();
                    }
                }
            }
            return instance;
        }
        #endregion

        public static readonly string configDirectory = "/LarkBotConfig/";

        public static LarkBotConfig larkBotConfig;

        private static void InitConfig()
        {
            larkBotConfig = LoadConfig<LarkBotConfig>(Environment.CurrentDirectory+ configDirectory + "larkBotConfig.json") as LarkBotConfig;
        }

        public static ConfigBase LoadConfig<T>(string path) where T : ConfigBase
        {
            if (!File.Exists(path)) return null;

            //读取
            StreamReader sr = new StreamReader(path);
            if (sr == null) return null;
            string json = sr.ReadToEnd();
            sr.Close();
            if (json.Length <= 0) return null;

            return JsonConvert.DeserializeObject<T>(json);
        }
    }

    public interface ConfigBase
    {

    }
}
