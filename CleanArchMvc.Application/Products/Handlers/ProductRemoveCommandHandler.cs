﻿using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.Products.Handlers
{
    public class ProductRemoveCommandHandler : IRequestHandler<ProductRemoveCommand, Product>
    {
        IProductRepository ProductRepository;

        public ProductRemoveCommandHandler(IProductRepository productRepository)
        {
            ProductRepository = productRepository;
        }
        public async Task<Product> Handle(ProductRemoveCommand request, CancellationToken cancellationToken)
        {
            var product = await ProductRepository.GetByIdAsync(request.Id);

            if(product == null) 
            {
                throw new ApplicationException();
            }

            return await ProductRepository.RemoveAsync(product);
        }
    }
}
