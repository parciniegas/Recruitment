using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Sequences.Api.V1.Controllers
{
    [Authorize]
    [ApiController]
    [Route("v{version:apiVersion}/sequences")]
    public class SequencesController : ControllerBase
    {
        [HttpGet("next")]
        public string GetNext(int subjectId)
        {
            return string.Empty;
        }
    }
}