using CleanArchMvc.Application.Products.Queries;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.Products.Handlers
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
    {
        IProductRepository ProductRepository;
        public GetProductByIdQueryHandler(IProductRepository productRepository)
        {
            ProductRepository = productRepository;
        }
        public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await ProductRepository.GetByIdAsync(request.Id);

            if (product == null) 
            {
                throw new ApplicationException();
            }

            return product;
        }
    }
}
