using System.Collections.Generic;

namespace NineYi.UnitTest.Service
{
    public interface IDiscountRepository
    {
        Dictionary<int, decimal> GetDiscountMap();
    }
}
