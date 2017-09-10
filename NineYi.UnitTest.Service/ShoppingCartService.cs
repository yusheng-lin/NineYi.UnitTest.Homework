using System;
using System.Linq;

namespace NineYi.UnitTest.Service
{
    public class ShoppingCartService
    {    
        private readonly IDiscountRepository _discountRepository;

        public ShoppingCartService(IDiscountRepository discountRepository)
        {
            this._discountRepository = discountRepository;
        }

        public decimal Calculate(ShoppingCartEntity shoppingCart)
        {
            if (shoppingCart == null || shoppingCart.ISBNList == null)
                throw new ArgumentNullException("購物車不可為空");

            if (shoppingCart.ISBNList.Any() == false)
                throw new ArgumentNullException("購買書籍不可為空");

            this.ProcessRule1(shoppingCart);           
            this.ProcessRule2(shoppingCart);

            return shoppingCart.Price;
        }

        /// <remarks>
        /// 一到五集的哈利波特，每一本都是賣100元
        /// </remarks>
        private void ProcessRule1(ShoppingCartEntity shoppingCart)
        {
            var count = shoppingCart.ISBNList.Count();

            if (count > 1)
                return;
            shoppingCart.Price = 100;
        }


        /// <remarks>
        /// 如果你買了三本不同的書，則會有10%的折扣
        /// 如果你從這個系列買了兩本不同的書，則會有5%的折扣
        /// 如果是四本不同的書，則會有20%的折扣
        /// 如果你一次買了整套一到五集，恭喜你將享有25%的折扣
        /// 需要留意的是，如果你買了四本書，其中三本不同，第四本則是重複的，那麼那三本將享有10%的折扣，但重複的那一本，則仍須100元。
        /// </remarks>
        private void ProcessRule2(ShoppingCartEntity shoppingCart)
        {
            var bookList = shoppingCart.ISBNList;

            if (bookList.Count() < 2)
                return;

            var totalCount = bookList.Count();

            var distinctCount = bookList.Distinct().Count();

            var discountMap = this._discountRepository.GetDiscountMap();

            var discount = discountMap[distinctCount];

            var discountPrice = distinctCount * 100 * (1 - discount);

            var restPrice = 100M * (totalCount - distinctCount);

            shoppingCart.Price = discountPrice + restPrice;
        }
    }
}
