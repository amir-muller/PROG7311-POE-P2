using Xunit;
using Web_API.Services;

namespace PROG7311_P2;

public class CurrencyTest
{
    [Fact]
    public void CalculateZarAmount_ReturnsCorrectProduct()
    {
        //arange 
        decimal usdAmount = 50;
        decimal exchangerate = 16;

        //act
        decimal zarAmount = usdAmount * exchangerate;

        //assert
        Assert.Equal(800, zarAmount);


    }
}