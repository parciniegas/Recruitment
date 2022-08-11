using Microsoft.AspNetCore.Mvc;
using Sequences.Data.Exceptions;
using Sequences.Services.Clients;

namespace Sequences.Api.Clients
{
    [Route("clients")]
    [ApiController]
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
        [Route("/")]
        [ProducesResponseType(typeof(IEnumerable<Client>), 200)]
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

        [HttpGet]
        [Route("/{id}")]
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

        [HttpPost]
        [Route("/")]
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
        [Route("/")]
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

        [HttpDelete]
        [Route("/{id}")]
        [ProducesResponseType(typeof(Client), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public ActionResult<string> Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return Ok("client deleted");
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