using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.Products.Handlers
{
    public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommand, Product>
    {
        IProductRepository ProductRepository { get; set; }

        public ProductCreateCommandHandler(IProductRepository productRepository)
        {
            ProductRepository = productRepository;
        }
        public async Task<Product> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
        {
            var product = new Product(request.Name, request.Description, request.Price, request.Stock, request.Image);

            if(product == null)
            {
                throw new ApplicationException();
            }
            else
            {
                product.CategoryId = request.CategoryId;
                return await ProductRepository.CreateAsync(product);
            }
        }
    }
}
