
using Microsoft.Extensions.Options;
using WebApplicationASP1.Models;
using WebApplicationASP1.Settings;
using WebApplicationASP1.test;
namespace WebApplicationASP1.Services
{
    public class TaxService
    {
        private readonly ProductService _productService;
        private readonly IMyTax _tax;
        private readonly decimal _defaultTax;

        public TaxService(
            ProductService productService,
            IMyTax tax,
            IOptions<TaxSettings> settings)
        {
            _productService = productService;
            _tax = tax;
            _defaultTax = settings.Value.DefaultTaxRate;
        }

        public async Task<decimal?> GetProductPriceWithTax(string productId)
        {
            var product = await _productService.GetByIdAsync(productId);

            if (product == null)
                return null;

            return _tax.GetPriceWithTax(product.Price, _defaultTax);
        }
    }
}
