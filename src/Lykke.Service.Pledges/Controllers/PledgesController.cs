using AutoMapper;
using Common.Log;
using JetBrains.Annotations;
using Lykke.Service.ClientAccount.Client;
using Lykke.Service.Pledges.Core.Domain;
using Lykke.Service.Pledges.Core.Services;
using Lykke.Service.Pledges.Requests;
using Lykke.Service.Pledges.Responses;
using Lykke.Service.Pledges.Strings;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.SwaggerGen.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Lykke.Service.Pledges.Controllers
{
    [Route("api/pledges")]
    public class PledgesController : Controller
    {
        private readonly ILog _log;
        private readonly IClientAccountClient _clientAccountClient;
        private readonly IShutdownManager _shutdownManager;
        private readonly IPledgesService _pledgesService;

        public PledgesController(
            ILog log, 
            IClientAccountClient clientAccountClient,
            IPledgesService pledgesService)
        {
            _log = log ?? throw new ArgumentException(nameof(log));
            _clientAccountClient = clientAccountClient ?? throw new ArgumentException(nameof(clientAccountClient));
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
        [ProducesResponseType(typeof(CreatePledgeResponse), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Create([FromBody] CreatePledgeRequest request)
        {
            if(request == null)
            {
                return BadRequest(Phrases.InvalidRequest);
            }

            if(String.IsNullOrEmpty(request.ClientId) || await _clientAccountClient.GetClientById(request.ClientId) == null)
            {
                return BadRequest(Phrases.InvalidClientId);
            }

            var pledgesLimitReached = await _pledgesService.IsPledgesLimitReached(request.ClientId);

            if(pledgesLimitReached)
            {
                return BadRequest(Phrases.PledgesLimitReached);
            }

            var pledge = Mapper.Map<CreatePledgeResponse>(await _pledgesService.Create(request));

            return Created(uri: $"api/pledges/{pledge.Id}", value: pledge);
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
                return BadRequest(Phrases.InvalidRequest);
            }

            var pledge = await _pledgesService.Get(id);

            if(pledge == null)
            {
                return NotFound(Phrases.PledgeNotFound);
            }

            var result = Mapper.Map<GetPledgeResponse>(pledge);

            return Ok(result);
        }

        /// <summary>
        /// Get pledge for provided client. 
        /// </summary>
        /// <param name="id">Id of the client we wanna get pledge for.</param>
        /// <returns></returns>
        [HttpGet("client/{id}")]
        [SwaggerOperation("GetPledgeByClientId")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(GetPledgeResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPledgeByClientId(string id)
        {
            if(String.IsNullOrEmpty(id))
            {
                return BadRequest(Phrases.InvalidRequest);
            }

            var pledge = await _pledgesService.GetPledgeByClientId(id);

            if(pledge == null)
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
        [ProducesResponseType(typeof(UpdatePledgeResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] UpdatePledgeRequest request)
        {
            if (request == null)
            {
                return BadRequest(Phrases.InvalidRequest);
            }

            if (String.IsNullOrEmpty(request.Id) || await _clientAccountClient.GetClientById(request.ClientId) == null)
            {
                return BadRequest(Phrases.InvalidClientId);
            }

            var pledge = await _pledgesService.Get(request.Id);

            if (pledge == null)
            {
                return NotFound(Phrases.PledgeNotFound);
            }

            pledge = await _pledgesService.Update(Mapper.Map<IPledge>(request));

            var result = Mapper.Map<UpdatePledgeResponse>(pledge);

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
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(string id)
        {
            if(String.IsNullOrEmpty(id))
            {
                return BadRequest(Phrases.InvalidRequest);
            }

            var pledge = await _pledgesService.Get(id);

            if (pledge == null)
            {
                return NotFound(Phrases.PledgeNotFound);
            }

            await _pledgesService.Delete(id);

            return NoContent();
        }
    }
}
