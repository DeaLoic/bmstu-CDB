using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.DTO;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("/api/v1/flow/[controller]")]
    public class FlowController : ControllerBase
    {

        public FlowController()
        {
        }

        [HttpGet]
        public IEnumerable<FlowDTO> Get()
        {
            return null;
        }
    }
}
