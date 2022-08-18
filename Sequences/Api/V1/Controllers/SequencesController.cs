using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Sequences.Api.V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SequencesController : ControllerBase
    {
        public string GetNext(int subjectId)
        {
            return string.Empty;
        }
    }
}