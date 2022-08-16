using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Sequences.Api.V1.Model;
using Sequences.Data.Exceptions;
using Sequences.Data.Subjects.Entities;
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
                var subjects = _mapper.Map<List<ResponseSubject>>(await _subjectsService.GetSubjects());

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
            try
            {
                var subject = _mapper.Map<ResponseSubject>(await _subjectsService.GetSubjectById(id));
                return Ok(subject);
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogError(message: "Controller: {Controller} Action: {Action}, Exception: {Message} StackTrace: {StackTrace}",
                   ControllerContext.ActionDescriptor.ControllerName, ControllerContext.ActionDescriptor.ActionName, ex.Message, ex.StackTrace);
                return Problem("subject not found", statusCode: StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: "Controller: {Controller} Action: {Action}, Exception: {Message} StackTrace: {StackTrace}",
                   ControllerContext.ActionDescriptor.ControllerName, ControllerContext.ActionDescriptor.ActionName, ex.Message, ex.StackTrace);
                return Problem("could not get subject information", statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        [Produces(contentType: "application/json")]
        [ProducesResponseType(typeof(RequestSubject), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseSubject>> Update([FromBody] RequestSubject requestSubject)
        {
            try
            {
                var subject = _mapper.Map<Subject>(requestSubject);
                var responseSubjet = _mapper.Map<ResponseSubject>(await _subjectsService.Update(subject));

                return Ok(responseSubjet);
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogError(message: "Controller: {Controller} Action: {Action}, Exception: {Message} StackTrace: {StackTrace}",
                   ControllerContext.ActionDescriptor.ControllerName, ControllerContext.ActionDescriptor.ActionName, ex.Message, ex.StackTrace);
                return Problem("subject not found", statusCode: StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: "Controller: {Controller} Action: {Action}, Exception: {Message} StackTrace: {StackTrace}",
                   ControllerContext.ActionDescriptor.ControllerName, ControllerContext.ActionDescriptor.ActionName, ex.Message, ex.StackTrace);
                return Problem("could not update subject", statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPatch("{id}")]
        [Produces(contentType: "application/json")]
        [ProducesResponseType(typeof(ResponseSubject), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseSubject>> PartialUpdate([FromRoute] int id, [FromBody] JsonPatchDocument subjectDocument)
        {
            try
            {
                var responseSubject = _mapper.Map<ResponseSubject>(await _subjectsService.Update(id, subjectDocument));

                return Ok(responseSubject);
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogError(message: "Controller: {Controller} Action: {Action}, Exception: {Message} StackTrace: {StackTrace}",
                   ControllerContext.ActionDescriptor.ControllerName, ControllerContext.ActionDescriptor.ActionName, ex.Message, ex.StackTrace);
                return Problem("subject not found", statusCode: StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: "Controller: {Controller} Action: {Action}, Exception: {Message} StackTrace: {StackTrace}",
                   ControllerContext.ActionDescriptor.ControllerName, ControllerContext.ActionDescriptor.ActionName, ex.Message, ex.StackTrace);
                return Problem("could not update subject", statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        [Produces(contentType: "application/json")]
        [ProducesResponseType(typeof(Subject), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> Delete([FromRoute] int id)
        {
            try
            {
                await _subjectsService.Delete(id);

                return Ok(new { result = "subject deleted" });
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogError(message: "Controller: {Controller} Action: {Action}, Exception: {Message} StackTrace: {StackTrace}",
                   ControllerContext.ActionDescriptor.ControllerName, ControllerContext.ActionDescriptor.ActionName, ex.Message, ex.StackTrace);
                return Problem("subject not found", statusCode: StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: "Controller: {Controller} Action: {Action}, Exception: {Message} StackTrace: {StackTrace}",
                   ControllerContext.ActionDescriptor.ControllerName, ControllerContext.ActionDescriptor.ActionName, ex.Message, ex.StackTrace);
                return Problem("could not update subject", statusCode: StatusCodes.Status500InternalServerError);
            }
        }
    }
}