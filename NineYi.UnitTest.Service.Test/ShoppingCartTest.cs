using NSubstitute;
using System.Collections.Generic;
using Xunit;

namespace NineYi.UnitTest.Service.Test
{
    public class ShoppingCartTest
    {
        private readonly ShoppingCartService _shoppingCartService;

        public ShoppingCartTest()
        {

            var discountRepository = Substitute.For<IDiscountRepository>();
            var discountMap = new Dictionary<int, decimal>
            {
                { 2, 0.05M },
                { 3, 0.1M },
                { 4, 0.2M },
                { 5, 0.25M },
            };
            discountRepository.GetDiscountMap().Returns(discountMap);
            this._shoppingCartService = new ShoppingCartService(discountRepository);
        }

        [Fact]

        public void Test_BuyOneBook()
        {
            var shoppingCart = new ShoppingCartEntity { ISBNList = new string[] { "9573317249" } };

            var expected = 100M;

            var actual = this._shoppingCartService.Calculate(shoppingCart);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test_BuyTwoDiffBook()
        {
            var shoppingCart = new ShoppingCartEntity { ISBNList = new string[] { "9573317249", "9573317583" } };

            var expected = 2 * 100 * 0.95M;

            var actual = this._shoppingCartService.Calculate(shoppingCart);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test_BuyThreeDiffBook()
        {
            var shoppingCart = new ShoppingCartEntity { ISBNList = new string[] { "9573317249", "9573317583", "9573318008" } };

            var expected = 3 * 100 * 0.9M;

            var actual = this._shoppingCartService.Calculate(shoppingCart);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test_BuyFourDiffBook()
        {
            var shoppingCart = new ShoppingCartEntity { ISBNList = new string[] { "9573317249", "9573317583", "9573318008", "9573318318" } };

            var expected = 4 * 100 * 0.8M;

            var actual = this._shoppingCartService.Calculate(shoppingCart);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test_BuyFiveDiffBook()
        {
            var shoppingCart = new ShoppingCartEntity { ISBNList = new string[] { "9573317249", "9573317583", "9573318008", "9573318318", "9573319861" } };

            var expected = 5 * 100 * 0.75M;

            var actual = this._shoppingCartService.Calculate(shoppingCart);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test_BuyThreeOutOfFourDiffeBook()
        {
            var shoppingCart = new ShoppingCartEntity { ISBNList = new string[] { "9573317249", "9573317583", "9573318008", "9573317249" } };

            var expected = 3 * 100 * 0.9M + 100M;

            var actual = this._shoppingCartService.Calculate(shoppingCart);

            Assert.Equal(expected, actual);
        }
    }
}
