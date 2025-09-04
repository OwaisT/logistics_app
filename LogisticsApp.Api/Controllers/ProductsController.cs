using Microsoft.AspNetCore.Mvc;

namespace LogisticsApp.Api.Controllers;

[Route("[controller]")]
public class ProductsController : ApiController
{
    [HttpGet]
    public IActionResult ListProducts()
    {
        // var products = _productService.ListProducts();
        // var response = products.Select(p => new ProductResponse(p.ProductId, p.ProductRef, p.Season));
        // return Ok(response);
        return Ok(Array.Empty<string>()); // Placeholder response
    }

}