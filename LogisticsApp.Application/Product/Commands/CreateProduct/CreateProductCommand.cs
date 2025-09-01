using LogisticsApp.Application.Services.Product.Common;
using MediatR;

namespace LogisticsApp.Application.Product.Commands.CreateProduct;

public record CreateProductCommand(string ProductRef, string Season) : IRequest<ProductResult>;
