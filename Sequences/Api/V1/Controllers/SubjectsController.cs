using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Sequences.Api.V1.Model;
using Sequences.Data.Exceptions;
using Sequences.Services.Subjets;

namespace Sequences.Api.V1.Controllers
{
    [Authorize]
    [ApiController]
    [Route("v{version:apiVersion}/subjects")]
    public class SubjectsController : ControllerBase
    {
        private readonly ILogger<SubjectsController> _logger;
        private readonly ISubjectsService _subjectsService;
        private readonly IMapper _mapper;

        public SubjectsController(ISubjectsService subjectsService, ILogger<SubjectsController> logger, IMapper mapper)
        {
            _subjectsService = subjectsService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost]
        [Produces(contentType: "application/json")]
        [ProducesResponseType(typeof(ResponseSubject), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseSubject>> Create([FromBody] RequestSubject requestSubject)
        {
            try
            {
                var subject = _mapper.Map<Subject>(requestSubject);
                var responseSubject = _mapper.Map<ResponseSubject>(await _subjectsService.Add(subject));

                return CreatedAtAction(nameof(GetSubjectById), new { id = responseSubject.Id }, responseSubject);
            }
            catch (EntityAlreadyExistException ex)
            {
                _logger.LogError(message: "Controller: {Controller} Action: {Action}, Exception: {Message} StackTrace: {StackTrace}",
                    ControllerContext.ActionDescriptor.ControllerName, ControllerContext.ActionDescriptor.ActionName, ex.Message, ex.StackTrace);
                return Problem(ex.Message, statusCode: StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: "Controller: {Controller} Action: {Action}, Exception: {Message} StackTrace: {StackTrace}",
                    ControllerContext.ActionDescriptor.ControllerName, ControllerContext.ActionDescriptor.ActionName, ex.Message, ex.StackTrace);
                return Problem("could not add customer", statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Produces(contentType: "application/json")]
        [ProducesResponseType(typeof(IEnumerable<ResponseSubject>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<ResponseSubject>>> GetAll()
        {
            try
            {
                var subjects = await _subjectsService.GetSubjects();
                return Ok(subjects);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: "Controller: {Controller} Action: {Action}, Exception: {Message} StackTrace: {StackTrace}",
                    ControllerContext.ActionDescriptor.ControllerName, ControllerContext.ActionDescriptor.ActionName, ex.Message, ex.StackTrace);
                return Problem("could not get subjects information", statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        [Produces(contentType: "application/json")]
        [ProducesResponseType(typeof(ResponseSubject), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseSubject>> GetSubjectById([FromRoute] int id)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id}")]
        [Produces(contentType: "application/json")]
        [ProducesResponseType(typeof(RequestSubject), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<Subject> Update([FromRoute] int id, [FromBody] Subject subject)
        {
            throw new NotImplementedException();
        }

        [HttpPatch("{id}")]
        [Produces(contentType: "application/json")]
        [ProducesResponseType(typeof(ResponseSubject), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ResponseSubject> PartialUpdate([FromRoute] int id, [FromBody] JsonPatchDocument subjectDocument)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        [Produces(contentType: "application/json")]
        [ProducesResponseType(typeof(Subject), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> Delete([FromRoute] int id)
        {
            throw new NotImplementedException();
        }
    }
}