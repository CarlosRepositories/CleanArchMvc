using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Application.Products.Queries;
using MediatR;

namespace CleanArchMvc.Application.Services
{
    public class ProductService : IProductService
    {
        public IMapper Mapper { get; set; }

        public IMediator Mediator { get; set; }

        public ProductService(IMapper mapper, IMediator mediator)
        {
            Mapper = mapper;
            Mediator = mediator;
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsAsync()
        {
            var productsQuery = new GetProductsQuery();

            if (productsQuery == null)
            {
                throw new Exception("Entity could not be loaded.");
            }

            var products = await Mediator.Send(productsQuery);

            return Mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        //public async Task<ProductDTO> GetProductCategorieAsync(int id)
        //{
        //    var productByIdQuery = new GetProductByIdQuery(id);

        //    if (productByIdQuery == null)
        //    {
        //        throw new Exception("Entity could not be loaded.");
        //    }

        //    var product = await Mediator.Send(productByIdQuery);

        //    return Mapper.Map<ProductDTO>(product);
        //}

        public async Task<ProductDTO> GetByIdAsync(int? id)
        {
            var productByIdQuery = new GetProductByIdQuery(id.Value);

            if (productByIdQuery == null)
            {
                throw new Exception("Entity could not be loaded.");
            }

            var product = await Mediator.Send(productByIdQuery);

            return Mapper.Map<ProductDTO>(product);
        }

        public async Task CreateAsync(ProductDTO product)
        {
            var productCreateCommand = Mapper.Map<ProductCreateCommand>(product);

            if (productCreateCommand == null)
            {
                throw new Exception("Entity could not be loaded.");
            }

            await Mediator.Send(productCreateCommand);
        }

        public async Task UpdateAsync(ProductDTO product)
        {
            var productUpdateCommand = Mapper.Map<ProductUpdateCommand>(product);

            if (productUpdateCommand == null)
            {
                throw new Exception("Entity could not be loaded.");
            }

            await Mediator.Send(productUpdateCommand);
        }

        public async Task RemoveAsync(int id)
        {
            var ProductRemoveCommand = new ProductRemoveCommand(id);

            if (ProductRemoveCommand == null)
            {
                throw new Exception("Entity could not be loaded.");
            }

            await Mediator.Send(ProductRemoveCommand);
        }
    }
}
