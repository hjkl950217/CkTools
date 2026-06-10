using Newtonsoft.Json;

namespace 工具基础核心库.BaseTool.ConfigHelper
{
    public static class ConfigHelper
    {
        public static Dictionary<string, object> configCacheDic = new();

        public const string ConfigPath = "config.json";

        public static T GetConfig<T>(string configFilePath = ConfigPath)
            where T : IConfig, new()
        {
            // 如果配置文件不存在，则创建默认配置文件
            if(!File.Exists(configFilePath))
            {
                T defaultConfigObj = new();
                defaultConfigObj.TryInitDefaultConfig();
                string jsonContent = JsonConvert.SerializeObject(defaultConfigObj, Formatting.Indented);
                File.WriteAllText(ConfigPath, jsonContent);
                return defaultConfigObj;
            }

            // 如果配置文件已经加载，则直接返回
            bool isExist = configCacheDic.TryGetValue(typeof(T).Name, out object oldConfigObj);
            if(isExist)
            {
                return (T)oldConfigObj;
            }

            // 读取 JSON 配置文件
            string json = File.ReadAllText(configFilePath);

            // 解析 JSON 文件
            T newConfig = JsonConvert.DeserializeObject<T>(json);
            ConfigHelper.configCacheDic.Add(typeof(T).Name, newConfig);

            return newConfig;
        }
    }
}