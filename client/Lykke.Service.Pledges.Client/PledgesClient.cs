using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Service.Pledges.Client.AutorestClient;
using Lykke.Service.Pledges.Client.AutorestClient.Models;

namespace Lykke.Service.Pledges.Client
{
    public class PledgesClient : IPledgesClient, IDisposable
    {
        private readonly ILog _log;
        private PledgesAPI _service;

        public PledgesClient(string serviceUrl, ILog log)
        {
            _log = log;
            _service = new PledgesAPI(new Uri(serviceUrl));
        }

        public void Dispose()
        {
            if (_service == null)
                return;
            _service.Dispose();
            _service = null;
        }

        public async Task<CreatePledgeResponse> Create(CreatePledgeRequest request)
        {
            try
            {
                return await _service.CreatePledgeAsync(request);
            }
            catch (Exception ex)
            {
                await _log.WriteErrorAsync(nameof(PledgesClient), nameof(Create), ex);
                throw;
            }
        }

        public async Task Delete(string id)
        {
            try
            {
                await _service.DeletePledgeAsync(id);
            }
            catch (Exception ex)
            {
                await _log.WriteErrorAsync(nameof(PledgesClient), nameof(Delete), ex);
                throw;
            }
        }        

        public async Task<GetPledgeResponse> Get(string id)
        {
            try
            {
                return await _service.GetPledgeAsync(id);
            }
            catch (Exception ex)
            {
                await _log.WriteErrorAsync(nameof(PledgesClient), nameof(Get), ex);
                throw;
            }
        }

        public async Task<IEnumerable<GetPledgeResponse>> GetPledgesByClientId(string id)
        {
            try
            {
                return await _service.GetPledgesByClientIdAsync(id);
            }
            catch (Exception ex)
            {
                await _log.WriteErrorAsync(nameof(PledgesClient), nameof(GetPledgesByClientId), ex);
                throw;
            }
        }

        public async Task<UpdatePledgeResponse> Update(UpdatePledgeRequest request)
        {
            try
            {
                return await _service.UpdatePledgeAsync(request);
            }
            catch (Exception ex)
            {
                await _log.WriteErrorAsync(nameof(PledgesClient), nameof(Update), ex);
                throw;
            }
        }
    }
}
