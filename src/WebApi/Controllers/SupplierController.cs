using Application.Suppliers.Commands;
using Application.Suppliers.Queries;
using Canducci.Pagination;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    /// <summary>
    /// Gestão de Fornecedores
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        protected readonly ISender _sender;

        /// <summary>
        /// Gestão de Fornecedor
        /// </summary>
        public SupplierController(ISender sender)
        {
            _sender = sender;
        }

        /// <summary>
        /// Consultar Fornecedor por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSupplierByIdAsync([FromRoute] int id)
        {
            var retorno = await _sender.Send(new GetSupplierByIdQuery(id));
            return Ok(retorno);
        }

        /// <summary>
        /// Lista todos os Fornecedores paginas, podendo ser filtrado
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetPagedSuppliersAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string filter = "")
        {
            var products = await _sender.Send(new GetSupplierListQuery(page, pageSize, filter));

            return Ok(products.ToPaginatedRest(page, pageSize));
        }

        /// <summary>
        /// Adicionar novo Fornecedor
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddSupplierCommand command)
        {
            var retorno = await _sender.Send(command);

            return Created(nameof(GetSupplierByIdAsync), new { id = retorno });
        }

        /// <summary>
        /// Editar um Fornecedor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupplier(int id, [FromBody] UpdateSupplierCommand command)
        {
            if (id != command.Id)
            {
                throw new AppException($"Fornecedor de ID {command.Id} informado é inválido.", HttpStatusCode.NotFound);
            }

            await _sender.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Excluir um Fornecedor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(int id, [FromBody] DeleteSupplierCommand command)
        {
            if (id != command.Id)
            {
                throw new AppException($"Fornecedor de ID {command.Id} informado é inválido.", HttpStatusCode.NotFound);
            }

            await _sender.Send(command);

            return NoContent();
        }
    }
}
