using LogisticsApp.Application.Aggregates.Products.Commands.CreateProduct;
using LogisticsApp.Application.UnitTests.TestUtils.Constants;

namespace LogisticsApp.Application.UnitTests.Products.Commands.TestUtils;

public static class CreateProductCommandUtils
{
    // Utility methods for creating product commands can be added here in the future.
    public static CreateProductCommand CreateCommand(
        List<CreateProductAssortmentCommand>? assortments = null) =>
        new(
            Constants.Product.RefCode,
            Constants.Product.Season,
            Constants.Product.Name,
            Constants.Product.Description,
            Constants.Product.GeneralPrice,
            Constants.Product.IsActive,
            Constants.Product.CategoriesFromCount(2),
            Constants.Product.ColorsFromCount(2),
            Constants.Product.SizesFromCount(5),
            assortments ?? CreateAssortmentsCommand(2)
        );

    public static List<CreateProductAssortmentCommand> CreateAssortmentsCommand(int assortmentsCount) =>
        Enumerable.Range(0, assortmentsCount)
            .Select(i => new CreateProductAssortmentCommand(
                Color: Constants.Product.AssortmentColorFromIndex(i),
                Sizes: Constants.Product.AssortmentSizes
            ))
            .ToList();
}
