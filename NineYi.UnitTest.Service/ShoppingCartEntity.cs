using System;
using System.Collections.Generic;

namespace NineYi.UnitTest.Service
{
    public class ShoppingCartEntity
    {
        public IEnumerable<string> ISBNList { get; set; }

        public decimal Price { get; set; }
    }
}
