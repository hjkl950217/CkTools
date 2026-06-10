namespace 技术点验证
{
    /// <summary>
    /// Nair需要的配置类实体
    /// </summary>
    public class NairOption
    {
        public Nairsdksettings? NairSDKSettings { get; set; }
    }

    public class Nairsdksettings
    {
        /// <summary>
        /// 默认环境
        /// </summary>
        public string? DefaultCluster { get; set; }

        /// <summary>
        /// 对应环境的配置
        /// </summary>
        public Clusters? Clusters { get; set; }
    }

    /// <summary>
    /// 对应环境的配置
    /// </summary>
    public class Clusters
    {
        public GdevCluster? GDEVCluster { get; set; }
        public GqcCluster? GQCCluster { get; set; }
        public Wh7Cluster? WH7Cluster { get; set; }
    }

    public class GdevCluster
    {
        public string? NairServerIps { get; set; }
        public string? DataBaseLocation { get; set; }
    }

    public class GqcCluster
    {
        public string? NairServerIps { get; set; }
        public string? DataBaseLocation { get; set; }
    }

    public class Wh7Cluster
    {
        public string? NairServerIps { get; set; }
        public string? DataBaseLocation { get; set; }
    }
}