using System;
using System.Linq;

namespace NineYi.UnitTest.Service
{
    public class ShoppingCartService
    {    
        public decimal Calculate(ShoppingCartEntity shoppingCart)
        {
            if (shoppingCart == null || shoppingCart.ISBNList == null)
                throw new ArgumentNullException("購物車不可為空");

            if (shoppingCart.ISBNList.Any() == false)
                throw new ArgumentNullException("購買書籍不可為空");

            this.ProcessRule1(shoppingCart);           

            return shoppingCart.Price;
        }

        /// <summary>
        /// 一到五集的哈利波特，每一本都是賣100元
        /// </summary>
        /// <param name="shoppingCart"></param>
        private void ProcessRule1(ShoppingCartEntity shoppingCart)
        {
            var count = shoppingCart.ISBNList.Count();

            if (count > 1)
                return;
            shoppingCart.Price = 100;
        }        
    }
}
