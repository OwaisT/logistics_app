using FluentAssertions;
using LogisticsApp.Application.Aggregates.Products.Commands.CreateProduct;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;

namespace LogisticsApp.Application.UnitTests.TestUtils.Products.Extensions;
public static partial class ProductExtensions
{
    public static void ValidateProductCreatedFrom(this Product product, CreateProductCommand command)
    {
        product.Should().NotBeNull();
        product.RefCode.Should().Be(command.RefCode);
        product.Season.Should().Be(command.Season);
        product.Name.Should().Be(command.Name);
        product.Description.Should().Be(command.Description);
        product.IsActive.Should().Be(command.IsActive);
        product.Categories.Should().BeEquivalentTo(command.Categories);
        product.Colors.Should().BeEquivalentTo(command.Colors);
        product.Sizes.Should().BeEquivalentTo(command.Sizes);
        product.Assortments.Count.Should().Be(command.Assortments.Count);
        product.Assortments.Zip(command.Assortments).ToList().ForEach(pair => ValidateAssortment(pair.First, pair.Second));

        static void ValidateAssortment(Assortment assortment, CreateProductAssortmentCommand command)
        {
            assortment.Color.Should().Be(command.Color);
            assortment.Sizes.Should().BeEquivalentTo(command.Sizes);
        }


    }
}