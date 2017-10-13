namespace Lykke.Service.Pledges.Core.Settings.ServiceSettings
{
    public class PledgesSettings
    {
        public DbSettings Db { get; set; }
        public decimal TreeCO2Mitigation { get; set; }
        public ServiceSettings Services { get; set; }
    }
}
