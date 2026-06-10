using Newtonsoft.Json;
using 工具基础核心库.BaseTool.ConfigHelper;

namespace 获取Tracker
{
    public class AppSettings : IConfig
    {
        /// <summary>
        /// Tracker源地址集合
        /// </summary>
        [JsonProperty("trackerSourceUrlList")]
        public string[] TrackerSourceUrlList { get; set; }

        /// <summary>
        /// 格式化模板
        /// </summary>
        [JsonProperty("trackerFormatTemplet")]
        public string TrackerFormatTemplet { get; set; }

        /// <summary>
        /// qt配置文件路径
        /// </summary>
        [JsonProperty("qtConfigFilePath")]
        public string QtConfigFilePath { get; set; }

        /// <summary>
        /// Key名-追加Tracker
        /// </summary>
        [JsonProperty("keyName_AppendTracker")]
        public string KeyName_AppendTracker { get; set; }

        public void TryInitDefaultConfig()
        {
            this.TrackerFormatTemplet = @"{url}\n";
            this.KeyName_AppendTracker = @"Session\AdditionalTrackers";
        }
    }
}