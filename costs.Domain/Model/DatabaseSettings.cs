using costs.Domain.Interfaces;

namespace costs.Domain.Model
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
