using Lykke.Service.Pledges.Core.Settings.ServiceSettings;
using Lykke.Service.Pledges.Core.Settings.SlackNotifications;

namespace Lykke.Service.Pledges.Core.Settings
{
    public class AppSettings
    {
        public PledgesSettings PledgesService { get; set; }
        public SlackNotificationsSettings SlackNotifications { get; set; }
    }
}
