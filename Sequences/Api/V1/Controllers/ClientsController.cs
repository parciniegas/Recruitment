using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Sequences.Data.Exceptions;
using Sequences.Services.Clients;

namespace Sequences.Api.V1.Controllers
{
    [Authorize]
    [ApiController]
    [Route("v{version:apiVersion}/clients")]
    public class ClientsController : ControllerBase
    {
        #region Private Fields

        private readonly IClientsService _service;
        private readonly ILogger<ClientsController> _logger;

        #endregion Private Fields

        public ClientsController(IClientsService service, ILogger<ClientsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        #region Public Methods

        [HttpGet]
        [Produces(contentType: "application/json")]
        [ProducesResponseType(typeof(IEnumerable<Client>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Client>>> GetClientsAsync()
        {
            try
            {
                var clients = await _service.GetClients();
                return clients;
            }
            catch (Exception ex)
            {
                _logger.LogError(message: "Controller: {Controller} Action: {Action}, Exception: {Message} StackTrace: {StackTrace}",
                    ControllerContext.ActionDescriptor.ControllerName, ControllerContext.ActionDescriptor.ActionName, ex.Message, ex.StackTrace);
                return Problem("could not get customer information", statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        [Produces(contentType: "application/json")]
        [ProducesResponseType(typeof(Client), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Client>> GetClientById([FromRoute] int id)
        {
            try
            {
                var clients = await _service.GetClientById(id);
                return Ok(clients);
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogError(message: "Controller: {Controller} Action: {Action}, Exception: {Message} StackTrace: {StackTrace}",
                   ControllerContext.ActionDescriptor.ControllerName, ControllerContext.ActionDescriptor.ActionName, ex.Message, ex.StackTrace);
                return Problem("client not found", statusCode: StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: "Controller: {Controller} Action: {Action}, Exception: {Message} StackTrace: {StackTrace}",
                   ControllerContext.ActionDescriptor.ControllerName, ControllerContext.ActionDescriptor.ActionName, ex.Message, ex.StackTrace);
                return Problem("could not get customer information", statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Produces(contentType: "application/json")]
        [ProducesResponseType(typeof(Client), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Client>> Add([FromBody] Client client)
        {
            try
            {
                client = await _service.Add(client);
                return CreatedAtAction(nameof(GetClientById), new { id = client.Id }, client);
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

        [HttpPut]
        [Produces(contentType: "application/json")]
        [ProducesResponseType(typeof(Client), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Client>> Update([FromBody] Client client)
        {
            try
            {
                client = await _service.Update(client);
                return Ok(client);
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogError(message: "Controller: {Controller} Action: {Action}, Exception: {Message} StackTrace: {StackTrace}",
                   ControllerContext.ActionDescriptor.ControllerName, ControllerContext.ActionDescriptor.ActionName, ex.Message, ex.StackTrace);
                return Problem("client not found", statusCode: StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: "Controller: {Controller} Action: {Action}, Exception: {Message} StackTrace: {StackTrace}",
                   ControllerContext.ActionDescriptor.ControllerName, ControllerContext.ActionDescriptor.ActionName, ex.Message, ex.StackTrace);
                return Problem("could not update customer", statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPatch("{id}")]
        [Produces(contentType: "application/json")]
        [ProducesResponseType(typeof(Client), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Client>> UpdateOrCreate([FromRoute] int id, [FromBody] JsonPatchDocument clientDocument)
        {
            try
            {
                var client = await _service.Update(id, clientDocument);
                return Ok(client);
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogError(message: "Controller: {Controller} Action: {Action}, Exception: {Message} StackTrace: {StackTrace}",
                   ControllerContext.ActionDescriptor.ControllerName, ControllerContext.ActionDescriptor.ActionName, ex.Message, ex.StackTrace);
                return Problem("client not found", statusCode: StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: "Controller: {Controller} Action: {Action}, Exception: {Message} StackTrace: {StackTrace}",
                   ControllerContext.ActionDescriptor.ControllerName, ControllerContext.ActionDescriptor.ActionName, ex.Message, ex.StackTrace);
                return Problem("could not update customer", statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        [Produces(contentType: "application/json")]
        [ProducesResponseType(typeof(Client), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> Delete(int id)
        {
            try
            {
                await _service.Delete(id);
                return Ok(new { result = "client deleted" });
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogError(message: "Controller: {Controller} Action: {Action}, Exception: {Message} StackTrace: {StackTrace}",
                   ControllerContext.ActionDescriptor.ControllerName, ControllerContext.ActionDescriptor.ActionName, ex.Message, ex.StackTrace);
                return Problem("client not found", statusCode: StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: "Controller: {Controller} Action: {Action}, Exception: {Message} StackTrace: {StackTrace}",
                   ControllerContext.ActionDescriptor.ControllerName, ControllerContext.ActionDescriptor.ActionName, ex.Message, ex.StackTrace);
                return Problem("could not update customer", statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        #endregion Public Methods
    }
}