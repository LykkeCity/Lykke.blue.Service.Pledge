namespace Lykke.Service.Pledges.Core.Domain
{
    public interface IPledge
    {
        string Id { get; }
        string ClientId { get; }
        int CO2Footprint { get; }
        int ClimatePositiveValue { get; }
    }
}
