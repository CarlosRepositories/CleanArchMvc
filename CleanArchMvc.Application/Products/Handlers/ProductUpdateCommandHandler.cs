using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.Products.Handlers
{
    public class ProductUpdateCommandHandler : IRequestHandler<ProductUpdateCommand, Product>
    {
        IProductRepository ProductRepository;
        public ProductUpdateCommandHandler(IProductRepository productRepository)
        {
            ProductRepository = productRepository;
        }
        public async Task<Product> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
        {
            var product = await ProductRepository.GetByIdAsync(request.Id);

            if (product == null) 
            {
                throw new ApplicationException();
            }

            return await ProductRepository.UpdateAsync(product);
        }
    }
}
