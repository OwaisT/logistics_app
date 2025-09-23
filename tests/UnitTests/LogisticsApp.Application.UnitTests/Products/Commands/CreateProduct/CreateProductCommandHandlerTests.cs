using FluentAssertions;
using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Application.Products.Commands.CreateProduct;
using LogisticsApp.Application.UnitTests.Products.Commands.TestUtils;
using LogisticsApp.Application.UnitTests.TestUtils.Products.Extensions;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Product;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Product.Services;
using Moq;

namespace LogisticsApp.Application.UnitTests.Products.Commands.CreateProduct;

public class CreateProductCommandHandlerTests
{
    private readonly CreateProductCommandHandler _handler;

    private readonly Mock<IProductRepository> _mockProductRepository = new();
    private readonly Mock<IProductUniquenessChecker> _mockProductUniquenessChecker = new();
    private readonly ProductFactory _productFactory;

    public CreateProductCommandHandlerTests()
    {
        _mockProductRepository = new Mock<IProductRepository>();
        _productFactory = new ProductFactory(_mockProductUniquenessChecker.Object);
        _handler = new CreateProductCommandHandler(_mockProductRepository.Object, _productFactory);
    }

    [Theory]
    [MemberData(nameof(ValidCreateProductCommands))]
    public async Task HandleCreateProductCommand_WhenProductIsValid_ShouldCreateAndReturnProduct(CreateProductCommand createProductCommand)
    {
        // Act
        var result = await _handler.Handle(createProductCommand, default);

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.ValidateProductCreatedFrom(createProductCommand);
        _mockProductRepository.Verify(p => p.Add(result.Value), Times.Once);

    }

    public static IEnumerable<object[]> ValidCreateProductCommands()
    {
        yield return new[] { CreateProductCommandUtils.CreateCommand() };
        yield return new[]
        {
            CreateProductCommandUtils.CreateCommand(
                assortments: CreateProductCommandUtils.CreateAssortmentsCommand(3))
        };
 
    }
}