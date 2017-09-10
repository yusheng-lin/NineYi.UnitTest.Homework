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
            this._shoppingCartService = new ShoppingCartService();
        }

        [Fact]

        public void Test_BuyOneBook()
        {
            var shoppingCart = new ShoppingCartEntity { ISBNList = new string[] { "9573317249" } };

            var expected = 100M;

            var actual = this._shoppingCartService.Calculate(shoppingCart);

            Assert.Equal(expected, actual);
        }
    }
}
