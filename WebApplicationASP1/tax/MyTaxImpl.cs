using Microsoft.Extensions.Options;
using WebApplicationASP1.Settings;

namespace WebApplicationASP1.test
{
    public class MyTaxImpl : IMyTax
    {
        private readonly TaxSettings _taxSettings;

        public decimal Price { get; private set; }
        public string Currency { get; private set; } = string.Empty;
        public decimal DefaultTaxRate => _taxSettings.DefaultTaxRate;

        public MyTaxImpl(IOptions<TaxSettings> taxSettings)
        {
            _taxSettings = taxSettings.Value;
        }

        public decimal GetPriceWithTax(decimal price) =>
            price + (price * _taxSettings.DefaultTaxRate);

        public decimal GetPriceWithTax(decimal price, decimal taxRate) =>
            price + (price * taxRate);
    }
}

