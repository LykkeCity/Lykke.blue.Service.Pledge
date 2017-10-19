using Common.Log;
using JetBrains.Annotations;
using Lykke.Service.ClientAccount.Client;
using Lykke.Service.Pledges.Core.Domain;
using Lykke.Service.Pledges.Core.Services;
using Lykke.Service.Pledges.Models.Pledge;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.SwaggerGen.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Lykke.Service.Pledges.Controllers
{
    [Route("api/[controller]")]
    public class PledgesController : Controller
    {
        private readonly IPledgeRepository _pledgeRepository;
        private readonly ILog _log;
        private readonly IClientAccountClient _clientAccountClient;
        private readonly IShutdownManager _shutdownManager;

        public PledgesController(
            [NotNull] IPledgeRepository pledgeRepository, 
            ILog log, 
            IClientAccountClient clientAccountClient)
        {
            _log = log ?? throw new ArgumentException(nameof(log));
            _pledgeRepository = pledgeRepository ?? throw new ArgumentException(nameof(pledgeRepository));
            _clientAccountClient = clientAccountClient ?? throw new ArgumentException(nameof(clientAccountClient));
        }

        /// <summary>
        /// Create a new pledge.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation("CreatePledge")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(CreatePledgeResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreatePledgeRequest request)
        {
            if(request == null)
            {
                return BadRequest();
            }

            if(String.IsNullOrEmpty(request.ClientId) || await _clientAccountClient.GetClientById(request.ClientId) == null)
            {
                return NotFound();
            }

            var pledge = await _pledgeRepository.Create(request.ClientId, request.CO2Footprint, request.ClimatePositiveValue);

            var result = CreatePledgeResponse.Create(pledge);

            return Ok(result);
        }

        /// <summary>
        /// Get pledge.
        /// </summary>
        /// <param name="id">Id of the pledge we wanna find.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [SwaggerOperation("GetPledge")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(GetPledgeResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(string id)
        {
            if(String.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            var pledge = await _pledgeRepository.Get(id);

            if(pledge == null)
            {
                return NotFound();
            }

            var result = GetPledgeResponse.Create(pledge);

            return Ok(result);
        }

        /// <summary>
        /// Get pledges for provided client. 
        /// </summary>
        /// <param name="id">Id of the client we wanna get pledges for.</param>
        /// <returns></returns>
        [HttpGet("client/{id}")]
        [SwaggerOperation("GetPledgesByClientId")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<GetPledgeResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPledgesByClientId(string id)
        {
            if(String.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            var pledges = await _pledgeRepository.GetPledgesByClientId(id);

            var result = pledges.Select(x => GetPledgeResponse.Create(x));

            return Ok(result);
        }

        /// <summary>
        /// Update pledge details.
        /// </summary>
        /// <param name="id">Id of the pledge we wanna update.</param>
        /// <param name="request">Pledge values we wanna change.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [SwaggerOperation("UpdatePledge")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(UpdatePledgeResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update(string id, [FromBody] UpdatePledgeRequest request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            if (String.IsNullOrEmpty(id) || await _clientAccountClient.GetClientById(request.ClientId) == null)
            {
                return NotFound();
            }

            var pledge = await _pledgeRepository.UpdatePledge(request.Create(id));

            var result = UpdatePledgeResponse.Create(pledge);

            return Ok(result);
        }

        /// <summary>
        /// Delete pledge
        /// </summary>
        /// <param name="id">Id of the pledge we wanna delete.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [SwaggerOperation("DeletePledge")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(string id)
        {
            if(String.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            var pledge = await _pledgeRepository.Get(id);

            if (pledge == null)
            {
                return NotFound();
            }

            await _pledgeRepository.Delete(id);

            return Ok();
        }
    }
}
