namespace WebApplicationASP1.test
{
    public interface IMyTax
    {
        decimal Price { get; }
        string Currency { get; }
        decimal DefaultTaxRate { get; }

        decimal GetPriceWithTax(decimal price);
        decimal GetPriceWithTax(decimal price, decimal taxRate);

    }
}
