using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Sequences.Data.Exceptions;
using Sequences.Services.Clients;

namespace Sequences.Api.V1.Controllers
{
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

        [HttpGet()]
        [Produces(contentType: "application/json")]
        [ProducesResponseType(typeof(IEnumerable<Client>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public ActionResult<List<Client>> GetClients()
        {
            try
            {
                return _service.GetClients();
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
        public ActionResult<Client> GetClientById(int id)
        {
            try
            {
                var clients = _service.GetClientById(id);
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

        [HttpPost()]
        [Produces(contentType: "application/json")]
        [ProducesResponseType(typeof(Client), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public ActionResult<Client> Add([FromBody] Client client)
        {
            try
            {
                client = _service.Add(client);
                return Ok(client);
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
        public ActionResult<Client> Update([FromBody] Client client)
        {
            try
            {
                client = _service.Update(client);
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
        public ActionResult<Client> UpdateOrCreate([FromRoute] int id, [FromBody] JsonPatchDocument clientDocument)
        {
            try
            {
                var client = _service.Update(id, clientDocument);
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
        public ActionResult<string> Delete(int id)
        {
            try
            {
                _service.Delete(id);
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