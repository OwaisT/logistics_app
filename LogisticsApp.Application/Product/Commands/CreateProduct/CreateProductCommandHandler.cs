using LogisticsApp.Application.Services.Product.Common;
using MediatR;

namespace LogisticsApp.Application.Product.Commands.CreateProduct;

// public class CreateProductCommandHandler : 
//     IRequestHandler<CreateProductCommand, ProductResult>
// {
//     // public Task<ProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
//     // {
//     //     // Logic to create a product would go here.
//     //     var product = Product.Create(command.ProductRef, command.Season);

//     //     var result = new ProductResult(
//     //         product.Id.ToString(),
//     //         product.ProductRef,
//     //         product.Season
//     //     );

//     //     return Task.FromResult(result);
//     // }
// }
