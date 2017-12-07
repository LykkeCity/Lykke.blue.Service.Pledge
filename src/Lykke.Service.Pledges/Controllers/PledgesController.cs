using AutoMapper;
using Common.Log;
using Lykke.Service.Pledges.Core.Domain;
using Lykke.Service.Pledges.Core.Services;
using Lykke.Service.Pledges.Requests;
using Lykke.Service.Pledges.Responses;
using Lykke.Service.Pledges.Strings;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.SwaggerGen.Annotations;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Lykke.Service.Pledges.Controllers
{
    [Route("api/pledges")]
    public class PledgesController : Controller
    {
        private readonly ILog _log;
        private readonly IPledgesService _pledgesService;

        public PledgesController(
            ILog log,
            IPledgesService pledgesService)
        {
            _log = log ?? throw new ArgumentException(nameof(log));
            _pledgesService = pledgesService ?? throw new ArgumentException(nameof(pledgesService));
        }

        /// <summary>
        /// Create a new pledge.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation("CreatePledge")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Create([FromBody] CreatePledgeRequest request)
        {
            if (request == null)
            {
                return BadRequest(Phrases.InvalidRequest);
            }

            if (String.IsNullOrEmpty(request.ClientId))
            {
                return BadRequest(Phrases.InvalidClientId);
            }

            var pledgesLimitReached = await _pledgesService.IsPledgesLimitReached(request.ClientId);

            if (pledgesLimitReached)
            {
                return BadRequest(Phrases.PledgesLimitReached);
            }

            await _pledgesService.Create(request);

            return Created(uri: $"api/pledges/{request.ClientId}", value: string.Empty);
        }

        /// <summary>
        /// Get pledge for provided client. 
        /// </summary>
        /// <param name="clientId">Id of the client we wanna get pledge for.</param>
        /// <returns></returns>
        [HttpGet("{clientId}")]
        [SwaggerOperation("GetPledge")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(GetPledgeResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(string clientId)
        {
            if (String.IsNullOrEmpty(clientId))
            {
                return BadRequest(Phrases.InvalidRequest);
            }

            var pledge = await _pledgesService.GetPledgeByClientId(clientId);

            if (pledge == null)
            {
                return NotFound(Phrases.PledgeNotFoundByClientId);
            }

            var result = Mapper.Map<GetPledgeResponse>(pledge);

            return Ok(result);
        }

        /// <summary>
        /// Update pledge details.
        /// </summary>
        /// <param name="id">Id of the pledge we wanna update.</param>
        /// <param name="request">Pledge values we wanna change.</param>
        /// <returns></returns>
        [HttpPut]
        [SwaggerOperation("UpdatePledge")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Update([FromBody] UpdatePledgeRequest request)
        {
            if (request == null)
            {
                return BadRequest(Phrases.InvalidRequest);
            }

            if (String.IsNullOrEmpty(request.ClientId))
            {
                return BadRequest(Phrases.InvalidClientId);
            }

            var pledge = await _pledgesService.GetPledgeByClientId(request.ClientId);

            if (pledge == null)
            {
                return NotFound(Phrases.PledgeNotFound);
            }

            request.Id = pledge.Id;

            await _pledgesService.Update(Mapper.Map<IPledge>(request));

            return NoContent();
        }

        /// <summary>
        /// Delete pledge
        /// </summary>
        /// <param name="clientId">Id of the pledge we wanna delete.</param>
        /// <returns></returns>
        [HttpDelete("{clientId}")]
        [SwaggerOperation("DeletePledge")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(string clientId)
        {
            if (String.IsNullOrEmpty(clientId))
            {
                return BadRequest(Phrases.InvalidRequest);
            }

            var pledge = await _pledgesService.GetPledgeByClientId(clientId);

            if (pledge == null)
            {
                return NotFound(Phrases.PledgeNotFound);
            }

            await _pledgesService.Delete(pledge.Id);

            return NoContent();
        }
    }
}
