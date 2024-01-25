using Application.Products.Commands;
using Application.Products.Queries;
using Canducci.Pagination;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    /// <summary>
    /// Gestão de Produtos
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        protected readonly ISender _sender;

        /// <summary>
        /// Gestão de Produtos
        /// </summary>
        public ProductController(ISender sender)
        {
            _sender = sender;
        }

        /// <summary>
        /// Consultar Produtos por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductByIdAsync([FromRoute] int id)
        {
            var retorno = await _sender.Send(new GetProductByIdQuery(id));
            return Ok(retorno);
        }

        /// <summary>
        /// Lista todos os Produtos paginas, podendo ser filtrado
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetPagedProductsAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string filter = "")
        {
            var products = await _sender.Send(new GetProductListQuery(page, pageSize, filter));

            return Ok(products.ToPaginatedRest(page, pageSize));
        }

        /// <summary>
        /// Adicionar novo Produto
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddProductCommand command)
        {
            var retorno = await _sender.Send(command);

            return Created(nameof(GetProductByIdAsync), new { id = retorno });
        }

        /// <summary>
        /// Editar um Produto
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductCommand command)
        {
            if (id != command.Id)
            {
                throw new AppException($"Produto de ID {command.Id} informado é inválido.", HttpStatusCode.NotFound);
            }

            await _sender.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Excluir um Produto
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id, [FromBody] DeleteProductCommand command)
        {
            if (id != command.Id)
            {
                throw new AppException($"Produto de ID {command.Id} informado é inválido.", HttpStatusCode.NotFound);
            }

            await _sender.Send(command);

            return NoContent();
        }
    }
}
