using Newtonsoft.Json;
using 工具基础核心库.BaseTool.ConfigHelper;

namespace linux文件复制工具
{
    public class AppSettings : IConfig
    {
        /// <summary>
        /// 最小文件限制（M）
        /// </summary>
        [JsonProperty("minFileSizeLimit")]
        public int MinFileSizeLimit { get; set; }

        /// <summary>
        /// 最大文件限制（M）
        /// </summary>
        [JsonProperty("maxFileSizeLimit")]
        public int MaxFileSizeLimit { get; set; }

        /// <summary>
        /// 允许复制的后缀名
        /// </summary>
        [JsonProperty("allowedExtensions")]
        public string[] AllowedExtensions { get; set; }

        /// <summary>
        /// 从QB下载完成的目录复制到归档目录（115）时， 使用的配置文件
        /// </summary>
        [JsonProperty("archiveFile")]
        public ArchiveFileAppSettings ArchiveFile { get; set; }

        /// <summary>
        /// 从115下载完成的目录复制到QB目录时， 使用的配置文件
        /// </summary>
        [JsonProperty("archiveFolder")]
        public ArchiveFolderAppSettings ArchiveFolder { get; set; }

        /// <summary>
        /// 尝试初始化配置
        /// </summary>
        public void TryInitDefaultConfig()
        {
            this.MinFileSizeLimit = this.MinFileSizeLimit == 0 ? 20 : this.MinFileSizeLimit;
            this.MaxFileSizeLimit = this.MaxFileSizeLimit == 0 ? 15 * 1024 : this.MaxFileSizeLimit;

            this.AllowedExtensions = this.AllowedExtensions ?? ["avi", "mp4", "ts", "wmv", "mpeg", "m4v", "mov", "asf", "flv", "f4v", "rmvb", "3gp", "mkv", "vob", "mts", "m2ts", "tp", "trp", "divx", "xvid"];

            this.ArchiveFile.TryInitDefaultConfig();
        }
    }

    /// <summary>
    /// 从QB下载完成的目录复制到归档目录（115）时， 使用的配置文件
    /// </summary>
    public class ArchiveFileAppSettings : IConfig
    {
        /// <summary>
        /// 源地址-完整地址
        /// </summary>
        [JsonProperty("source")]
        public string Source { get; set; }

        /// <summary>
        /// 目标地址-完整地址
        /// </summary>
        [JsonProperty("target")]
        public string Target { get; set; }

        /// <summary>
        /// 时间点，大于这个时间节点的数据不会被复制
        /// </summary>
        [JsonProperty("timeLimit")]
        public DateTime? TimeLimit { get; set; }

        /// <summary>
        /// 排除地址
        /// </summary>
        [JsonProperty("excludeAddrs")]
        public string[] ExcludeAddrs { get; set; }

        /// <summary>
        /// 尝试初始化配置
        /// </summary>
        public void TryInitDefaultConfig()
        {
            this.Source = string.IsNullOrEmpty(this.Source) ? "Y:\\qBittorrentDownload" : this.Source;
            this.Target = string.IsNullOrEmpty(this.Target) ? "Y:\\LoActionMovie\\115" : this.Target;
            this.TimeLimit = this.TimeLimit ?? DateTime.MinValue;
            this.ExcludeAddrs = this.ExcludeAddrs ?? ["Y:\\qBittorrentDownload\\incomplete"];
        }
    }

    /// <summary>
    /// 从115下载完成的目录复制到QB目录时， 使用的配置文件
    /// </summary>
    public class ArchiveFolderAppSettings : IConfig
    {
        /// <summary>
        /// 源地址-115下载完成的地址
        /// </summary>
        [JsonProperty("sourceAddr")]
        public string SourceAddr { get; set; }

        /// <summary>
        /// 目标地址-qb未完成任务的地址
        /// </summary>
        [JsonProperty("targetAddr")]
        public string TargetAddr { get; set; }

        public void TryInitDefaultConfig()
        {
        }
    }
}