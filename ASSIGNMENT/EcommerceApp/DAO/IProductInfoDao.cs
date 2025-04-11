using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.DAO
{
    public interface IProductInfoDao<T>
    {
        T SaveProductInfo(T productInfo);
        bool DeleteProductInfo(int productId);
        
        T GetProductInfoById(int productId);
        T UpdateProductInfo(T productInfo);
        List<T> GetAllProductInfo();
    }
}
