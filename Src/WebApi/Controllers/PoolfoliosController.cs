using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SafeStak.Deltas.WebApi.Controllers
{
    [ApiController]
    public class PoolfoliosController
    {
        
        [HttpGet]
        [Route("poolfolio/v1/pools/{poolId:required}/metrics")]
        public Task<string> Get(CancellationToken ct)
        {
            return Task.FromResult("[]");
        }
    }
}
