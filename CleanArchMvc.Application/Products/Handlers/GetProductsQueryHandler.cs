using CleanArchMvc.Application.Products.Queries;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.Products.Handlers
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
    {
        IProductRepository ProductRepository;

        public GetProductsQueryHandler(IProductRepository productRepository)
        {
            ProductRepository = productRepository;
        }
        public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await ProductRepository.GetProductsAsync();

            if (products == null)
            {
                throw new ApplicationException();
            }

            return products;
        }
    }
}
