using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lark.Bot.CQA.Services.CoinmarketcapService;

namespace Lark.Bot.CQA.Uitls.Config
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
        public static PushNewsConfig pushNewsConfig;

        //用于检测复读机
        public static string lastMessage = "";
        public static string lastLastMessage = "";

        private static Dictionary<string, string> dict_CoinSymbol;

        private static void InitConfig()
        {

            larkBotConfig = LoadConfig<LarkBotConfig>(Environment.CurrentDirectory+ configDirectory + "larkBotConfig.json") as LarkBotConfig;
            pushNewsConfig = LoadConfig<PushNewsConfig>(Environment.CurrentDirectory + configDirectory + "PushNewsConfig.json") as PushNewsConfig;
        }

        /// <summary>
        /// 获取币对列表
        /// </summary>
        private static async void PullCoinList()
        {
            dict_CoinSymbol = new Dictionary<string, string>();
            HttpResult httpResult = await HttpUitls.HttpGetRequestAsync("https://api.coinmarketcap.com/v1/ticker/?limit=0");

            if (!httpResult.Success) return;

            var models = JsonConvert.DeserializeObject<CoinmarketcapTicker[]>(httpResult.StrResponse);
            foreach (var model in models)
            {
                if (!dict_CoinSymbol.ContainsKey(model.symbol.ToLower()))
                {
                    dict_CoinSymbol.Add(model.symbol.ToLower(), model.id);
                }
                else
                {
                    var a = dict_CoinSymbol[model.symbol.ToLower()];
                    //TODO:symbol可能有重复的？？？
                }
            }
        }

        /// <summary>
        /// 加载配置文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        private static ConfigBase LoadConfig<T>(string path) where T : ConfigBase
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

        /// <summary>
        /// 输入key查询symbol，小写
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string CheckSymbol(string key)
        {
            if (dict_CoinSymbol == null)
            {
                PullCoinList();
            }

            if (dict_CoinSymbol.ContainsKey(key.ToLower()))
            {
                return dict_CoinSymbol[key.ToLower()];
            }
            else
            {
                return null;
            }
        }
    }

    public interface ConfigBase
    {

    }
}
