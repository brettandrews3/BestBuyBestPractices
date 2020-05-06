using System;
using System.Collections.Generic;

namespace BestBuyBestPractices
{
    public interface IProductRepository
    {
        public IEnumerable<Product> GetAllProducts(); //stubbed-out method
        void CreateProduct(string name, int price, int CategoryID);
    }
}
//"Anything that conforms to this interface must behave like this"
//Anything that conforms will contain the specific implementation of this behavior