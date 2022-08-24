using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Sequences.Api.V1.Model;
using Sequences.Data.Exceptions;
using Sequences.Services.Subjects;
using Sequences.Api.V1.Maps;

namespace Sequences.Api.V1.Controllers
{
    [Authorize]
    [ApiController]
    [Route("v{version:apiVersion}/subjects")]
    public class SubjectsController : ControllerBase
    {
        private readonly ILogger<SubjectsController> _logger;
        private readonly ISubjectsService _subjectsService;
        private readonly Mapper _mapper;

        public SubjectsController(ISubjectsService subjectsService, ILogger<SubjectsController> logger)
        {
            _subjectsService = subjectsService;
            _logger = logger;
            _mapper = new Mapper();
        }

        [HttpPost]
        [Produces(contentType: "application/json")]
        [ProducesResponseType(typeof(ResponseSubject), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseSubject>> Create([FromBody] CreateSubject createSubject)
        {
            try
            {
                var subject = _mapper.GetSubjectForCreate(createSubject);
                subject = await _subjectsService.Add(subject);
                var responseSubject = _mapper.GetResponseSubject(subject);

                return CreatedAtAction(nameof(GetSubjectById), new { id = responseSubject.SubjectId }, responseSubject);
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
                var responseSubjects = _mapper.GetResponseSubjects(subjects);

                return Ok(responseSubjects);
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
                var subject = await _subjectsService.GetSubjectById(id);
                var responseSubject = _mapper.GetResponseSubject(subject);

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
                return Problem("could not get subject information", statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        [Produces(contentType: "application/json")]
        [ProducesResponseType(typeof(UpdateSubject), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseSubject>> Update([FromBody] UpdateSubject updateSubject)
        {
            try
            {
                var subject = _mapper.GetSubjectForUpdate(updateSubject);
                subject = await _subjectsService.Update(subject);
                var responseSubjet = _mapper.GetResponseSubject(subject);

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
                var subject = await _subjectsService.Update(id, subjectDocument);
                var responseSubject = _mapper.GetResponseSubject(subject);

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
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
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

        [HttpGet("{id}/next_sequence")]
        [Produces(contentType: "application/json")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> NextSequence([FromRoute] int id)
        {
            try
            {
                return Ok(await _subjectsService.NextSequece(id));
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogError(message: "Controller: {Controller} Action: {Action}, Exception: {Message} StackTrace: {StackTrace}",
                   ControllerContext.ActionDescriptor.ControllerName, ControllerContext.ActionDescriptor.ActionName, ex.Message, ex.StackTrace);
                return Problem($"subject with id: {id} not found", statusCode: StatusCodes.Status404NotFound);
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