namespace CommonLibrary.RedisCache
{
    public class RedisCacheConfigModel
    {
        public string Configuration { get; set; }
        public string InstanceName { get; set; }

        public bool IsConfigured()
        {
            return !string.IsNullOrWhiteSpace(Configuration) && !string.IsNullOrWhiteSpace(InstanceName);
        }
    }
}
