
using Lykke.Service.Pledges.Client.AutorestClient.Models;
using System.Threading.Tasks;

namespace Lykke.Service.Pledges.Client
{
    public interface IPledgesClient
    {
        /// <summary>
        /// Create a new pledge.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task Create(CreatePledgeRequest request);

        /// <summary>
        /// Get pledge for provided client.
        /// </summary>
        /// <param name="clientId">Id of the client we wanna get pledge for.</param>
        /// <returns></returns>
        Task<GetPledgeResponse> Get(string clientId);

        /// <summary>
        /// Update pledge details.
        /// </summary>
        /// <param name="id">Id of the pledge we wanna update.</param>
        /// <param name="request">Pledge values we wanna change.</param>
        /// <returns></returns>
        Task Update(UpdatePledgeRequest request);

        /// <summary>
        /// Delete pledge
        /// </summary>
        /// <param name="clientId">Id of the pledge we wanna delete.</param>
        /// <returns></returns>
        Task Delete(string id);
    }
}
